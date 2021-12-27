using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace BatchRename
{
    public partial class MainWindow : Window
    {
        // list of actions with arguments provided and supposed to be performed
        BindingList<Rules> choiceList;

        // list of actions displayed to user to select and modify arguments
        BindingList<Rule_Control> ruleList;

        // list of filenames added to listview
        BindingList<Filename> filenameList;

        // list of foldernames added to listview
        BindingList<Foldername> foldernameList;


        // list of images which located in 'Images' folder


        public MainWindow()
        {

            InitializeComponent();


            // create log file



            // main grid binds to this class
            MainGrid.DataContext = this;



            // list of possible actions for user to select
            // when user configured arguments, a new action instance created and send to main window
            ruleList = new BindingList<Rule_Control>
            {
                new Rule_Control(new AddPrefixRule(){ }, new AddPrefixControl()),
                new Rule_Control(new AddSuffixRule(){ }, new AddSuffixControl()),
                new Rule_Control(new PascalCaseRule(){ }, new PascalCaseNameControl()),
                new Rule_Control(new RemoveSpaceRule(){ }, new RemoveSpaceControl()),
                new Rule_Control(new ReplaceRule(){ }, new ReplaceControl()),
                new Rule_Control(new ChangeExtensionRule(){ }, new ChangeExtensionControl()),
                new Rule_Control(new LC_RS_Rule(){ }, new LC_RS_Control()),
                new Rule_Control(new AddCounterRule(){ }, new AddCounterControl()),
            };
            // add event handler when new action comes
            foreach (var item in ruleList)
            {
                item.NewActionEvent += AddNewActionToAddList;
            }
            // and a listview is binded to this list
            RulesListView.ItemsSource = ruleList;


            // list of instance actions which configured arguments
            choiceList = new BindingList<Rules>();
            // and a list is binded to this list
            AddListListView.ItemsSource = choiceList;

            
            // a listview binded to list of filenames 
            filenameList = new BindingList<Filename>();
            FileListView.ItemsSource = filenameList;


            // a listview binded to list of foldernames
            foldernameList = new BindingList<Foldername>();
            FolderListView.ItemsSource = foldernameList;

        }


        // action GUI expand
        // user clicks expand button on an action, a control will be display to received arguments 
        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rule_Control RC = RulesListView.SelectedItem as Rule_Control;
                int result = RC.ShowControlToGetArgument();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        // if many action controls are opened, click on an action will make its control appears on top
        private void RuleButton_Click(object sender, RoutedEventArgs e)
        {
            Rule_Control RC = RulesListView.SelectedItem as Rule_Control;
            UserControlGrid.Children.Remove(RC.Control);
            UserControlGrid.Children.Add(RC.Control);

        }

        // this is event handler when new action comes
        public void AddNewActionToAddList(Rules rule)
        {
            if (rule != null)
            {
                choiceList.Add(rule);
            }
        }


        // remove an instance of action
        private void RemoveMenuContextAddList_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = AddListListView.SelectedItem as Rules;
            choiceList.Remove(selectedItem);
        }

        // open a preset
        private void OpenButtonChoiceList_click(object sender, RoutedEventArgs e)
        {
            // open file dialog
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog()
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // get filename
                string filename = openFileDialog1.FileName;
                
                // read all lines in filename
                List<string> lines = new List<string>();
 
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
                // each line, parse arguments to corresponding action and create new action
                foreach (var line in lines)
                {
                    try
                    {
                        List<string> tokens = new List<string>();
                        string[] s=line.Split(
                        new string[] { " ", "," },
                         StringSplitOptions.RemoveEmptyEntries);
                        foreach (var token in s)
                        {
                            tokens.Add(token);
                        }
                        string ruleName = tokens[0];
                            tokens.Remove(tokens[0]);
                            List<string> arguments = tokens;

                            var newRule = Helper.CreateAction(ruleName, arguments);
                            choiceList.Add(newRule);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            
        }

        // save a preset
        private void SaveButtonChoiceList_Click(object sender, RoutedEventArgs e)
        {
            // save file dialog
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog()
            {
                Title = "Save text Files",
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            // get filename
            string filename = "";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog.FileName;
            }


            // make file empty
            using (StreamWriter sw = new StreamWriter(filename))
            {

                    sw.WriteLine("");
            }

            // each action will be append to file
            foreach (var rule in choiceList)
            {
                StringBuilder builder = new StringBuilder();
                List<string> keywords = rule.KeyWord;

                foreach (var item in keywords)
                {
                    builder.Append(item);
                    builder.Append(" ");
                }
                string lines = builder.ToString();
                using (StreamWriter sw = new StreamWriter(filename, append: true))
                {

                    foreach (string line in new List<string>() { lines })
                    {
                        sw.WriteLine(line);
                    }

                }
            }

            
        }

        
        // add all filenames in particular folder
        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();

            // show dialog
            System.Windows.Forms.DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // get all filenames in path
                string path = folderDlg.SelectedPath + "\\";
                string[] filenames = Directory.GetFiles(path);

                // add all to filenameList
                foreach (var filename in filenames)
                {
                    string newFilename = filename.Remove(0, path.Length);
                    filenameList.Add(new Filename() { Value = newFilename, Path = path });
                }
            }


        }

        // add all foldernames in particular folder
        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();

            // show dialog
            System.Windows.Forms.DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // get all foldernames
                string path = folderDlg.SelectedPath + "\\";
                string[] foldernames = Directory.GetDirectories(path);

                // add all to foldername list
                foreach (var foldername in foldernames)
                {
                    string newFoldername = foldername.Remove(0, path.Length);
                    foldernameList.Add(new Foldername() { Value = newFoldername, Path = path });
                }
            }


        }



        // refresh all things 
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // clear instances of actions
            choiceList.Clear();

            // clear all filenames added
            filenameList.Clear();

            // clear all folderames added
            foldernameList.Clear();

            // clear action models and their controls
            foreach (var item in ruleList)
            {
                item.Clear();
            }
        }





        // preview new filename before renaming
        private void PreviewFolderButton_Click(object sender, RoutedEventArgs e)
        {
            int now = -1;
            foreach (var foldername in foldernameList)
            {
                // each foldername in list
                string newFoldername = foldername.Value;

                // apply all action to that foldername
                foreach (var rule in choiceList)
                {
                    try
                    {
                        newFoldername = rule.Process(newFoldername, false,ref now);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show( ex.Message);
                    }
                }


                foldername.NewFoldername = newFoldername;
                foldername.ClearState();
            }

        }

        // preview new foldername before renaming
        private void PreviewFileButton_Click(object sender, RoutedEventArgs e)
        {
            int now = -1;
            foreach (var filename in filenameList)
            {
                // each filename
                string newFilename = filename.Value;

                // apply all actions to
                foreach (var rule in choiceList)
                {
                    try
                    {
                        newFilename = rule.Process(newFilename, true,ref now);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show( ex.Message);
                    }
                }


                filename.NewFilename = newFilename;
                filename.ClearState();
            }

        }

        // rename filename
        private void BatchFileButton_Click(object sender, RoutedEventArgs e)
        {
            // show message box warns user about options selected such as: make new name or skip, ...
            string message = "Some final result will be different with preview result due to options\n" 
                + "Are you sure to continue?";
            string caption = "Warning";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(message, caption, buttons, icon);


           
            if (result == MessageBoxResult.OK)
            {
                int now = -1;
                foreach (var filename in filenameList)
                {
                    // each filename
                    string newFilename = filename.Value;
                    filename.BatchState = "Success";
                    filename.FailedActions = "Failed Actions List:\n";
                    bool isSuccess = true;

                    // apply all actions to
                    foreach (var rule in choiceList)
                    {
                        try
                        {
                            newFilename = rule.Process(newFilename, true,ref now);
                        }
                        catch (Exception ex)
                        {
                            isSuccess = false;
                            filename.FailedActions += rule.Description + "\n";
                            MessageBox.Show( ex.Message);
                        }
                    }

                    if (!isSuccess)
                    {
                        filename.BatchState = "Fail";
                    }

                    // after getting new filename, we rename filename
                    filename.NewFilename = newFilename;
                    try
                    {
                        string oldFilename = filename.Path + filename.Value;
                        newFilename = filename.Path + filename.NewFilename;

                        // if filename already existed
                        if (newFilename.ToLower() != oldFilename.ToLower()
                            && File.Exists(newFilename))
                        {
                            
      
           
                                int count = 1;
                                string sample = newFilename;
                                while (File.Exists(newFilename))
                                {
                                    string difference = " (" + count.ToString()+")";
                                  
                                    string extension = System.IO.Path.GetExtension(sample);
                                    newFilename= sample.Insert(sample.Length - extension.Length, difference);
                                    count++;
                                }

                                // rename
                                System.IO.File.Move(oldFilename, newFilename);
                        }
                        else // if filename doesnot exist
                        {
                            System.IO.File.Move(oldFilename, newFilename);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }

        }

        // rename foldername
        private void BatchFolderButton_Click(object sender, RoutedEventArgs e)
        {
            string message = "Some final result will be different with preview result due to options\n"
                + "Are you sure to continue?";
            string caption = "Warning";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(message, caption, buttons, icon);



            if (result == MessageBoxResult.OK)
            {
                int now = -1;
                foreach (var foldername in foldernameList)
                {
                    // each foldername
                    string newFoldername = foldername.Value;
                    foldername.BatchState = "Success";
                    foldername.FailedActions = "Failed Actions List:\n";
                    bool isSuccess = true;

                    // apply all actions to
                    foreach (var rule in choiceList)
                    {
                        try
                        {
                            newFoldername = rule.Process(newFoldername, false,ref now);
                        }
                        catch (Exception ex)
                        {
                            isSuccess = false;
                            foldername.FailedActions += rule.Description + "\n";

                            MessageBox.Show(ex.Message);
                        }
                    }


                    if (!isSuccess)
                    {
                        foldername.BatchState = "Fail";
                    }

                    foldername.NewFoldername = newFoldername;
                    try
                    {
                        string oldFoldername = foldername.Path + foldername.Value;
                        newFoldername = foldername.Path + foldername.NewFoldername;

                        // if foldername already existed
                        if (newFoldername.ToLower() != oldFoldername.ToLower() 
                            && Directory.Exists(newFoldername))
                        {
                            
                                // make new foldername
                                int count = 1;
                                string sample = newFoldername;
                                while (Directory.Exists(newFoldername))
                                {
                                    string difference = " (" + count.ToString()+")";
                                    newFoldername = sample.Insert(sample.Length, difference);
                                    count++;
                                }

				// generate a GUID
            			Guid g = Guid.NewGuid();
            			string guidString = g.ToString();
				string oldFoldernameGUID = foldername.Path + guidString;
				
                                System.IO.Directory.Move(oldFoldername, oldFoldernameGUID);
				System.IO.Directory.Move(oldFoldernameGUID, newFoldername);
                            }

                        
                        else // if foldername doesnot exist, just rename
                        {
                            //System.IO.Directory.Move(oldFoldername, newFoldername);

			    // rename
			    // generate a GUID
            		    Guid g = Guid.NewGuid();
            		    string guidString = g.ToString();
			    string oldFoldernameGUID = foldername.Path + guidString;
				
                            System.IO.Directory.Move(oldFoldername, oldFoldernameGUID);
			    System.IO.Directory.Move(oldFoldernameGUID, newFoldername);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }


        // remove a foldername from list
        private void RemoveMenuContextFolders_Click(object sender, RoutedEventArgs e)
        {
            var item = FolderListView.SelectedItem as Foldername;
            foldernameList.Remove(item);
        }

        // remove a filename from list
        private void RemoveMenuContextFiles_Click(object sender, RoutedEventArgs e)
        {
            var item = FileListView.SelectedItem as Filename;
            filenameList.Remove(item);
        }



    }
}
