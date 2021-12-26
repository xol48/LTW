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
    /// Interaction logic for LowerCase_RemoveSpaces.xaml
    /// </summary>
    public partial class LowerCase_RemoveSpaces : Window
    {
        public LowerCase_RemoveSpaces()
        {
            InitializeComponent();
        }

        private void choiceButton_Click(object sender, RoutedEventArgs e)
        {
            


            List<string> arguments = new List<string>() {  };

            //RaiseEventHandler(arguments);
        }

        /*  override public void Clear()
          {
              AddPrefixTextBox.Text = "";
          }
        */
    }
}
