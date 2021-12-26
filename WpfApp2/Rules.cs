using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public interface Rules
    {
        string ClassName { get; }

        List<string> KeyWord { get; }

        string Description { get; }

        string Process(string inputString, bool isFilename);

        Rules Create(List<string> arguments);
    }
}
