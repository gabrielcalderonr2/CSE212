using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        // Use a set to store words we have seen
        var set = new HashSet<string>();
        var result = new List<string>();

        foreach (var word in words)
        {
            // Reverse the word
            var reversed = new string(new char[] { word[1], word[0] });

            // Ignore cases like "aa"
            if (word == reversed)
                continue;

            // If reversed word already exists, we found a pair
            if (set.Contains(reversed))
            {
                // FIX: always keep consistent order
                result.Add($"{word} & {reversed}");
            }

            // Add current word to set
            set.Add(word);
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            // FIX: ensure valid row (avoid index errors)
            if (fields.Length < 4)
                continue;

            // Column 4 contains the degree (index 3)
            var degree = fields[3].Trim();

            // Skip empty values
            if (degree == "")
                continue;

            // Count occurrences
            if (!degrees.ContainsKey(degree))
            {
                degrees[degree] = 0;
            }

            degrees[degree]++;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and normalize case
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // If lengths differ → not anagrams
        if (word1.Length != word2.Length)
            return false;

        // Dictionary to count letters
        var dict = new Dictionary<char, int>();

        // Count letters in word1
        foreach (char c in word1)
        {
            if (!dict.ContainsKey(c))
                dict[c] = 0;

            dict[c]++;
        }

        // Subtract using word2
        foreach (char c in word2)
        {
            if (!dict.ContainsKey(c))
                return false;

            dict[c]--;

            if (dict[c] < 0)
                return false;
        }

        // All counts match → anagram
        return true;
    }

    /// <summary>
    /// Earthquake summary (not required to pass assignment)
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Not implemented (optional problem)
        // Create a list to store results
var results = new List<string>();

// Loop through all earthquakes
foreach (var feature in featureCollection.Features)
{
    var place = feature.Properties.Place;
    var mag = feature.Properties.Mag;

    // Simple string format
    results.Add($"{place} - Mag {mag}");
}

// Return results as array
return results.ToArray();
    }
}