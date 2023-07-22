using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using MyLab.BlazorAdmin.ComponentModel;

namespace MyLab.BlazorAdmin.Services.PageNavigation;

class PageNavigator : IPageNavigator
{
    private readonly NavigationCategory[] _navigation;
    private readonly Dictionary<string, IndexedPageNav>.ValueCollection _index;

    public PageNavigator(IOptions<PageNavigationOptions> opts)
        : this(opts.Value)
    {
        
    }

    public PageNavigator(PageNavigationOptions opts)
    {
        _navigation = CreateNavigation(opts.Navigation);

        _index = IndexNavigation(opts.Navigation);
    }

    public IEnumerable<NavigationCategory> GetNavigation()
    {
        return _navigation;
    }

    public PageNavigation? GetPageDescription(string url)
    {
        var urlItems = url == "/"
            ? new[] { "" }
            : url.Split('/', StringSplitOptions.RemoveEmptyEntries);

        foreach (var indexed in _index)
        {
            if (IsSuitable(urlItems, indexed.Items, out var bindings))
            {
                if (bindings == null) continue;

                var pathItems = new List<PageNavigationPathItem>();

                for (int i = 0; i < bindings!.Length; i++)
                {
                    var pageUrl = "/" + string.Join("/", bindings.Take(i + 1).Select(b => b.ActualUrlItem));
                    pathItems.Add(new PageNavigationPathItem(bindings[i].Indexed.Title, pageUrl));
                }

                var result = new PageNavigation(
                    indexed.Page.Title,
                    pathItems.ToArray()
                );

                return result;
            }
        }

        return null;

        bool IsSuitable(string[] actual, PageNavigationNodeDefinition[] indexed, out (string ActualUrlItem, PageNavigationNodeDefinition Indexed)[]? bindings)
        {
            if (actual.Length < indexed.Length)
            {
                bindings = null;
                return false;
            }

            var resB = new List<(string ActualUrlItem, PageNavigationNodeDefinition Indexed)>();

            for (int i = 0; i < actual.Length; i++)
            {
                if (i >= indexed.Length)
                {
                    bindings = null;
                    return false;
                }

                var indexedUrlNameItems = indexed[i].UrlItem.Split('/');

                if (!IsUrlPartSuitable(actual.Skip(i).ToArray(), indexedUrlNameItems))
                {
                    bindings = null;
                    return false;
                }

                var actualUrlItem = string.Join('/', actual.Skip(i).Take(indexedUrlNameItems.Length));

                resB.Add((actualUrlItem, indexed[i]));
                i += indexedUrlNameItems.Length - 1;
            }

            bindings = resB.ToArray();
            return true;
        }

        bool IsUrlPartSuitable(string[] actualTail, string[] indexed)
        {
            if (actualTail.Length < indexed.Length)
                return false;

            for (int i = 0; i < indexed.Length; i++)
            {
                if (actualTail[i] != indexed[i] && indexed[i] != "{*}")
                    return false;
            }

            return true;
        }
    }

    private NavigationCategory[] CreateNavigation(IEnumerable<NavigationCategoryDefinition> navigation)
    {
        return navigation.Select(g => new NavigationCategory
        {
            Title = g.Title,
            Pages = g.Nodes?.Select(p => new NavigationLink
            (
                p.Title,
                p.UrlItem,
                p.FaClass
            )).ToArray()
        }).ToArray();
    }

    private Dictionary<string, IndexedPageNav>.ValueCollection IndexNavigation(IEnumerable<NavigationCategoryDefinition> navigation)
    {
        var index = new Dictionary<string, IndexedPageNav>();

        foreach (var navigationGroupDefinition in navigation)
        {
            if (navigationGroupDefinition.Nodes != null)
            {
                foreach (var pageNavigationDefinition in navigationGroupDefinition.Nodes)
                {
                    IndexPage(pageNavigationDefinition);
                }
            }
        }

        return index.Values;

        void IndexPage(PageNavigationNodeDefinition page)
        {
            var sequences = GetSubSequences(page);

            foreach (var sequence in sequences)
            {

                var urlItems = sequence
                    .Select(s => s)
                    .ToArray();

                var key = string.Join('/', urlItems.Select(i => i.UrlItem));

                if (index.ContainsKey(key)) continue;

                var indexItem = new IndexedPageNav(
                    sequence.Last(),
                    urlItems
                );

                index.Add(key, indexItem);
            }
        }

        IEnumerable<PageNavigationNodeDefinition[]> GetSubSequences(PageNavigationNodeDefinition page)
        {
            var sequences = new List<PageNavigationNodeDefinition[]> { new[] { page } };

            if (page.Nodes != null)
            {
                foreach (var child in page.Nodes)
                {
                    foreach (var subSequence in GetSubSequences(child))
                    {
                        var seq = Enumerable
                            .Repeat(page, 1)
                            .Concat(subSequence)
                            .ToArray();
                        sequences.Add(seq);
                    }
                }
            }

            return sequences;
        }
    }

    class IndexedPageNav
    {
        public PageNavigationNodeDefinition Page { get; }
        public PageNavigationNodeDefinition[] Items { get; }

        public IndexedPageNav(PageNavigationNodeDefinition page, PageNavigationNodeDefinition[] items)
        {
            Page = page;
            Items = items;
        }
    }
}