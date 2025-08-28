namespace SparkLogViewer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    /// <inheritdoc />
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        const int fixedWidth = 830;
        const int fixedHeight = 450;

        window.Width = fixedHeight;
        window.Height = fixedHeight;

        window.MinimumWidth = fixedWidth;
        window.MinimumHeight = fixedHeight;


        //window.MaximumWidth = fixedWidth;
        //window.MaximumHeight = fixedHeight;

        return window;
    }
}