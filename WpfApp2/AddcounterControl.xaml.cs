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
    /// <summary>
    /// Interaction logic for AddcounterControl.xaml
    /// </summary>
    public partial class AddcounterControl : ArgumentForStringActionControl
    {
        public AddcounterControl()
        {
            InitializeComponent();
        }

        private void choiceButton_Click(object sender, RoutedEventArgs e)
        {
            // get arguments

            string start = StartTextBox.Text;
            string step = StepTextBox.Text;

            List<string> arguments = new List<string>()
            {
                start,
                step
            };

            RaiseEventHandler(arguments);
        }
        override public void Clear()
        {

            StartTextBox.Text = "";
            StepTextBox.Text = "";

        }
    }
}
