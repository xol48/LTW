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
using Serilog;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // list of actions with arguments provided and supposed to be performed
        BindingList<Rules> addList;

        // list of actions displayed to user to select and modify arguments
        BindingList<Rule_Control> actionList;

        // list of filenames added to listview
        BindingList<Filename> filenameList;

        // list of foldernames added to listview
        BindingList<Foldername> foldernameList;

        // option when name collision, default is make new name, it can be modified in Option child window
        int NameCollisionOption = OptionsWindow.NewName;

        // list of images which located in 'Images' folder


        public MainWindow()
        {

            InitializeComponent();


            // create log file
            Log.Logger = new LoggerConfiguration().
                                 MinimumLevel.Debug().
                                 WriteTo.File("logs\\demo.txt", rollingInterval: RollingInterval.Day).
                                 CreateLogger();

            Log.Information("MainWindow: Init");


            // main grid binds to this class
            MainGrid.DataContext = this;



            // list of possible actions for user to select
            // when user configured arguments, a new action instance created and send to main window
            actionList = new BindingList<Rule_Control>
            {
                new Rule_Control(new AddPrefixRule(){ }, new AddPrefixControl()),
                new Rule_Control(new AddSuffixRule(){ }, new AddSuffixControl()),
                new Rule_Control(new PascalCaseRule(){ }, new PascalCaseNameControl()),
                new Rule_Control(new RemoveSpaceRule(){ }, new RemoveSpaceControl()),
                new Rule_Control(new ReplaceRule(){ }, new ReplaceControl()),
                new Rule_Control(new ChangeExtensionRule(){ }, new ChangeExtensionControl()),
                new Rule_Control(new LC_RS_Rule(){ }, new LC_RS_Control()),
            };
            // add event handler when new action comes
            foreach (var item in actionList)
            {
                item.NewActionEvent += AddNewActionToAddList;
            }
            // and a listview is binded to this list
            ActionsListView.ItemsSource = actionList;


            // list of instance actions which configured arguments
            addList = new BindingList<Rules>();
            // and a list is binded to this list
            AddListListView.ItemsSource = addList;

            
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
                Rule_Control actionGUI = ActionsListView.SelectedItem as Rule_Control;
                int result = actionGUI.ShowControlToGetArgument();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error("ExpandButton_Click: " + ex.Message);
            }
            
            
        }

        // if many action controls are opened, click on an action will make its control appears on top
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            Rule_Control actionGUI = ActionsListView.SelectedItem as Rule_Control;
            UserControlGrid.Children.Remove(actionGUI.Control);
            UserControlGrid.Children.Add(actionGUI.Control);

        }

        // this is event handler when new action comes
        public void AddNewActionToAddList(Rules action)
        {
            if (action != null)
            {
                addList.Add(action);
                Log.Information("AddNewActionToAddList: " + "receive an action successfully");
            }
            else
            {
                Log.Error("AddNewActionToAddList: " + "receive a null action");
            }
        }


        // remove an instance of action
        private void RemoveMenuContextAddList_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = AddListListView.SelectedItem as Rules;
            addList.Remove(selectedItem);
        }

        // open a preset
        private void OpenButtonAddList_click(object sender, RoutedEventArgs e)
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
                FileIO.ReadFromFile(filename, ref lines);

                // each line, parse arguments to corresponding action and create new action
                foreach (var line in lines)
                {
                    try
                    {
                        List<string> tokens = new List<string>();
                        if (FileIO.SplitString(line, ref tokens) == 0)
                        {
                            string actionName = tokens[0];
                            tokens.Remove(tokens[0]);
                            List<string> actionArguments = tokens;

                            var newAction = Helper.CreateAction(actionName, actionArguments);
                            addList.Add(newAction);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warning("OpenButtonAddList_click: " + "FileIO exception: " + ex.Message);
                    }
                }
            }
            
        }

        // save a preset
        private void SaveButtonAddList_Click(object sender, RoutedEventArgs e)
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
            FileIO.WriteToFile(filename, new List<string>() { "" });

            // each action will be append to file
            foreach (var action in addList)
            {
                StringBuilder builder = new StringBuilder();
                List<string> keywords = action.KeyWord;

                foreach (var item in keywords)
                {
                    builder.Append(item);
                    builder.Append(" ");
                }
                string line = builder.ToString();
                FileIO.AppendToFile(filename, new List<string>() { line });
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
            addList.Clear();

            // clear all filenames added
            filenameList.Clear();

            // clear all folderames added
            foldernameList.Clear();

            // clear action models and their controls
            foreach (var item in actionList)
            {
                item.Clear();
            }
        }

        // show information about application
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            string message = "An project from Window Programming Course\n"
                + "Performed by 1753102 - Bui Quang Thang\n"
                + "Contact: 1753102@student.hcmus.edu.vn";

            string caption = "Batch Rename Information";

            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;

            // Show message box
            MessageBoxResult result = MessageBox.Show(message, caption, buttons, icon);
        }



        // preview new filename before renaming
        private void PreviewFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("PreviewFolderButton_Click: Init");

            foreach (var foldername in foldernameList)
            {
                // each foldername in list
                string newFoldername = foldername.Value;

                // apply all action to that foldername
                foreach (var action in addList)
                {
                    try
                    {
                        newFoldername = action.Process(newFoldername, false);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Preview Foldername Exception: " + ex.Message);
                        Log.Warning("PreviewFolderButton_Click: " + ex.Message);
                    }
                }


                foldername.NewFoldername = newFoldername;
                foldername.ClearState();
            }

            Log.Information("PreviewFolderButton_Click: End");
        }

        // preview new foldername before renaming
        private void PreviewFileButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("PreviewFileButton_Click: Init");

            foreach (var filename in filenameList)
            {
                // each filename
                string newFilename = filename.Value;

                // apply all actions to
                foreach (var action in addList)
                {
                    try
                    {
                        newFilename = action.Process(newFilename, true);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Preview Foldername Exception: " + ex.Message);
                        Log.Warning("PreviewFileButton_Click: " + ex.Message);
                    }
                }


                filename.NewFilename = newFilename;
                filename.ClearState();
            }

            Log.Information("PreviewFileButton_Click: End");
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
                Log.Information("BatchFileButton_Click: Init");

                foreach (var filename in filenameList)
                {
                    // each filename
                    string newFilename = filename.Value;
                    filename.BatchState = "Success";
                    filename.FailedActions = "Failed Actions List:\n";
                    bool isSuccess = true;

                    // apply all actions to
                    foreach (var action in addList)
                    {
                        try
                        {
                            newFilename = action.Process(newFilename, true);
                        }
                        catch (Exception ex)
                        {
                            isSuccess = false;
                            filename.FailedActions += action.Description + "\n";

                            //MessageBox.Show("Preview Filename Exception: " + ex.Message);
                            Log.Error("BatchFileButton_Click: " + ex.Message);
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
                            
                            if (NameCollisionOption == OptionsWindow.NewName)
                            {
                                // make new filename, ex: hello.txt => hello - 1.txt
                                int count = 1;
                                string sample = newFilename;
                                while (File.Exists(newFilename))
                                {
                                    string difference = " - " + count.ToString();
                                  
                                    string extension = System.IO.Path.GetExtension(sample);
                                    newFilename= sample.Insert(sample.Length - extension.Length, difference);
                                    count++;
                                }

                                // rename
                                System.IO.File.Move(oldFilename, newFilename);
                            }
                            else if (NameCollisionOption == OptionsWindow.Skip)
                            {
                                // do nothing
                            }
                        }
                        else // if filename doesnot exist
                        {
                            System.IO.File.Move(oldFilename, newFilename);
                        }

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(newFilename + "\n" + ex.Message);
                        Log.Error("BatchFileButton_Click: " + ex.Message);
                    }
                }

                Log.Information("BatchFileButton_Click: End");
            }

        }

        // rename foldername
        private void BatchFolderButton_Click(object sender, RoutedEventArgs e)
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
                Log.Information("BatchFolderButton_Click: Init");

                foreach (var foldername in foldernameList)
                {
                    // each foldername
                    string newFoldername = foldername.Value;
                    foldername.BatchState = "Success";
                    foldername.FailedActions = "Failed Actions List:\n";
                    bool isSuccess = true;

                    // apply all actions to
                    foreach (var action in addList)
                    {
                        try
                        {
                            newFoldername = action.Process(newFoldername, false);
                        }
                        catch (Exception ex)
                        {
                            isSuccess = false;
                            foldername.FailedActions += action.Description + "\n";

                            //MessageBox.Show("Preview Foldername Exception: " + ex.Message);
                            Log.Error("BatchFolderButton_Click: " + ex.Message);
                        }
                    }


                    if (!isSuccess)
                    {
                        foldername.BatchState = "Fail";
                    }

                    // after getting new foldername, we rename foldername
                    foldername.NewFoldername = newFoldername;
                    try
                    {
                        string oldFoldername = foldername.Path + foldername.Value;
                        newFoldername = foldername.Path + foldername.NewFoldername;

                        // if foldername already existed
                        if (newFoldername.ToLower() != oldFoldername.ToLower() 
                            && Directory.Exists(newFoldername))
                        {
                            
                            if (NameCollisionOption == OptionsWindow.NewName)
                            {
                                // make new foldername
                                int count = 1;
                                string sample = newFoldername;
                                while (Directory.Exists(newFoldername))
                                {
                                    string difference = " - " + count.ToString();
                                    newFoldername = sample.Insert(sample.Length, difference);
                                    count++;
                                }

                                // rename
				// generate a GUID
            			Guid g = Guid.NewGuid();
            			string guidString = g.ToString();
				string oldFoldernameGUID = foldername.Path + guidString;
				
                                System.IO.Directory.Move(oldFoldername, oldFoldernameGUID);
				System.IO.Directory.Move(oldFoldernameGUID, newFoldername);
                            }
                            else if (NameCollisionOption == OptionsWindow.Skip)
                            {
                                // do nothing
                            }
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
                        //MessageBox.Show(ex.Message);
                        Log.Error("BatchFolderButton_Click: " + ex.Message);
                    }
                }

                Log.Information("BatchFolderButton_Click: End");
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

       
        // remove all actions from action added list
        private void RemoveAllActions_Click(object sender, RoutedEventArgs e)
        {
            addList.Clear();
        }


        // open option child window
        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            // create new option window
            OptionsWindow childOptionsWindow = new OptionsWindow();
            bool? result = childOptionsWindow.ShowDialog();

            if (result == true)
            {
                // get option selected
                this.NameCollisionOption = childOptionsWindow.NameCollisionOption;
            }
        }

        // after we rename some files in a path, we need to refresh all filenames in that path
        private void RefreshFiles_Click(object sender, RoutedEventArgs e)
        {
            if (filenameList != null && filenameList.Count > 0)
            {
                string path = filenameList[0].Path;
                filenameList.Clear();

                try
                {
                    string[] filenames = Directory.GetFiles(path);

                    foreach (var filename in filenames)
                    {
                        string newFilename = filename.Remove(0, path.Length);
                        filenameList.Add(new Filename() { Value = newFilename, Path = path });
                    }
                }
                catch (Exception) { }
            }
        }

        // after we rename some folders in a path, we need to refresh all foldernames in that path
        private void RefreshFolders_Click(object sender, RoutedEventArgs e)
        {
            if (foldernameList != null && foldernameList.Count > 0)
            {
                string path = foldernameList[0].Path;
                foldernameList.Clear();

                try
                {
                    string[] foldernames = Directory.GetDirectories(path);

                    foreach (var foldername in foldernames)
                    {
                        string newFoldername = foldername.Remove(0, path.Length);
                        foldernameList.Add(new Foldername() { Value = newFoldername, Path = path });
                    }
                }
                catch (Exception) { }

            }
        }
    }
}
