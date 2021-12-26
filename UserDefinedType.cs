using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    /// <summary>
    /// string implemented INotifyPropertyChanged
    /// </summary>
    public class MyString : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseEventHandler(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string value;
        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                RaiseEventHandler("Value");
            }
        }
    }

    /// <summary>
    /// string filename implemented INotifyPropertyChanged, used for Batch Rename 
    /// </summary>
    public class Filename : MyString
    {
        private string path;
        public string Path
        {
            get => path;
            set
            {
                path = value;
                RaiseEventHandler("Path");
            }
        }

        private string newFilename;
        public string NewFilename
        {
            get => newFilename;
            set
            {
                newFilename = value;
                RaiseEventHandler("NewFileName");
            }
        }

        private string batchState;
        public string BatchState
        {
            get => batchState;
            set
            {
                batchState = value;
                RaiseEventHandler("BatchState");
            }
        }


        public string FailedActions { get; set; } = "";

        public void ClearState()
        {
            BatchState = "";
            FailedActions = "";
        }
    }

    /// <summary>
    /// string foldername implemented INotifyPropertyChanged, used for Batch Rename
    /// </summary>
    public class Foldername : MyString
    {
        private string path;
        public string Path
        {
            get => path;
            set
            {
                path = value;
                RaiseEventHandler("Path");
            }
        }

        private string newFoldername;
        public string NewFoldername
        {
            get => newFoldername;
            set
            {
                newFoldername = value;
                RaiseEventHandler("NewFoldername");
            }
        }


        private string batchState;
        public string BatchState
        {
            get => batchState;
            set
            {
                batchState = value;
                RaiseEventHandler("BatchState");
            }
        }


        public string FailedActions { get; set; } = "";

        public void ClearState()
        {
            BatchState = "";
            FailedActions = "";
        }
    }


    /// <summary>
    /// user defined event type
    /// </summary>
    public static class TypeEventHandler
    {
        /// <summary>
        /// event when new argument comes
        /// </summary>
        /// <param name="arguments"></param>
        public delegate void NewArgumentEventHandler(List<string> arguments);

        /// <summary>
        /// event when new instance of action comes
        /// </summary>
        /// <param name="action"></param>
        public delegate void NewActionEventHandler(Rules action);
    }
}
