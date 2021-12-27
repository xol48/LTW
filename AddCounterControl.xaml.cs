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
    /// Interaction logic for AddCounterControl.xaml
    /// </summary>
    public partial class AddCounterControl : PassArgumentToControl
    {
        public AddCounterControl()
        {
            InitializeComponent();
        }

        private void ChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            string step = StepTextBox.Text;
            string start = StartTextBox.Text;
            string number = NumberOfDigitTextBox.Text;

            List<string> arguments = new List<string>()
            {
                start,
                step,
                number,
            };

            RaiseEventHandler(arguments);
        }
        override public void Refresh()
        {
            StepTextBox.Text = "";
            StartTextBox.Text = "";
            NumberOfDigitTextBox.Text = "";

        }
    }
}
