using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BatchRename
{
    public class NameToTitleConverter : IValueConverter
    {
        // static instance
        public static readonly NameToTitleConverter Instance = new NameToTitleConverter();

        // convert action classname to title, example: 'NewCaseAction' => 'New Case'
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            string result = "";
            input =input.Replace("Rule","");
            foreach( var c in input)
            {
                if(c>='A' && c<='Z')
                {
                    result += " ";
                }
                result += c;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

   
}
