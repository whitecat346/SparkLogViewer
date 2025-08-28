using System.ComponentModel;
using SparkLogViewer.ViewModels;

namespace SparkLogViewer.Views;

public partial class SparkLogViewerView : ContentPage
{
    private readonly SparkLogViewerViewModel _viewModel;
    private bool _isFirstAppearance = true;

    public SparkLogViewerView(SparkLogViewerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SparkLogViewerViewModel.IsLoading))
        {
            if (!_viewModel.IsLoading && _viewModel.Logs.Any())
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LogCollectionView.ScrollTo(_viewModel.Logs.First(), position: ScrollToPosition.Start,
                                               animate: false);
                });
            }
        }
    }

    /// <inheritdoc />
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!_isFirstAppearance)
        {
            return;
        }

        _isFirstAppearance = false;
        var args = Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            _viewModel.LoadLogCommand.ExecuteAsync(args[1]);
        }
        else
        {
#if DEBUG

            var testFilePath =
                @"C:\Users\WhiteCAT\Desktop\Games\PCL2\.minecraft\versions\1.20.4-Fabric 0.15.11-[轻量通用]\logs\2024-07-15-1.log";
            _viewModel.LoadLogCommand.ExecuteAsync(testFilePath);
#endif
        }
    }
}
