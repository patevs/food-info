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

      private const string APP_ID = "cdfcb58a";
      private const string APP_KEY = "1bf534f78ce5ecb6542063ce18f2db89";
      private const string API_ENDPOINT = "https://api.edamam.com/api/food-database/parser";

      /***************
       * * FUNCTIONS *
       ***************/

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
        DotNetEnv.Env.Load();
        // Construct request url
        string uri = BuildRequest();
        // Make get request
        GetRequest(uri);
      }
    }
}

/* EOF */
