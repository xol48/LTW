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
    /// Interaction logic for ChangeExtensionControl.xaml
    /// </summary>
    public partial class ChangeExtensionControl : PassArgumentToControl
    {
        public ChangeExtensionControl()
        {
            InitializeComponent();
        }
        private void choiceButton_Click(object sender, RoutedEventArgs e)
        {

            string extension = ExtensionTextBox.Text;

            List<string> arguments = new List<string>()
            {
                extension
            };

            RaiseEventHandler(arguments);
        }
        override public void Refresh()
        {
            ExtensionTextBox.Text = "";

        }
    }
}
