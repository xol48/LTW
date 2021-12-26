using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp2
{
    class ReplaceRule : Rules
    {
        public string ClassName => "ReplaceRule";

        public List<string> KeyWord
        {
            get
            {
                List<string> result = new List<string>();
                result.Add(ClassName);
                result.Add(Needle);
                result.Add(Hammer);
                result.Add(Area);

                return result;
            }
        }

        public string Description => $"Replace '{Needle}' with '{Hammer}' in {Area}";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 3)
            {
                result = new ReplaceRule()
                {
                    Needle = arguments[0],
                    Hammer = arguments[1],
                    Area = arguments[2]
                };
            }

            return result;
        }

        public string Process(string inputString, bool isFilename)
        {
            // get arguments
            string needle = this.Needle;
            string hammer = this.Hammer;
            string area = this.Area;

            // split name and extension
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }

            // process
            if (isFilename && area == ReplaceRule.ExtensionArea)
            {
                extension = extension.Replace(needle, hammer);
            }
            else
            {
                name = name.Replace(needle, hammer);
            }

            string result = name + extension;
            return result;
        }

        public string Needle { get; set; } = "";
        public string Hammer { get; set; } = "";


        public string Area { get; set; } = ReplaceRule.NameArea;


        public static string NameArea => "Name";

        public static string ExtensionArea => "Extension";
    }
}

