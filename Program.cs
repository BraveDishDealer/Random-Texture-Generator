using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

public class Program
{
    public static string output_folder = "resources/pack/assets/minecraft/textures/";
    public static string default_folder = "resources/default/textures/";
    public static string zip_output = "output/";
    public static bool loading;

    public static void Main()
    {
        Console.WriteLine("Loading...");
        System.Threading.Thread.Sleep(4000);
        BlockRandomization();
    }

    public static void Update()
    {
        if (loading) Console.WriteLine("");
    }

    public static IList<string> searchImages(string path)
    {
        var matches = new List<string>();

        var images = System.IO.Directory.GetFiles(path);

        foreach (var image in images)
        {
            if (image.Contains(".png"))
            {
                matches.Add(image);
            }
        }

        return matches;
    }

    public static void BlockRandomization()
    {
        //find png files from the default folder
        string copy = default_folder + "block/";
        string output = output_folder + "block/";
        List<string> files_to_copy = new List<string>();
        foreach (string i in searchImages(copy)) files_to_copy.Add(i);

        //clear the output folder
        foreach (string file in Directory.GetFiles(output)) File.Delete(file);

        //the filenames list
        List<string> files = new List<string>();
        foreach (string i in files_to_copy)
        {
            files.Add(Path.GetFileName(i));
            Console.WriteLine(i);
        }

        //random number
        Random rnd = new Random();

        //copying
        foreach (string i in files_to_copy)
        {
            int rand = rnd.Next(0, files.Count);
            File.Copy(i, output + files[rand]);
            files.Remove(files[rand]);
        }

        //zipping
        foreach (string file in Directory.GetFiles(zip_output))
        {
            File.Delete(file);
        }
        Console.WriteLine("Build Done! This window will automatically close. " + DateTime.Now);
        ZipFile.CreateFromDirectory(Directory.GetCurrentDirectory() + "/resources/pack", zip_output + "build-" + DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year + "-.zip");
    }
}