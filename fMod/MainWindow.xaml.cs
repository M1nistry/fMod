using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using fMod.JSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fMod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((TabItem)e.AddedItems[0]).Header.ToString())
            {
                case ("Browse Mods"):
                    //var webString = new WebClient().DownloadString("http://api.factoriomods.com/mods");
                    //var result = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RootObject[]>(webString));
                    //Console.WriteLine(result.Result[0].description);
                    PopulateMods();
                    break;
            }
        }

        private void PopulateMods()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = this.cancellationTokenSource.Token;
            var progressReporter = new ProgressReporter();

            var task = Task.Factory.StartNew(() =>
            {
                var webString = new WebClient().DownloadString("http://api.factoriomods.com/mods");
                var result = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<RootObject[]>(webString), cancellationToken);


                // After all that work, cause the error if requested.
                if (false)
                {
                    throw new InvalidOperationException("Oops...");
                }

                // The answer, at last! 
                return result.Result;
            }, cancellationToken);

            // ProgressReporter can be used to report successful completion,
            //  cancelation, or failure to the UI thread. 
            progressReporter.RegisterContinuation((Task) task, () =>
            {

                // Display results.
                if (task.Exception != null)
                    MessageBox.Show("Background task error: " + task.Exception.ToString());
                else if (task.IsCanceled)
                    MessageBox.Show("Background task cancelled");
                else
                {
                    var result = task.Result;
                    foreach (var mod in result)
                    {
                        var newRow = new ExpandableRow();
                        newRow.DataContext = mod;
                        newRow.Margin = new Thickness(2, 2, 2, 2);
                        StackPanelMods.Children.Add(newRow);
                    }
                    
                }
            });
        }

    }
}
