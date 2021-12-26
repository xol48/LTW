using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AddPrefixControl.xaml
    /// </summary>
    public partial class AddPrefixControl : ArgumentForStringActionControl

    {
        public AddPrefixControl()
        {
            InitializeComponent();
        }
        private void choiceButton_Click(object sender, RoutedEventArgs e)
        {

            string prefix = PrefixTextBox.Text;

            List<string> arguments = new List<string>()
            {
                prefix
            };

            RaiseEventHandler(arguments);
        }
        override public void Clear()
        {
            PrefixTextBox.Text = "";

        }
    }
}
