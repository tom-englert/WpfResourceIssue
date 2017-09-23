namespace Module1
{
    using System.ComponentModel.Composition;
    using System.Windows;

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export(typeof(FrameworkElement))]
    public partial class UserControl1
    {
        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
