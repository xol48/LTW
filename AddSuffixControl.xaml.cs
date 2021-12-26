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
    /// Interaction logic for AddSuffixControl.xaml
    /// </summary>
    public partial class AddSuffixControl : PassArgumentToControl
    {
        public AddSuffixControl()
        {
            InitializeComponent();
        }
        private void choiceButton_Click(object sender, RoutedEventArgs e)
        {

            string suffix = SuffixTextBox.Text;

            List<string> arguments = new List<string>()
            {
                suffix
            };

            RaiseEventHandler(arguments);
        }
        override public void Refresh()
        {
            SuffixTextBox.Text = "";

        }
    }
}
