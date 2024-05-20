using System;
using System.Threading;

class FileDownloader
{
    static void Main(string[] args)
    {
        string[] urls = {
            "http://example.com/file1",
            "http://example.com/file2",
            "http://example.com/file3",
            "http://example.com/file4"
        };

        Thread[] downloadThreads = new Thread[urls.Length];
        
        for (int i = 0; i < urls.Length; i++)
        {
            int index = i;
            downloadThreads[i] = new Thread(() => DownloadFile(urls[index]));
            downloadThreads[i].Start();
        }

        foreach (Thread t in downloadThreads)
        {
            t.Join();
        }

        Console.WriteLine("All files have been downloaded.");
    }

    static void DownloadFile(string url)
    {
        Console.WriteLine($"Starting download: {url}");
        Thread.Sleep(new Random().Next(2000, 5000));
        Console.WriteLine($"Download completed: {url}");
    }
}
