using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for ReplaceControl.xaml
    /// </summary>
    public partial class ReplaceControl : PassArgumentToControl
    {
        public ReplaceControl()
        {
            InitializeComponent();
        }
        private void choiceButton_Click(object sender, RoutedEventArgs e)
        {
            string needle = NeedleTextBox.Text;
            string hammer = HammerTextBox.Text;

            // send to parent class
            List<string> arguments = new List<string>()
            {
                needle,
                hammer,
            };

            RaiseEventHandler(arguments);

        }
        override public void Refresh()
        {
            NeedleTextBox.Text = "";
            HammerTextBox.Text = "";

        }

    }
}
