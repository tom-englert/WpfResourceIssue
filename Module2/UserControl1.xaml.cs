namespace Module2
{
    using System.ComponentModel.Composition;
    using System.Windows;

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export(typeof(FrameworkElement))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class UserControl1
    {
        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
