/**
 *  src/Food.cs
 */

using System;
using System.IO;
using System.Net;
// using DotNetEnv;

namespace food_api
{
    class Program
    {

      /***************
       * * CONSTANTS *
       ***************/

      private const string APP_ID = "cdfcb58a";
      private const string APP_KEY = "1bf534f78ce5ecb6542063ce18f2db89";
      private const string API_ENDPOINT = "https://api.edamam.com/api/food-database/parser";

      /***************
       * * FUNCTIONS *
       ***************/

      private static void LoadEnv(){
        // Load environment variables from .env file
        DotNetEnv.Env.Load();
        // Access environment variables
        string test = System.Environment.GetEnvironmentVariable("TEST");
        // string test = DotNetEnv.Env.GetString("TEST", "Variable not found");
        Console.WriteLine(test);
        // string ip = System.Environment.GetEnvironmentVariable("IP");
        // Console.WriteLine(ip);
        // Or using helper methods
        // string val = DotNetEnv.Env.GetString("THIS_DOES_NOT_EXIST", "Variable not found");
        // Console.WriteLine(val);
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
