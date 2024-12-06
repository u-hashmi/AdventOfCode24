﻿namespace Helpers;

public static class FileReader
{
    public static string[] ReadFromFile(this FilePathRecord filePath)
    {
        try
        {
            return File.ReadAllLines(filePath.FilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return Array.Empty<string>();
        }
    }
}