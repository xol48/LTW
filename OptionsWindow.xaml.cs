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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        // constructor
        public OptionsWindow()
        {
            InitializeComponent();
        }

        // property
        public int NameCollisionOption { get; set; } = NewName;

        // static values
        public static int NewName = 0;

        public static int Skip = 1;


        // event handler
        private void NameCollisionOption_Changed(object sender, RoutedEventArgs e)
        {
            if (Option1RadioButton.IsChecked == true)
            {
                NameCollisionOption = NewName;
            }
            else if (Option2RadioButton.IsChecked == true)
            {
                NameCollisionOption = Skip;
            }

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
