namespace Application
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly AggregateCatalog _catalog = new AggregateCatalog();
        private readonly CompositionContainer _container;


        public MainWindow()
        {
            _container = new CompositionContainer(_catalog);

            InitializeComponent();

            Loaded += Self_Loaded;
        }

        private void Self_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var applicationDirectory = new DirectoryInfo(Path.GetDirectoryName(GetType().Assembly.Location));

            var moduleDirectories = applicationDirectory.EnumerateDirectories("Module*");

            // uncomment to only load moudle1:
            // moduleDirectories = moduleDirectories.Take(1);

            // uncomment to load modules in reverse order:
            // moduleDirectories = moduleDirectories.Reverse()

            var moduleAssemblies = moduleDirectories
                .SelectMany(dir => dir.EnumerateFiles("*.dll"))
                .Select(file => Assembly.LoadFile(file.FullName))
                .Select(path => new AssemblyCatalog(path))
                .ToArray();

            foreach (var assemblyCatalog in moduleAssemblies)
            {
                _catalog.Catalogs.Add(assemblyCatalog);
            }

            var visualExports = _container.GetExports<FrameworkElement>();

            var visuals = visualExports.Select(item => item.Value).ToArray();

            ItemsControl.ItemsSource = visuals;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _catalog.Dispose();
            _container.Dispose();
        }
    }
}