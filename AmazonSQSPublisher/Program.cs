using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonSQSPublisher
{
  class Program
  {
    static void Main(string[] args)
    {
     

      var entry1 = new SendMessageBatchRequestEntry
      {
        DelaySeconds = 0,
        Id = "Entry1",
        MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
          {
            "MyNameAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "John Doe" }
          },
          {
            "MyAddressAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "123 Main St." }
          },
          {
            "MyRegionAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "Any Town, United States" }
          }
        },
        MessageBody = "John Doe customer information."
      };

      var entry2 = new SendMessageBatchRequestEntry
      {
        DelaySeconds = 0,
        Id = "Entry2",
        MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
          {
            "MyNameAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "Jane Doe" }
          },
          {
            "MyAddressAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "456 Center Road" }
          },
          {
            "MyRegionAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "Any City, United States" }
          }
        },
        MessageBody = "Jane Doe customer information."
      };

      var entry3 = new SendMessageBatchRequestEntry
      {
        DelaySeconds = 0,
        Id = "Entry3",
        MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
          {
            "MyNameAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "Richard Doe" }
          },
          {
            "MyAddressAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "789 East Blvd." }
          },
          {
            "MyRegionAttribute",
            new MessageAttributeValue
            { DataType = "String", StringValue = "Anywhere, United States" }
          }
        },
        MessageBody = "Richard Doe customer information."
      };

      var request = new SendMessageBatchRequest
      {
        Entries = new List<SendMessageBatchRequestEntry>() { entry1, entry2, entry3 },
        QueueUrl = "https://sqs.sa-east-1.amazonaws.com/103067334640/testes"
      };

      var sqsConfig = new AmazonSQSConfig();
      sqsConfig.RegionEndpoint = Amazon.RegionEndpoint.SAEast1;
      sqsConfig.ServiceURL = "https://sqs.sa-east-1.amazonaws.com/103067334640/testes";

      var client = new AmazonSQSClient(sqsConfig);

      try
      {
        client.SendMessageBatch(request);
      }
      catch (Exception e)
      {
        throw;
      }

      var response = client.SendMessageBatch(request);
      if (response.Successful.Count > 0)
      {
        Console.WriteLine("Successfully sent:");

        foreach (var success in response.Successful)
        {
          Console.WriteLine("  For ID: '" + success.Id + "':");
          Console.WriteLine("    Message ID = " + success.MessageId);
          Console.WriteLine("    MD5 of message attributes = " +
            success.MD5OfMessageAttributes);
          Console.WriteLine("    MD5 of message body = " +
            success.MD5OfMessageBody);
        }
      }

      if (response.Failed.Count > 0)
      {
        Console.WriteLine("Failed to be sent:");

        foreach (var fail in response.Failed)
        {
          Console.WriteLine("  For ID '" + fail.Id + "':");
          Console.WriteLine("    Code = " + fail.Code);
          Console.WriteLine("    Message = " + fail.Message);
          Console.WriteLine("    Sender's fault? = " +
            fail.SenderFault);
        }
      }



      //var sqsConfig = new AmazonSQSConfig();
      //sqsConfig.RegionEndpoint = Amazon.RegionEndpoint.SAEast1;
      //sqsConfig.ServiceURL = "https://sqs.sa-east-1.amazonaws.com/103067334640/testes";

      //var client = new AmazonSQSClient(sqsConfig);
      //var sendMessageBatchRequest = new SendMessageBatchRequest
      //{
      //  Entries = new List<SendMessageBatchRequestEntry>
      //  {
      //      new SendMessageBatchRequestEntry("message1", "FirstMessageContent"),
      //      new SendMessageBatchRequestEntry("message2", "SecondMessageContent"),
      //      new SendMessageBatchRequestEntry("message3", "ThirdMessageContent")
      //  },
      //  QueueUrl = "https://sqs.sa-east-1.amazonaws.com/103067334640/testes",
      //};

      //var response = client.SendMessageBatch(sendMessageBatchRequest);
      //Console.WriteLine("Messages successfully sent:");
      //foreach (var success in response.Successful)
      //{
      //  Console.WriteLine("    Message id : {0}", success.MessageId);
      //  Console.WriteLine("    Message content MD5 : {0}", success.MD5OfMessageBody);
      //}

      //Console.WriteLine("Messages failed to send:");
      //foreach (var failed in response.Failed)
      //{
      //  Console.WriteLine("    Message id : {0}", failed.Id);
      //  Console.WriteLine("    Message content : {0}", failed.Message);
      //  Console.WriteLine("    Sender's fault? : {0}", failed.SenderFault);
      //}

      //Console.ReadLine();

      //var obj = JsonConvert.SerializeObject(new
      //{
      //  Tipo = "Produto",
      //  Preco = "20",
      //  Nome = "Sabao"
      //});

      //var sqsConfig = new AmazonSQSConfig();
      //sqsConfig.RegionEndpoint = Amazon.RegionEndpoint.SAEast1;

      //var client = new AmazonSQSClient(sqsConfig);
      //var request = new SendMessageRequest
      //{
      //  DelaySeconds = (int)TimeSpan.FromSeconds(5).TotalSeconds,
      //  MessageAttributes = new Dictionary<string, MessageAttributeValue>
      //  {
      //    {
      //      "Tipo", new MessageAttributeValue
      //        { DataType = "String", StringValue = "Produto" }
      //    },
      //    {
      //      "Preco", new MessageAttributeValue
      //        { DataType = "String", StringValue = "20" }
      //    },
      //    {
      //      "Nome", new MessageAttributeValue
      //        { DataType = "String", StringValue = "Sabao" }
      //    }
      //  },
      //  MessageBody = "John Doe customer information.",
      //  QueueUrl = "https://sqs.sa-east-1.amazonaws.com/103067334640/testes"
      //};

      //var response = client.SendMessage(request);
      //Console.WriteLine("For message ID '" + response.MessageId + "':");
      //Console.WriteLine("  MD5 of message attributes: " +
      //  response.MD5OfMessageAttributes);
      //Console.WriteLine("  MD5 of message body: " + response.MD5OfMessageBody);
    }
  }
}
