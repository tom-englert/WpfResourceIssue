namespace Application
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    using TomsToolbox.Desktop;
    using TomsToolbox.Desktop.Composition;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly CompositionHost _compositionHost = new CompositionHost();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += Self_Loaded;
        }

        private void Self_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var applicationDirectory = GetType().Assembly.GetAssemblyDirectory();

            var ModuleDirectories = applicationDirectory.EnumerateDirectories("Module*");

            // uncomment to only load moudle1:
            // ModuleDirectories = ModuleDirectories.Take(1);

            var ModuleAssemblies = ModuleDirectories
                .SelectMany(dir => dir.EnumerateFiles("*.dll"))
                .Select(file => Assembly.LoadFile(file.FullName))
                .ToArray();

            _compositionHost.AddCatalog(ModuleAssemblies);

            var visualExports = _compositionHost.GetExports<FrameworkElement>();

            var visuals = visualExports.Select(item => item.Value).ToArray();

            ItemsControl.ItemsSource = visuals;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _compositionHost.Dispose();
        }
    }
}