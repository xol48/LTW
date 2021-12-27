using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class ReplaceRule:Rules
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
                return result;
            }
        }

        public string Description => $"Replace '{Needle}' with '{Hammer}'";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 2)
            {
                result = new ReplaceRule()
                {
                    Needle = arguments[0],
                    Hammer = arguments[1],
                };
            }

            return result;
        }

        public string Process(string inputString, bool isFilename, ref int now)
        {
            // get arguments
            string needle = this.Needle;
            string hammer = this.Hammer;

            // split name and extension
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }

            // process

                name = name.Replace(needle, hammer);
            

            string result = name + extension;
            return result;
        }

        public string Needle { get; set; } = "";
        public string Hammer { get; set; } = "";


    }
}
