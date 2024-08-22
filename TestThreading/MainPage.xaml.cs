using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using TestThreading.Model;
//using Newtonsoft.Json;

namespace TestThreading;

public sealed partial class MainPage : Page
{
    private BackgroundWorker worker = new BackgroundWorker();
    private DispatcherQueue dispatcherQueue => DispatcherQueue.GetForCurrentThread();
    public MainPage()
    {
        this.InitializeComponent();

        // Set the properties of the BackgroundWorker
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;

        // Handle the events of the BackgroundWorker
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

    }

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        // Update the UI with the progress on the main thread
        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        {
            //ProgressBarControl.Value = e.ProgressPercentage;
            //ProgressLabel.Text = $"{e.ProgressPercentage}%";
        });
    }

    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // Handle the completion or cancellation of the task on the main thread
        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        {
            if (e.Cancelled)
            {
                //StatusLabel.Text = "Cancelled";
                LogMessage("Cancelled");
            }
            else if (e.Error != null)
            {
                //StatusLabel.Text = "Error: " + e.Error.Message;
                LogMessage("Error: " + e.Error.Message);
            }
            else
            {
                //StatusLabel.Text = "Done";
                LogMessage("Done");
            }
        });
    }

    byte[] serialized = null;
    private async Task Serialize_Sync_Click(object sender, RoutedEventArgs e)
    {
        LogMessage("Synchronous serialisation");
        await Serialize();
        LogMessage("End Synchronous serialisation");
        //txtObject.Text = serialized.Length.ToString();
    }

    private async Task Deserialize_Sync_Click(object sender, RoutedEventArgs e)
    {
        //Deserialize

        await Deserialize(textBlock:stDeserializedSync);

    }
    private async Task Serialize(string fromThread = "")
    {
        var t1 = DateTime.Now;
        serialized = JsonSerializer.SerializeToUtf8Bytes(resultWithSummary);
        var t2 = DateTime.Now;
        LogMessage("Time taken in serialisation: " + fromThread + (t2 - t1).TotalMilliseconds + " milliseconds", stSerialized);

        //txtObject.Text = serialized.Length.ToString();

        await Task.CompletedTask;
    }
    private async Task Deserialize(string fromThread = "", TextBlock textBlock = null)
    {
        var t1 = DateTime.Now;
        var obj = JsonSerializer.Deserialize<Summary>(serialized);
        var t2 = DateTime.Now;
        LogMessage("Time taken in deserialisation: " + fromThread + (t2 - t1).TotalMilliseconds + " milliseconds", textBlock);
        LogMessage("Deserialised connotes: " + fromThread + obj.Items.Count());
        await Task.CompletedTask;
    }
    Summary resultWithSummary = null;
    private void Generate_Click(object sender, RoutedEventArgs e)
    {
        var items = new List<Items>();
        var code = "TSTAB";
        var numConnote = int.Parse(txtConnotes.Text);
        for (int i = 0; i < numConnote; i++)
        {
            items.Add(new Items { V5 = $"{code}{i.ToString().PadLeft(5, '0')}", V13 = "A00001", V21 = "TEST1", V22 = "TEST2" });
        }
        resultWithSummary = new Summary
        {
            Items = items,
            Total = new Total { V1 = 45000, V5 = 90000, V2 = 47000, V3 = 10000, V4 = 40000 }
        };
        LogMessage("Object generated", stGenerated);
    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        // Perform the task on a separate thread
        //for (int i = 0; i <= 100; i++)
        {
            // Check for cancellation
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                //break;
            }

            // Simulate some work
            //System.Threading.Thread.Sleep(100);
            Deserialize("(from Thread) ", stDeserializedAsync);

            // Report progress
            //worker.ReportProgress(i);
        }
    }
    private async Task Run()
    {
        LogMessage("Startup");
        //worker.d
        worker.RunWorkerAsync();
        await Task.CompletedTask;
        return;
    }

    private static void LogMessage(string message, TextBlock textBlock = null)
    {
        Console.WriteLine($"[tid:{Thread.CurrentThread.ManagedThreadId}] {message}");
        if (textBlock != null)
        {
            textBlock.Text = message;
        }
    }

    

    private async Task Deserialize_Async_Click(object sender, RoutedEventArgs e)
    {
        await Run();
    }
    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        stGenerated.Text = stSerialized.Text = stDeserializedSync.Text = stDeserializedAsync.Text = string.Empty;
        txtConnotes.Text = "5000";
    }
}
