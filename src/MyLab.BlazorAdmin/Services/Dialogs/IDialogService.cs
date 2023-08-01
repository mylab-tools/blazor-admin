using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Net.Mime;
using MyLab.BlazorAdmin.Shared.Dialogs;
using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Services.Dialogs
{
    /// <summary>
    /// Manages the dialogs
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Creates <see cref="IDialogBuilder{TContent}"/>
        /// </summary>
        /// <typeparam name="TContent">dialog content type</typeparam>
        /// <param name="title">title</param>
        IDialogBuilder<TContent> Create<TContent>(string title);
    }

    /// <summary>
    /// Represent a dialog place
    /// </summary>
    public interface IDialogPlace
    {
        void AddDialog<TContent>(DialogDescription description);
    }

    /// <summary>
    /// Registers a dialog place
    /// </summary>
    public interface IDialogPlaceRegistrar
    {
        /// <summary>
        /// Registers dialog place
        /// </summary>
        void Register(IDialogPlace dialogPlace);
    }

    class DialogService : IDialogService, IDialogPlaceRegistrar
    {
        private IDialogPlace? _dialogPlace;

        public void Register(IDialogPlace dialogPlace)
        {
            _dialogPlace = dialogPlace;
        }

        public IDialogBuilder<TContent> Create<TContent>(string title)
        {
            return new DialogBuilder<TContent>(title, _dialogPlace);
        }
    }

    /// <summary>
    /// Builds dialog <see cref="DialogDescription"/>
    /// </summary>
    public interface IDialogBuilder<TContent>
    {
        /// <summary>
        /// Adds OkYes button
        /// </summary>
        public IDialogBuilder<TContent> WithOkYesButton(Action callback, string title = "OK");
        /// <summary>
        /// Adds No button
        /// </summary>
        public IDialogBuilder<TContent> WithNoButton(Action callback, string title = "No");
        /// <summary>
        /// Adds cancel button
        /// </summary>
        public IDialogBuilder<TContent> WithCancelButton(string title = "Cancel");
        /// <summary>
        /// Adds custom button
        /// </summary>
        public IDialogBuilder<TContent> WithButton(DialogButtonDescription description);
        /// <summary>
        /// Specifies a footer component
        /// </summary>
        /// <typeparam name="TFooter">component type</typeparam>
        /// <param name="setParams">member init expression</param>
        public IDialogBuilder<TContent> WithFooter<TFooter>(Expression<Func<TFooter>>? setParams = null);
        /// <summary>
        /// Specifies the footer initial parameters
        /// </summary>
        /// <param name="setParams">member init expression</param>
        public IDialogBuilder<TContent> WithParameters(Expression<Func<TContent>> setParams);
        /// <summary>
        /// Specifies a modal-backdrop element 
        /// </summary>
        public IDialogBuilder<TContent> WithBackdrop(DialogBackdrop backdrop);
        /// <summary>
        /// Opens a dialog
        /// </summary>
        public DialogDescription Open();
    }
    
    class DialogBuilder<TContent> : IDialogBuilder<TContent>
    {
        readonly string _title;

        private readonly IDialogPlace? _dialogPlace;

        private List<DialogButtonDescription> _buttons = new();

        private Type _contentType;

        private Type? _footerType;

        private DialogBackdrop? _backdrop;

        private InitParametersDictionary? _contentParameters;

        private InitParametersDictionary? _footerParameters;
        
        public DialogBuilder(string title, IDialogPlace? dialogPlace)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));
            _dialogPlace = dialogPlace;
            _contentType = typeof(TContent);
        }

        public IDialogBuilder<TContent> WithOkYesButton(Action callback, string title = "OK")
        {
            var clone = Clone();
            
            clone._buttons.Add(new DialogButtonDescription(title){ Callback = callback, Primary = true});

            return clone;
        }

        public IDialogBuilder<TContent> WithNoButton(Action callback, string title = "No")
        {
            var clone = Clone();

            clone._buttons.Add(new DialogButtonDescription(title) { Callback = callback });

            return clone;
        }

        public IDialogBuilder<TContent> WithCancelButton(string title = "Cancel")
        {
            var clone = Clone();

            clone._buttons.Add(new DialogButtonDescription(title));

            return clone;
        }

        public IDialogBuilder<TContent> WithButton(DialogButtonDescription description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            var clone = Clone();

            clone._buttons.Add(description);

            return clone;
        }

        public IDialogBuilder<TContent> WithFooter<TFooter>(Expression<Func<TFooter>>? setParams = null)
        {
            var clone = Clone();

            clone._footerType = typeof(TFooter);

            if (setParams != null)
            {
                clone._footerParameters = InitParametersDictionary.FromExpression(setParams);
            }

            return clone;
        }

        public IDialogBuilder<TContent> WithParameters(Expression<Func<TContent>> setParams)
        {
            if (setParams == null) throw new ArgumentNullException(nameof(setParams));

            var clone = Clone();

            clone._contentParameters = InitParametersDictionary.FromExpression(setParams);

            return clone;
        }

        public IDialogBuilder<TContent> WithBackdrop(DialogBackdrop backdrop)
        {
            var clone = Clone();

            clone._backdrop = backdrop;

            return clone;
        }

        public DialogDescription Open()
        {
            throw new NotImplementedException();
        }

        DialogDescription ToDescription()
        {
            return new DialogDescription(_contentType)
            {
                Title = _title,
                Backdrop = _backdrop ?? DialogBackdrop.False,
                FooterType = _footerType,
                Buttons = _buttons.ToImmutableArray(),
                ContentParameters = _contentParameters,
                FooterParameters = _footerParameters
            };
        }

        DialogBuilder<TContent> Clone()
        {
            return new DialogBuilder<TContent>(_title, _dialogPlace)
            {
                _backdrop = _backdrop,
                _contentParameters = _contentParameters,
                _footerParameters = _footerParameters,
                _buttons = _buttons,
                _contentType = _contentType,
                _footerType = _footerType
            };
        }
    }

    public interface IDialog
    {

    }
}
