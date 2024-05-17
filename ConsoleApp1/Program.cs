using System;
using System.Threading;

class FileDownloader
{
    static void Main(string[] args)
    {
        // URLs of files to download (using dummy URLs for example)
        string[] urls = {
            "http://example.com/file1",
            "http://example.com/file2",
            "http://example.com/file3",
            "http://example.com/file4"
        };

        // Array of threads for file downloading
        Thread[] downloadThreads = new Thread[urls.Length];

        // Starting threads for file downloads
        for (int i = 0; i < urls.Length; i++)
        {
            int index = i; // Local variable for closure
            downloadThreads[i] = new Thread(() => DownloadFile(urls[index]));
            downloadThreads[i].Start();
        }

        // Waiting for all threads to complete
        foreach (Thread t in downloadThreads)
        {
            t.Join();
        }

        Console.WriteLine("All files have been downloaded.");
    }

    static void DownloadFile(string url)
    {
        Console.WriteLine($"Starting download: {url}");
        // Simulate file download time
        Thread.Sleep(new Random().Next(2000, 5000));
        Console.WriteLine($"Download completed: {url}");
    }
}
