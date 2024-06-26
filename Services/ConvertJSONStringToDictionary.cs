// File: ConvertJsonUtility.cs
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShoppyMcShopFace.Services
{
    public static class JsonUtilities
    {
        public static Dictionary<string, List<string>> ConvertJsonStringToDictionary(string jsonString)
        {
            // Normalize the JSON string
            string normalizedJsonString = jsonString;
            if (jsonString.StartsWith("\"") && jsonString.EndsWith("\""))
            {
                normalizedJsonString = JsonConvert.DeserializeObject<string>(jsonString);
            }

            // Parse the normalized JSON string into a dictionary with string values
            var stringDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(normalizedJsonString);

            // Convert the dictionary with string values to a dictionary with List<string> values
            var listDict = new Dictionary<string, List<string>>();
            foreach (var kvp in stringDict)
            {
                listDict[kvp.Key] = new List<string> { kvp.Value };
            }

            return listDict;
        }
    }
}
