using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Collections.Generic;

class FileDownloader
{
    /*pdf URls examples
     * https://www.tutorialspoint.com/csharp/csharp_tutorial.pdf
     * https://www.lkouniv.ac.in/site/writereaddata/siteContent/202004261306373620rohit_engg_multi_threaded.pdf
     * https://www4.comp.polyu.edu.hk/~csajaykr/myhome/teaching/eel602/MT.pdf
     * https://web.stanford.edu/class/cs110/lectures/cs110-win2122-lecture-13.pdf
     * https://horstmann.com/corejava/cj7v2ch1.pdf
     */

    static void Main(string[] args)
    {
        List<string> urls = new List<string>();
        string input;

        Console.WriteLine("Enter URLs to download files (type 'done' to finish):");

        while (true)
        {
            input = Console.ReadLine();
            if (input.ToLower() == "done")
                break;
            if (!string.IsNullOrEmpty(input))
                urls.Add(input);
        }

        if (urls.Count == 0)
        {
            Console.WriteLine("No URLs provided. Exiting.");
            return;
        }

        string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        Thread[] downloadThreads = new Thread[urls.Count];

        for (int i = 0; i < urls.Count; i++)
        {
            int index = i;
            downloadThreads[i] = new Thread(() => DownloadFile(urls[index], downloadsPath));
            downloadThreads[i].Start();
        }

        foreach (Thread t in downloadThreads)
        {
            t.Join();
        }

        Console.WriteLine("All files have been downloaded.");
    }

    static void DownloadFile(string url, string downloadsPath)
    {
        Console.WriteLine($"Starting download: {url}");
        try
        {
            using (WebClient client = new WebClient())
            {
                string fileName = Path.GetFileName(url);
                string destinationPath = Path.Combine(downloadsPath, fileName);

                client.DownloadFile(url, destinationPath);

                Console.WriteLine($"Download completed: {url} -> {destinationPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading {url}: {ex.Message}");
        }
    }
}
