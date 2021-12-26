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

namespace WpfApp2
{
    public partial class Addsuffix : Window
    {
        public Addsuffix()
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
        override public void Clear()
        {
            SuffixTextBox.Text = "";

        }
    }
}
