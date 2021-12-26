using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BatchRename
{
    public class ActionToTitleConverter : IValueConverter
    {
        // static instance
        public static readonly ActionToTitleConverter Instance = new ActionToTitleConverter();

        // convert action classname to title, example: 'NewCaseAction' => 'New Case'
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string haystack = value as string;
            string result = "";

            // insert spaces
            foreach (var character in haystack)
            {
                char charValue = character;
                if (charValue >= (char)65 && charValue <= (char)90)
                {
                    result += ' '.ToString() + character.ToString();
                }
                else
                {
                    result += character.ToString();
                }
            }

            // remove 'action'
            string target = "Action";
            int index = result.IndexOf(target);
            if (index >= 0)
            {
                result = result.Remove(index, target.Length);  
            }

            // remove start and end spaces
            result = result.TrimStart(' ');
            result = result.TrimEnd(' ');

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RelativeToAbsolutePathConverter : IValueConverter
    {
        // static instance
        public static readonly RelativeToAbsolutePathConverter Instance 
                                = new RelativeToAbsolutePathConverter();

        // convert relative path to absolute path
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relativePath = (string)value;

            string absolutePath = AppDomain.CurrentDomain.BaseDirectory + relativePath;

            return absolutePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
