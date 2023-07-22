using System.Security.Claims;
using System.Timers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MyLab.BlazorAdmin.Services;

namespace MyLab.BlazorAdmin.Shared
{
    /// <summary>
    /// User block component
    /// </summary>
    public partial class UserBlock : IDisposable
    {
        private readonly System.Timers.Timer _timer = new(5000);
        private UserMetric[]? _metrics;
        private UserInfo? _userInfo;
        private IUserMetricsProvider? _userMetricsProvider;
        private IUserInfoProvider? _userInfoProvider;

        /// <summary>
        /// <see cref="IServiceProvider"/> injection
        /// </summary>
        [Inject]
        public IServiceProvider? ServiceProvider { get; set; }
        /// <summary>
        /// <see cref="AuthenticationStateProvider"/> injection
        /// </summary>
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            _userMetricsProvider = ServiceProvider!.GetService<IUserMetricsProvider>();
            _userInfoProvider = ServiceProvider!.GetRequiredService<IUserInfoProvider>();

            _timer.Elapsed += OnTimerCallback;
            _timer.Start();

            var authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
            _userInfo = await _userInfoProvider.ProvideAsync(authState.User);

            await UpdateKlStateAsync();
            await base.OnInitializedAsync();
        }

        private void OnTimerCallback(object? sender, ElapsedEventArgs e)
        {
            _ = InvokeAsync(UpdateKlStateAsync);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _timer.Dispose();
        }

        async Task UpdateKlStateAsync()
        {
            if (_userMetricsProvider != null)
            {
                try
                {
                    _metrics = await _userMetricsProvider.ProvideAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                StateHasChanged();
            }
        }
    }
}
