using System.Windows.Input;

namespace FitMailHiFi.Views
{
    public partial class HelpWindow
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
