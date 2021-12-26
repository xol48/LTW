using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class AddcounterRule : Rules
    {
        public string ClassName => "AddcounterRule";

        public List<string> KeyWord
        {
            get
            {
                List<string> result = new List<string>();
                result.Add(ClassName);
                result.Add(Start);
                result.Add(Step);


                return result;
            }
        }
        public string Description => $"Addcounter with start '{Start}' step {Step}";
        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 2)
            {
                result = new AddcounterRule()
                {
                    Start = arguments[0],
                    Step = arguments[1],

                };
            }

            return result;
        }
        public string Process(string inputString, bool isFilename)
        {
            // get arguments
            string start = this.Start;
            string step = this.Step;
          // split name and extension
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }
            // process
            // conbine and return
            string result = name + extension;
            return result;
        }
        public string Start { get; set; } = "";
        public string Step { get; set; } = "";
    }
}
