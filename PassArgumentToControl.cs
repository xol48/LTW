using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BatchRename
{
    public abstract class PassArgumentToControl : UserControl
    {

        public event TypeEventHandler.NewArgumentEventHandler NewArgumentEvent;

        /// <summary>
        /// notify parent class
        /// </summary>
        /// <param name="arguments"></param>
        protected void RaiseEventHandler(List<string> arguments)
        {
            NewArgumentEvent?.Invoke(arguments);
        }

        /// <summary>
        /// clear all arguments
        /// </summary>
        virtual public void Refresh() { }

    }
}
