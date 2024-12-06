using System.Text.Json.Serialization;
using Day5_PrintQueue.Classes;

namespace Day5_PrintQueue;

// ReSharper disable CollectionNeverQueried.Local
public static class Program
{
    public static void Main()
    {
        const string printQueueFileName = "Day5_PrintQueue_Part2_Input.txt";
        var printSections = GetPrintSections(ReadFromFile(printQueueFileName));
        var validLines = GetCorrectRules(printSections);
        var invalidLines = GetIncorrectRules(printSections);
        var fixedLines = CorrectIncorrectUpdates(printSections, invalidLines);
        Console.WriteLine("Printing valid print updates:");
        PrintRules(validLines);
        Console.WriteLine("Printing invalid print updates:");
        PrintRules(invalidLines);
        Console.WriteLine("Printing fixed print updates:");
        PrintRules(fixedLines);
        Console.WriteLine($"Sum of valid print updates: {GetSumOfMiddles(validLines)}");
        Console.WriteLine($"Sum of fixed print updates: {GetSumOfMiddles(fixedLines)}");
    }

    private static string[] ReadFromFile(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return Array.Empty<string>();
        }
    }

    private static PrintSections GetPrintSections(string[] fileContents)
    {
        var printSections = new PrintSections();

        foreach (var line in fileContents)
        {
            //Split sections where empty line occurs
            if (line.Contains('|') && line.Trim() != string.Empty)
            {
                printSections.OrderingRulesSection.Add(line);
            }
            else
            {
                printSections.UpdatingRulesSection.Add(line);
            }
        }

        return printSections;
    }

    #region Rules

    //Rule 1 
    private static List<string> GetCorrectRules(PrintSections printSections)
    {
        if (printSections.OrderingRulesSection.Count == 0 || printSections.UpdatingRulesSection.Count == 0)
            return null!;
        
        return (
            from
                printRules in printSections.UpdatingRulesSection
            where
                printRules.Trim() != string.Empty
                let positions = printRules.Split(',').ToList()
                let allRulesValid = printSections.OrderingRulesSection.Select(printOrder => printOrder.Split("|"))
                .All(numbersSplit => !positions.Contains(numbersSplit[0]) || !positions.Contains(numbersSplit[1]) || positions.IndexOf(numbersSplit[0]) < positions.IndexOf(numbersSplit[1]))
            where allRulesValid
            select printRules).ToList();
    }
    
    private static List<string> GetIncorrectRules(PrintSections printSections)
    {
        if (printSections.OrderingRulesSection.Count == 0 || printSections.UpdatingRulesSection.Count == 0)
            return null!;
        
        return (
            from
                printRules in printSections.UpdatingRulesSection
            where
                printRules.Trim() != string.Empty
            let positions = printRules.Split(',').ToList()
            let allRulesValid = printSections.OrderingRulesSection.Select(printOrder => printOrder.Split("|"))
                .All(numbersSplit => !positions.Contains(numbersSplit[0]) || !positions.Contains(numbersSplit[1]) || positions.IndexOf(numbersSplit[0]) < positions.IndexOf(numbersSplit[1]))
            where !allRulesValid
            select printRules).ToList();
    }

    private static List<string> CorrectIncorrectUpdates(PrintSections printSections, List<string> invalidUpdateLines)
    {
        var correctedUpdates = new List<string>();

        foreach (var update in invalidUpdateLines)
        {
            // Split the update into positions
            var positions = update.Split(',').ToList();

            // Perform a topological sort based on the ordering rules
            var sortedPositions = TopologicalSort(positions, printSections.OrderingRulesSection);

            // Join the sorted positions back into a string
            correctedUpdates.Add(string.Join(",", sortedPositions));
        }

        return correctedUpdates;
    }
    
    private static List<string> TopologicalSort(List<string> positions, List<string> orderingRules)
    {
        // Parse ordering rules into a dependency graph
        var dependencyGraph = new Dictionary<string, List<string>>();
        var incomingEdges = new Dictionary<string, int>();

        foreach (var position in positions)
        {
            dependencyGraph[position] = new List<string>();
            incomingEdges[position] = 0;
        }

        foreach (var rule in orderingRules)
        {
            var splitRule = rule.Split('|');
            var before = splitRule[0];
            var after = splitRule[1];

            if (positions.Contains(before) && positions.Contains(after))
            {
                dependencyGraph[before].Add(after);
                incomingEdges[after]++;
            }
        }

        // Perform Kahn's algorithm for topological sorting
        var sorted = new List<string>();
        var queue = new Queue<string>();

        foreach (var position in positions)
        {
            if (incomingEdges[position] == 0)
            {
                queue.Enqueue(position);
            }
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            sorted.Add(current);

            foreach (var neighbor in dependencyGraph[current])
            {
                incomingEdges[neighbor]--;
                if (incomingEdges[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return sorted;
    }

    private static int GetSumOfMiddles(List<string> validLines) => (
        from line in validLines
        select line.Split(",") into splitArray 
        let middleIndex = splitArray.Length / 2 
        select int.Parse(splitArray[middleIndex])).Sum();

    private static void PrintRules(List<string> validLines)
    {
        foreach (var printRule in validLines) 
            Console.WriteLine(printRule);
    }

    #endregion
}