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
    /// Interaction logic for ReplaceControl.xaml
    /// </summary>
    public partial class ReplaceControl : ArgumentForStringActionControl
    {
        public ReplaceControl()
        {
            InitializeComponent();

        }

        private void choiceButton_Click(object sender, RoutedEventArgs e)
        {
            // get arguments
            var areaSelected = AreaComboBox.SelectedItem as ComboBoxItem;
            string area = areaSelected.Content as string;
            string needle = NeedleTextBox.Text;
            string hammer = HammerTextBox.Text;

            // send to parent class
            List<string> arguments = new List<string>()
            {
                needle,
                hammer,
                area
            };

            RaiseEventHandler(arguments);

        }
        override public void Clear()
        {
            AreaComboBox.SelectedIndex = 0;
            NeedleTextBox.Text = "";
            HammerTextBox.Text = "";

        }

        private void AreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
