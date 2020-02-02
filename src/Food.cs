/**
 *  2_food/Food.cs
 */

using System;
using System.IO;
using System.Net;

namespace Food
{
    class Program
    {
      /*
      public static string Get(string uri)
      {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using(Stream stream = response.GetResponseStream())
        using(StreamReader reader = new StreamReader(stream))
        {
          string result = reader.ReadToEnd();
          Console.WriteLine(result);
          return result;
        }
      }
      */

      public static void GetRequest(string uri) {
        // 'https://api.edamam.com/api/food-database/parser?ingr=red%20apple&app_id={your app_id}&app_key={your app_key}'
        // string uri = @"https://api.edamam.com/api/food-database/parser";
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

      static void Main(string[] args)
      {
        Console.WriteLine("\n --- FOOD DATABASE --- \n");
        // construct url
        // string app_id = "cdfcb58a";
        // string app_key = "1bf534f78ce5ecb6542063ce18f2db89";
        // string endpoint = "https://api.edamam.com/api/food-database/parser";
        // string uri = @endpoint + "?ingr=red%20apple&app_id=" + app_id + "&app_key" + app_key;
        string uri = @"https://api.edamam.com/api/food-database/parser?ingr=red%20apple&app_id=cdfcb58a&app_key=1bf534f78ce5ecb6542063ce18f2db89";
        GetRequest(uri);
      }
    }
}

/* EOF */
