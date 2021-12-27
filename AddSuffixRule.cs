using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class AddSuffixRule:Rules
    {
        public string ClassName => "AddSuffixRule";

        public List<string> KeyWord
        {
            get
            {
                List<string> result = new List<string>();
                result.Add(ClassName);
                result.Add(Suffix);
                return result;
            }
        }

        public string Description => $"Add suffix string '{Suffix}' ";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 1)
            {
                result = new AddSuffixRule()
                {
                    Suffix = arguments[0]

                };
            }

            return result;
        }

        public string Process(string inputString, bool isFilename, ref int now)
        {
            string suffix = this.Suffix;
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }

            name = name + suffix;

            string result = name + extension;
            return result;
        }

        public string Suffix { get; set; } = "";

    }
}
