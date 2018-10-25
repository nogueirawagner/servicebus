using DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.TiposPadronizados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
  class Program
  {
    static void Main(string[] args)
    {
      XProcessamento processamento = new XProcessamento();

      var tipo = processamento.GetType().AssemblyQualifiedName;

      Type type = Type.GetType(tipo);

      /*MessageQueue msg = new MessageQueue(@"FormatName:Direct=TCP:servtest\private$\talkprocess", QueueAccessMode.Send);
      var processamento = new XProcessamentoCompartilhamentoProcesso()
      {
        TipoProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XTipoProcessamento.CompartilhamentoProcesso,
        ID = Guid.Parse("1FC5DEF5-237D-4365-A7D0-D4CBAFD58408"),
        RegistroParaProcessarID = Guid.Parse("DFA7E3CD-AD7A-4E5F-B123-72F94E191AF2"),
        ProcessoID = Guid.Parse("1a80629d-ad18-4799-971c-ac7f6be0f907"),
        StatusProcessamento = DeptoDesenvFontes.ApoioProjetos.Comum.Utilitarios.Enumerados.XStatusProcessamento.Aguardando,
      };

      msg.Send(processamento);
      //for (int i = 0; i < 10000; i++)
      //{
      //  var messageText = new MessageText();
      //  messageText.Value = "TESTE" + i + 1;
      //  msg.Send(messageText);
      //  System.Console.WriteLine("Enviado.");
      //}
      System.Console.ReadLine();*/
    }
  }
}
