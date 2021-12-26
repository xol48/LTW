using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class RemoveSpaceRule:Rules
    {
        public string ClassName => "RemoveSpaces";

        public List<string> KeyWord =>
            new List<string>() { ClassName };

        public string Description =>
            $"Remove Spaces From Beginning & Ending";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null )
            {
                result = new RemoveSpaceRule ()
                {
                };

            }

            return result;
        }

        public string Process(string inputString, bool isFilename)
        {


            // split
            string input ="";
            string extension = "";
            if (isFilename)
            {
                input = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, input.Length);
            }
            string result = "";
            int start;
            int end;
            for (start=0;start<input.Length;start++)
            {
                if(input[start]!=' ')
                {
                    break;
                }
            }
            for (end = input.Length-1; end>=0; end--)
            {
                if (input[end] != ' ')
                {
                    break;
                }
            }

            return input.Substring(start,end-start)+extension;
        }
    }
}
