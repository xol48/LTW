using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    class AddCounterRule : Rules
    {
        public string ClassName => "AddCounterRule";

        public List<string> KeyWord
        {
            get
            {
                List<string> result = new List<string>();
                result.Add(ClassName);
                result.Add(Start);
                result.Add(Step);
                result.Add(Number);


                return result;
            }
        }

        public string Description => $"Addcounter with start '{Start}' step {Step} with {Number} digit";

        public Rules Create(List<string> arguments)
        {
            Rules result = null;
            if (arguments != null && arguments.Count >= 3)
            {
                result = new AddCounterRule()
                {
                    Start = arguments[0],
                    Step = arguments[1],
                    Number=arguments[2],

                };
            }

            return result;
        }

        public string Process(string inputString, bool isFilename, ref int now)
        {
            // get arguments
            int start = int.Parse(this.Start);
            int step = int.Parse(this.Step);
            int number = int.Parse(this.Number);
            


            // split name and extension
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }

            // process
            
            if(now==-1)
            {
                now = start;
                string a= Convert.ToString(now);
                string b = "";
                for(int i=0;i<number-a.Length;i++)
                {
                    b += "0";
                }
                b += a;
                name += b;
               
            }
            else
            {
                now += step;
                string a = Convert.ToString(now);
                string b = "";
                for (int i = 0; i < number - a.Length; i++)
                {
                    b += "0";
                }
                b += a;
                name += b;
            }



            // conbine and return
            string result = name + extension;
            return result;
        }



        public string Start { get; set; } = "";


        public string Step { get; set; } = "";

        public string Number { get; set; } = "";





    }
}
