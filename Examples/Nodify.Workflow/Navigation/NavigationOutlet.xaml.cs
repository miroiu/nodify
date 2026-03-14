using Nodify.Workflow.Common;
using R3;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Nodify.Workflow.Navigation;

/// <summary>
/// Interaction logic for NavigationOutlet.xaml
/// </summary>
public partial class NavigationOutlet : UserControl
{
    private IDisposable? _viewSubscription;

    public static readonly DependencyProperty NavigationServiceProperty = DependencyProperty.Register(nameof(NavigationService), typeof(NavigationService), typeof(NavigationOutlet), new PropertyMetadata(OnNavigationServiceChanged));

    public NavigationService NavigationService
    {
        get => (NavigationService)GetValue(NavigationServiceProperty);
        set => SetValue(NavigationServiceProperty, value);
    }

    public NavigationOutlet()
    {
        InitializeComponent();
    }

    private static void OnNavigationServiceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NavigationOutlet outlet)
        {
            if (e.OldValue is NavigationService)
            {
                outlet.UnsubscribeFromNavigation();
            }

            if (e.NewValue is NavigationService newService)
            {
                outlet.SubscribeToNavigation(newService);
            }
        }
    }

    private void UnsubscribeFromNavigation()
    {
        _viewSubscription?.Dispose();
    }

    private void SubscribeToNavigation(NavigationService newService)
    {
        _viewSubscription = Observable.FromEventHandler<NavigationEventArgs>(
            h => newService.Navigated += h,
            h => newService.Navigated -= h)
            .Select(args => args.e)
            .SubscribeAwait(OnNavigatedAsync, AwaitOperation.Switch);

        if (newService.CurrentView.Value is not null)
        {
            ContentHost.Content = newService.CurrentView.Value;
        }
    }

    private async ValueTask OnNavigatedAsync(NavigationEventArgs args, CancellationToken cancellationToken)
    {
        const double offset = 100d;

        if (args.IsSameLayer)
        {
            if (args.Direction is NavigationDirection.Forward)
            {
                await ContentHost.FadeOut();
                ContentHost.Content = args.NewEntry.ViewModel;
                await Task.WhenAll
                (
                    ContentHost.FadeIn(),
                    ContentHost.TranslateY(0, new AnimationOptions<double> { From = offset, Easing = new CubicEase { EasingMode = EasingMode.EaseIn } })
                );
            }
            else
            {
                await Task.WhenAll
                (
                    ContentHost.FadeOut(),
                    ContentHost.TranslateY(offset, new AnimationOptions<double> { From = 0, Easing = new CubicEase { EasingMode = EasingMode.EaseIn } })
                );
                ContentHost.Content = args.NewEntry.ViewModel;
                await Task.WhenAll
                (
                    ContentHost.TranslateY(0, new AnimationOptions<double> { Duration = TimeSpan.Zero, Easing = new CubicEase { EasingMode = EasingMode.EaseIn } }),
                    ContentHost.FadeIn()
                );
            }
        }
        else
        {
            if (args.Direction is NavigationDirection.Forward)
            {
                await ContentHost.TranslateX(-offset, new AnimationOptions<double> { From = 0, Easing = new CubicEase { EasingMode = EasingMode.EaseIn } });
                ContentHost.Content = args.NewEntry.ViewModel;
                await ContentHost.TranslateX(0, new AnimationOptions<double> { From = offset * 2, Easing = new CubicEase { EasingMode = EasingMode.EaseOut } });
            }
            else
            {
                await ContentHost.TranslateX(offset, new AnimationOptions<double> { From = 0, Easing = new CubicEase { EasingMode = EasingMode.EaseIn } });
                ContentHost.Content = args.NewEntry.ViewModel;
                await ContentHost.TranslateX(0, new AnimationOptions<double> { From = -offset, Easing = new CubicEase { EasingMode = EasingMode.EaseOut } });
            }
        }
    }
}
