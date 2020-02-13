/**
 *  src/Program.cs
 */

using System;
using System.IO;
using System.Net;

using Alba.CsConsoleFormat.Fluent;

using Newtonsoft.Json.Linq;

using ConsoleTables;

namespace food_info
{
    class Program
    {

      /***************
       * * CONSTANTS *
       ***************/

      private const string API_ENDPOINT = "https://api.edamam.com/api/food-database/parser";

      /*************
       * * GLOBALS *
       *************/

      private static string APP_ID;
      private static string APP_KEY;

      /***************
       * * FUNCTIONS *
       ***************/

      private static void DisplayResult(string name, JObject details)
      {
        // ..
        ConsoleTable table = new ConsoleTable("Name", "Quanity", "Unit");

        JToken energy = details["ENERC_KCAL"];
        JToken protien = details["PROCNT"];
        JToken fat = details["FAT"];
        JToken carbs = details["CHOCDF"];
        JToken fiber = details["FIBTG"];

        table
          .AddRow("Energy", energy, "kcal")
          .AddRow("Protien", protien, "g")
          .AddRow("Fat", fat, "g")
          .AddRow("Carbs", carbs, "g")
          .AddRow("Fiber", fiber, "g");

        // table
        //   .AddRow(1, 2, 3)
        //   .AddRow("this line should be longer", "yes it is", "oh");

        table.Write();
        Console.WriteLine();
      }

      private static void ParseJson(string json)
      {
        dynamic item = JObject.Parse(json);
        string name = item.text;
        JObject details = item.parsed[0].food.nutrients;
        Console.WriteLine(name);
        Console.WriteLine(details);
        DisplayResult(name, details);
      }

      private static string GetRequest(string uri)
      {
        Colors.WriteLine(
          "  ",
          " INFO ".White().OnBlue(),
          " Performing GET Request... \n"
        );
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
        // request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using(Stream stream = response.GetResponseStream())
        using(StreamReader reader = new StreamReader(stream))
        {
          return reader.ReadToEnd();
        }
      }

      private static string BuildRequest(string query)
      {
        return @API_ENDPOINT + "?ingr=" + query + "&app_id=" + APP_ID + "&app_key=" + APP_KEY;
      }

      private static string GetQuery()
      {
        Console.Write(" Enter a food item to lookup : ");
        Console.ForegroundColor = ConsoleColor.Green;
        // Accept input from the user
        string query = Console.ReadLine();
        Console.ResetColor();
        Console.WriteLine();
        // Check query length
        // TODO: Ensure query is not a number
        if(query.Length < 2) {
          Colors.WriteLine(
            "  ",
            " ERROR ".White().OnRed(),
            " Query must be greater than 2 letters! \n"
          );
        }
        return query;
      }

      private static void Run()
      {
        PrintWelcome();
        // Get user query
        string query = "";
        while(query.Length < 2)
        {
          query = GetQuery();
        }
        // Construct request url
        string uri = BuildRequest(query);
        // Make get request
        string result = GetRequest(uri);
        // parse the result
        ParseJson(result);
      }

      private static void PrintWelcome()
      {
        // Clear the console
        Console.Clear();
        // Print welcome message
        // Console.WriteLine("\n --- FOOD DATABASE --- \n");
        Colors.WriteLine(
          "\n ---- ",
          " WELCOME TO THE FOOD DATABASE ".Black().OnGreen(),
          " ---- \n"
        );
      }

      private static void LoadEnv()
      {
        // Load environment variables from .env file
        DotNetEnv.Env.Load();
        // Access environment variables
        APP_ID = System.Environment.GetEnvironmentVariable("APP_ID");
        APP_KEY = System.Environment.GetEnvironmentVariable("APP_KEY");
        // Or using helper methods
        // DotNetEnv.Env.GetString("APP_ID", "APP_ID VARIABLE NOT FOUND!");
      }

      private static void Init()
      {
        // Load environment variables
        LoadEnv();
        // Set encoding for ASCII graphics
        // Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Clear the console
        Console.Clear();
        Console.ResetColor();
        // Set console title
        Console.Title = "FOOD DATABASE";
      }

      /*****************************
       * * APPLICATION ENTRY POINT *
       *****************************/

      static void Main(string[] args)
      {
        // Initialize
        Init();
        // Main loop
        Run();
      }
    }
}

/* EOF */
