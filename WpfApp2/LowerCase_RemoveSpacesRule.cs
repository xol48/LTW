using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class LowerCase_RemoveSpacesRule:Rules
    {
        public string ClassName => "LowerCase_RemoveSpacesRule";

        public List<string> KeyWord =>
            new List<string>() { ClassName};

        public string Description =>
            $"Lower Case & Remove All Spaces";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 1)
            {
                result = new LowerCase_RemoveSpacesRule()
                {
                };

            }

            return result;
        }

        public string Process(string inputString, bool isFilename)
        {


            // split
            string input = inputString;
            string extension = "";
            if (isFilename)
            {
                input = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, input.Length);
            }


            return input.ToLower().Replace(" ", "")+extension;
        }

    }
}
