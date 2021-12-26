using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class FileIO
    {
        /// <summary>
        /// write multiple lines to file
        /// </summary>
        /// <param name="filename">name of file</param>
        /// <param name="lines">list of lines to be written</param>
        /// <returns>0 if success</returns>
        static public int WriteToFile(string filename, List<string> lines)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {

                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }

            }

            return 0;
        }

        /// <summary>
        /// append multiple lines to file 
        /// </summary>
        /// <param name="filename">name of file</param>
        /// <param name="lines">mutiple lines</param>
        /// <returns>0 if success</returns>
        static public int AppendToFile(string filename, List<string> lines)
        {
            using (StreamWriter sw = new StreamWriter(filename, append:true))
            {

                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }

            }

            return 0;
        }

        /// <summary>
        /// read all lines from file
        /// </summary>
        /// <param name="filename">name of file</param>
        /// <param name="strings">list of lines</param>
        /// <returns>0 if success</returns>
        static public int ReadFromFile(string filename, ref List<string> lines)
        {

            using (StreamReader sr = new StreamReader(filename))
            {
                string line;

                // Read and display lines from the file until 
                // the end of the file is reached. 
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return 0;
        }

        /// <summary>
        /// split a string by space and comma
        /// </summary>
        /// <param name="hayStack">string to be splitted</param>
        /// <param name="outputList">list of tokens</param>
        /// <returns>0 if success</returns>
        static public int SplitString(string hayStack, ref List<string> outputList)
        {
            // split
            string[] tokens = hayStack.Split(
                new string[] { " ", "," },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                outputList.Add(token);
            }
            return 0;
        }
        
    }

    


}
