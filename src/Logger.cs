/**
 *  src/Logger.cs
 */

using System;
// using System.IO;

using Alba.CsConsoleFormat.Fluent;

namespace food_info
{
  class Logger
  {
      public void Info(string message)
      {
        Colors.WriteLine(
          "  ",
          " INFO ".White().OnBlue(),
          " ", message, "\n"
        );
      }

      public void Error(string message)
      {
        Colors.WriteLine(
          "  ",
          " ERROR ".White().OnRed(),
          " ", message, "\n"
        );
      }

      public void PrintWelcome()
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
  }
}

/* EOF */
