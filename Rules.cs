using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public interface Rules
    {
        /// <summary>
        /// name of the class
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// list of keywords, useful when save action to file
        /// </summary>
        List<string> KeyWord { get; }

        /// <summary>
        /// description about the action
        /// </summary>
        string Description { get; }

        /// <summary>
        /// change a string to another one
        /// </summary>
        /// <param name="inputString">string to be changed</param>
        /// <param name="isFilename">is this string is a filename</param>
        /// <returns>string changed to</returns>
        string Process(string inputString, bool isFilename);

        /// <summary>
        /// self create with list of arguments
        /// </summary>
        /// <param name="arguments"> list of arguments</param>
        /// <returns>new created action</returns>
        Rules Create(List<string> arguments);

    }
}
