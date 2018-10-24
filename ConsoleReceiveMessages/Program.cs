using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleReceiveMessages
{
  class Program
  {
    static void Main(string[] args)
    {
      MessageQueue msg = new MessageQueue(@"FormatName:Direct=TCP:192.168.1.30\private$\talkprocess", QueueAccessMode.Receive);
      msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(MessageText) });
      while (true)
      {
        if (msg.CanRead)
        {
        try
        {
          Message text = msg.Receive();
          System.Console.WriteLine(((MessageText)text.Body).Value);
        }
        catch (Exception pEx)
        {
          System.Console.WriteLine("Inpossível ler.");
          Thread.Sleep(10000);
        }
        }
        
      }
      System.Console.ReadLine();
    }
  }
}
