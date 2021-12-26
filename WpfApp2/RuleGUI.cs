using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WpfApp2
{
    /// <summary>
    /// class ActionGUI corresponds for a action button in list view
    /// this class contains: an action to process string, a control to receive arguments
    /// </summary>
    class RuleGUI : INotifyPropertyChanged
    {
        /// <summary>
        /// Only one constructor, pass an action and a corresponding control
        /// </summary>
        /// <param name="action"> action to process string</param>
        /// <param name="control"> control to receive arguments</param>
        public RuleGUI(Rules action, ArgumentForStringActionControl control)
        {
            // control
            this.Control = control;
            this.Control.NewArgumentEvent += CreateAction;
            this.Control.Visibility = Visibility.Hidden;

            // control expand
            ExpandState = "+";
            NumberClicked = 0;

            // action registered to class helper
            this.Action = action;

        }


        // property
        public Rules Action { get; }
        public ArgumentForStringActionControl Control { get; }
        public string Description => Action.ClassName;




        /// <summary>
        /// when control send arguments, an action created and sent to main program
        /// </summary>
        /// <param name="arguments"></param>
        protected void CreateAction(List<string> arguments)
        {
            try
            {
                Rules result = Action.Create(arguments);
                if (result != null)
                {
                    RaiseEventHandler(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error(ex.Message);
            }
        }

        public event TypeEventHandler.NewActionEventHandler NewActionEvent;

        protected void RaiseEventHandler(Rules action)
        {

            NewActionEvent?.Invoke(action);

        }


        // expand GUI
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseEventHandler(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private string expandState;
        public string ExpandState
        {
            get => expandState;
            set
            {
                expandState = value;
                RaiseEventHandler("ExpandState");
            }
        }

        public int NumberClicked { get; set; }

        /// <summary>
        /// show control to receive arguments from user
        /// </summary>
        /// <returns></returns>
        public int ShowControlToGetArgument()
        {
            NumberClicked++;
            if (NumberClicked % 2 == 1)
            {
                ExpandState = "-";
                Control.Visibility = Visibility.Visible;

            }
            else
            {
                ExpandState = "+";
                Control.Visibility = Visibility.Hidden;

            }

            return NumberClicked;
        }


        /// <summary>
        /// close control and clear its arguments
        /// </summary>
        public void Clear()
        {
            NumberClicked = 0;
            ExpandState = "+";
            Control.Clear();
            Control.Visibility = Visibility.Hidden;

        }
    }



    /// <summary>
    /// class Helper helps creating an instance of action through action name and list of arguments
    /// usually used to create action from text file
    /// it keeps list of possible actions when actions added to ActionGUI
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// list of possible actions
        /// </summary>
        public static List<Rules> actions = new List<Rules>();

        /// <summary>
        /// create action by text
        /// </summary>
        /// <param name="actionName"> name of action</param>
        /// <param name="arguments"> arguments for that action</param>
        /// <returns></returns>
        public static Rules CreateAction(string actionName, List<string> arguments)
        {
            Rules result = null;
            foreach (var action in actions)
            {
                if (actionName == action.ClassName)
                {
                    result = action.Create(arguments);
                }
            }

            return result;
        }
    }














}
