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

      /*****************************
       * * APPLICATION ENTRY POINT *
       *****************************/

      static void Main(string[] args)
      {
        // Set encoding for ASCII graphics
        // Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Print welcome message
        // Console.WriteLine("\n --- FOOD DATABASE --- \n");
        Colors.WriteLine("\n --- ", "FOOD DATABASE".Green(), " --- \n");
        // Load environment variables
        LoadEnv();
        // Prints string out to the console with a line break (Write = No Line Break)
        Console.WriteLine("Enter your search : ");
        // Accept input from the user
        string query = Console.ReadLine();
        // Construct request url
        string uri = BuildRequest(query);
        // Make get request
        GetRequest(uri);
      }
    }
}

/* EOF */
