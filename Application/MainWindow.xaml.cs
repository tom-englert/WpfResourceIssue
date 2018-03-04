namespace Application
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly AggregateCatalog _catalog = new AggregateCatalog();
        private readonly CompositionContainer _container;
        private readonly ObservableCollection<FrameworkElement> _visuals = new ObservableCollection<FrameworkElement>();

        public MainWindow()
        {
            _container = new CompositionContainer(_catalog);

            InitializeComponent();

            ItemsControl.ItemsSource = _visuals;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _catalog.Dispose();
            _container.Dispose();
        }

        private void Module_CheckedChanged(object sender, RoutedEventArgs e)
        {
            // does not work if set here:
            AppContext.SetSwitch("Switch.System.Windows.Baml2006.AppendLocalAssemblyVersionForSourceUri", true);

            var checkBox = (CheckBox)sender;
            var folder = (string)checkBox.Tag;

            checkBox.IsEnabled = false;

            var applicationDirectory = new DirectoryInfo(Path.GetDirectoryName(GetType().Assembly.Location));
            var directory = applicationDirectory.GetDirectories(folder);

            var moduleAssemblies = directory
                .SelectMany(dir => dir.EnumerateFiles("*.dll"))
                .Select(file => Assembly.LoadFrom(file.FullName))
                .Select(assembly => new AssemblyCatalog(assembly))
                .ToArray();

            foreach (var assemblyCatalog in moduleAssemblies)
            {
                _catalog.Catalogs.Add(assemblyCatalog);
            }

            var visualExports = _container.GetExports<FrameworkElement>();

            foreach (var export in visualExports)
            {
                var f = export.Value;

                if (!_visuals.Contains(f))
                    _visuals.Add(f);
            }
        }
    }
}