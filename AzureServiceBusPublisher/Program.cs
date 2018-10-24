using DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.TiposPadronizados;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusPublisher
{
  class Program
  {

    static void Main(string[] args)
    {
      var connectionString = "Endpoint=sb://financialmanager.servicebus.windows.net/;SharedAccessKeyName=Public;SharedAccessKey=vizEnn0n32MRAtKqsKA2NfHj3BLOICQxEliLQGaKK4w=;EntityPath=talkprocess";
      //var connectionString = "Endpoint=sb://financialmanager.servicebus.windows.net/;SharedAccessKeyName=publicTest;SharedAccessKey=jypTLjXCdfFX+hhvNovA261Y07Qwz3JU1Fd/7jHVuOY=;EntityPath=talkprocesstest";
      connectionString = "sb://financialmanager.servicebus.windows.net";
      var token = "jypTLjXCdfFX+hhvNovA261Y07Qwz3JU1Fd/7jHVuOY=";
      var queueName = "talkprocesstest";

      var program = new Message();

      var task = Task.Run(async () =>
      {
        await program.SendMessagesAsync(connectionString, queueName, token);
      });
      Task.WaitAll(task);
      //Console.ReadLine();
      //var client = QueueClient.CreateFromConnectionString(connectionString);
      //
      //var message = new BrokeredMessage("This is a test message!");
      //
      //client.Send(message);
    }
  }
  public class Message
  {
    public async Task SendMessagesAsync(string namespaceAddress, string queueName, string sendToken)
    {
      var senderFactory = MessagingFactory.Create(
          namespaceAddress,
          new MessagingFactorySettings
          {
            TransportType = TransportType.Amqp,
            TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("Publictest", sendToken)
          });
      var sender = await senderFactory.CreateMessageSenderAsync(queueName);

      Console.WriteLine("Sending messages to Queue...");

      XProcessamento[] data = new[]{
        new XProcessamento()
        {
          TipoProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XTipoProcessamento.IndicadoresAreaFuncional,
          ID = Guid.Parse("8136C231-DF5F-47D3-A4D6-1354F55E4BD5"),
          RegistroParaProcessarID = Guid.Parse("9197E99A-E183-4258-87DC-FAC1BEE0776E"),
          StatusProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XStatusProcessamento.Aguardando,
        },
        //new XProcessamentoResumoDiscussao()
        //  {
        //    TipoProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XTipoProcessamento.EmailResumoGerencial,
        //    ID = Guid.Parse("73FE8E7D-D63D-4733-B8ED-8C20258771EF"),
        //    RegistroParaProcessarID = Guid.Parse("9E2E5E38-F0F4-48DF-9B50-A29BF645AEE2"),
        //    ProcessamentoParenteID = Guid.Parse("7F4FB846-83CA-4379-AA98-31682A397BB8"),
        //    DiscussaoID = Guid.Parse("4d013b26-1a4c-4993-a6f7-dbf57b0cdb0c"),
        //    EhGeracaoEmail = true,
        //    Publicado = false,
        //    StatusProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XStatusProcessamento.Processado,
        //  },
        //new XProcessamentoResumoDiscussao()
        //{
        //  TipoProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XTipoProcessamento.ResumoGerencial,
        //  ID = Guid.Parse("42961C86-3E2B-4093-8E32-F9EA1FA0FFFD"),
        //  RegistroParaProcessarID = Guid.Parse("2B0DA344-3FB4-49FF-8916-4F319ACE4780"),
        //  DiscussaoID = Guid.Parse("C9C789AA-DDEF-4C65-AEC2-F84286E04AF7"),
        //  StatusProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XStatusProcessamento.Aguardando,
        //  DataFinal = new DateTime(2017, 04, 03),
        //  DataInicial = new DateTime(2016, 12, 01),
        //  TipoResumoAtividade = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XTipoResumoAtividade.Comparacao
        //},
        //new XProcessamentoResumoDiscussao()
        //{
        //  TipoProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XTipoProcessamento.ResumoDiscussao,
        //  ID = Guid.Parse("9D57CCA7-82EE-4817-A72F-7A2F87CCF592"),
        //  RegistroParaProcessarID = Guid.Parse("3C0DEBF3-FE33-4711-BAB9-DDAB561498C4"),
        //  DiscussaoID = Guid.Parse("4D013B26-1A4C-4993-A6F7-DBF57B0CDB0C"),
        //  StatusProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XStatusProcessamento.Aguardando,
        //  DataFinal = new DateTime(2017, 03, 24),
        //  DataInicial = new DateTime(2016, 12, 01),
        //  TipoResumoAtividade = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XTipoResumoAtividade.Comparacao
        //}
      };

      foreach (var item in data)
      {
        //DiscussaoID = Guid.Parse("4D013B26-1A4C-4993-A6F7-DBF57B0CDB0C"),
        var message = new BrokeredMessage(item)
        {
          ContentType = item.GetType().FullName
        };
        try
        {
          await sender.SendAsync(message);
          lock (Console.Out)
          {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Message sent: Id = {0}", message.MessageId);
            Console.ResetColor();
          }
        }
        catch (Exception pEx)
        {

          throw;
        }
      }
    }
  }
}
