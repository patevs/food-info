/**
 *  src/Food.cs
 */

using System;
using System.IO;
using System.Net;

namespace food_api
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

      public static void GetRequest(string uri)
      {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        // request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using(Stream stream = response.GetResponseStream())
        using(StreamReader reader = new StreamReader(stream))
        {
          string result = reader.ReadToEnd();
          Console.WriteLine(result);
          // return result;
        }
      }

      public static string BuildRequest(){
        string uri = @API_ENDPOINT + "?ingr=apple&app_id=" + APP_ID + "&app_key=" + APP_KEY;
        return uri;
      }

      /*****************************
       * * APPLICATION ENTRY POINT *
       *****************************/

      static void Main(string[] args)
      {
        Console.WriteLine("\n --- FOOD DATABASE --- \n");
        // Load environment variables
        LoadEnv();
        // Construct request url
        // string uri = BuildRequest();
        // Make get request
        // GetRequest(uri);
      }
    }
}

/* EOF */
