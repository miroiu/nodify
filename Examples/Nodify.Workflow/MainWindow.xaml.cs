using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Nodify.Workflow.Shell;
using R3;

namespace Nodify.Workflow;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private const double _defaultMainWorkflowWidth = 350;
    private const double _defaultWorkflowsPanelWidth = 220;

    private double _lastMainWorkflowWidth = _defaultMainWorkflowWidth;
    private bool _isZenMode;
    private bool _hasSelectedWorkflow;

    private readonly Dictionary<ColumnDefinition, AnimatableDouble> _animations = [];

    static MainWindow()
    {
        NodifyEditor.AutoRegisterConnectionsLayer = false;
    }

    public MainWindow()
    {
        InitializeComponent();

        StateChanged += UpdateMainWindowVisuals;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ApplicationViewModel viewModel)
        {
            viewModel.IsZenMode.Subscribe(OnZenModeChanged);
            viewModel.SelectedWorkflow.Subscribe(OnSelectedWorkflowChanged);
        }
    }

    private void OnZenModeChanged(bool isZenMode)
    {
        _isZenMode = isZenMode;
        UpdateSplitterVisibility();

        if (isZenMode)
        {
            // Save current width before collapsing
            _lastMainWorkflowWidth = MainWorkflowColumn.Width.Value;

            if (_hasSelectedWorkflow)
            {
                AnimateColumnWidth(MainWorkflowColumn, MainWorkflowColumn.Width.Value, 0);
            }
            AnimateColumnWidth(WorkflowsPanelColumn, WorkflowsPanelColumn.Width.Value, 0);
        }
        else
        {
            // Restore to saved width
            if (_hasSelectedWorkflow)
            {
                AnimateColumnWidth(MainWorkflowColumn, 0, _lastMainWorkflowWidth);
            }
            AnimateColumnWidth(WorkflowsPanelColumn, 0, _defaultWorkflowsPanelWidth);
        }
    }

    private void OnSelectedWorkflowChanged(object? selectedWorkflow)
    {
        _hasSelectedWorkflow = selectedWorkflow is not null;
        UpdateSplitterVisibility();

        if (_hasSelectedWorkflow)
        {
            // Reset splitter state: restore columns to their default widths
            SelectedWorkflowColumn.Width = new GridLength(1, GridUnitType.Star);
            MainWorkflowColumn.Width = new GridLength(_defaultMainWorkflowWidth);
        }
        else
        {
            // Save current width and expand to fill remaining space
            if (MainWorkflowColumn.Width.IsAbsolute)
            {
                _lastMainWorkflowWidth = MainWorkflowColumn.Width.Value;
            }
            SelectedWorkflowColumn.Width = new GridLength(0);
            MainWorkflowColumn.Width = new GridLength(1, GridUnitType.Star);
        }
    }

    private void UpdateSplitterVisibility()
    {
        ColumnSplitter.Visibility = _hasSelectedWorkflow && !_isZenMode
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void AnimateColumnWidth(ColumnDefinition column, double from, double to)
    {
        var animation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = TimeSpan.FromSeconds(0.25),
            EasingFunction = new CubicEase { EasingMode = to == 0 ? EasingMode.EaseIn : EasingMode.EaseOut }
        };

        if (!_animations.TryGetValue(column, out var animatable))
        {

            animatable = new AnimatableDouble();
            animatable.ValueChanged += (_, value) => column.Width = new GridLength(value);
            animatable.BeginAnimation(AnimatableDouble.ValueProperty, animation);

            _animations[column] = animatable;
        }
        else
        {
            animatable.BeginAnimation(AnimatableDouble.ValueProperty, animation);
        }
    }

    private void UpdateMainWindowVisuals(object? sender, EventArgs args)
    {
        BorderThickness = WindowState is WindowState.Maximized ? new Thickness(8) : new Thickness(0);
    }

    private class AnimatableDouble : Animatable
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(AnimatableDouble),
                new PropertyMetadata(0.0, OnValueChanged));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public event EventHandler<double>? ValueChanged;

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatableDouble)d).ValueChanged?.Invoke(d, (double)e.NewValue);
        }

        protected override Freezable CreateInstanceCore() => new AnimatableDouble();
    }
}