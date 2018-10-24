using DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.TiposPadronizados;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureServiceBusReciver
{
  class Program
  {
    static void Main(string[] args)
    {
      var connectionString = "sb://financialmanager.servicebus.windows.net";
      var queueName = "publicTest";
      var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("Public", "jypTLjXCdfFX+hhvNovA261Y07Qwz3JU1Fd/7jHVuOY=");
      var program = new Program();

      var messageFactory = new MessagingFactorySettings
      {
        TransportType = TransportType.Amqp,
        TokenProvider = tokenProvider
      };
      var uri = new Uri(connectionString);
      var receiverFactory = MessagingFactory.Create(connectionString, messageFactory);

      Task.Run(async () =>
      {
        await program.ReceiveMessagesAsync(receiverFactory, queueName);
      });
      System.Console.ReadLine();
    }

    async Task ReceiveMessagesAsync(MessagingFactory receiverFactory, string queueName)
    {
      try
      {
        var receiver = await receiverFactory.CreateMessageReceiverAsync(queueName, ReceiveMode.PeekLock);

        Console.WriteLine("Receiving message from Queue...");
        while (true)
        {
          try
          {
            //receive messages from Queue
            var message = await receiver.ReceiveAsync(TimeSpan.FromSeconds(2));
            if (message != null)
            {
              await message.CompleteAsync();
              continue;
              var body = message.GetBody<XProcessamentoResumoDiscussao>();

              lock (Console.Out)
              {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(
                    "\t\t\t\tMessage received: \n\t\t\t\t\t\tMessageId = {0}, \n\t\t\t\t\t\tSequenceNumber = {1}, \n\t\t\t\t\t\tEnqueuedTimeUtc = {2}," +
                    "\n\t\t\t\t\t\tExpiresAtUtc = {5}, \n\t\t\t\t\t\tContentType = \"{3}\", \n\t\t\t\t\t\tSize = {4},  \n\t\t\t\t\t\tContent: {6}",
                    message.MessageId,
                    message.SequenceNumber,
                    message.EnqueuedTimeUtc,
                    message.ContentType,
                    message.Size,
                    message.ExpiresAtUtc,
                    body.ID);
                Console.ResetColor();
              }
              await message.CompleteAsync();
            }
            else
            {
              System.Console.WriteLine("\nSem novas Mensagens\n");//no more messages in the queue

            }
          }
          catch (MessagingException e)
          {
            if (!e.IsTransient)
            {
              Console.WriteLine(e.Message);
              throw;
            }
          }
        }
        await receiver.CloseAsync();
        await receiverFactory.CloseAsync();
      }
      catch (Exception pEx)
      {

        throw;
      }
    }
  }
}
