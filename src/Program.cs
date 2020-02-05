/**
 *  src/Program.cs
 */

using System;
using System.IO;
using System.Net;

using Alba.CsConsoleFormat.Fluent;

using Newtonsoft.Json.Linq;

namespace food_app
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

      private static void LoadEnv(){
        // Load environment variables from .env file
        DotNetEnv.Env.Load();
        // Access environment variables
        APP_ID = System.Environment.GetEnvironmentVariable("APP_ID");
        APP_KEY = System.Environment.GetEnvironmentVariable("APP_KEY");
        // Or using helper methods
        // string test = DotNetEnv.Env.GetString("API_KEY", "Variable not found");
      }

      private static string BuildRequest(string query){
        // string uri = @API_ENDPOINT + "?ingr=apple&app_id=" + APP_ID + "&app_key=" + APP_KEY;
        string uri = @API_ENDPOINT + "?ingr=" + query + "&app_id=" + APP_ID + "&app_key=" + APP_KEY;
        return uri;
      }

      private static void GetRequest(string uri)
      {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        // request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using(Stream stream = response.GetResponseStream())
        using(StreamReader reader = new StreamReader(stream))
        {
          string result = reader.ReadToEnd();
          // Console.WriteLine(result);
          // return result;
          ParseJson(result);
        }
      }

      private static void ParseJson(string json){
        dynamic item = JObject.Parse(json);
        string text = item.text;
        JObject details = item.parsed[0].food.nutrients;
        Console.WriteLine(text);
        Console.WriteLine(details);
      }

      private static void Run()
      {
        Console.Write(" Enter a food item to lookup : ");
        Console.ForegroundColor = ConsoleColor.Green;
        // Accept input from the user
        string query = Console.ReadLine();
        Console.ResetColor();
        Console.WriteLine();
        // Check query length
        if(query.Length < 2) {
          Colors.WriteLine(" Query must be greater than 2 letters... ".Red());
        }
        // Construct request url
        string uri = BuildRequest(query);
        // Make get request
        GetRequest(uri);
      }

      private static void Init(){
        // Load environment variables
        LoadEnv();
        // Set encoding for ASCII graphics
        // Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Clear the console
        Console.Clear();
        Console.ResetColor();
        // Print welcome message
        // Console.WriteLine("\n --- FOOD DATABASE --- \n");
        Colors.WriteLine(
          "\n ---- ",
          " WELCOME TO THE FOOD DATABASE ".Black().OnGreen(),
          " ---- \n");
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
