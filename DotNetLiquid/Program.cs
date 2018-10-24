using DotLiquid;
using DotLiquid.FileSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLiquid
{
  class Program
  {
    static void Main(string[] args)
    {

      //Template.RegisterSafeType(typeof(TemplateTeste)
      Template.RegisterFilter(typeof(TextFilter));
      Template.FileSystem = new LocalFileSystem("C:\\temp\\");
      Template template = Template.Parse("{% include 'partial_template' %}");
      template = Template.Parse("Name: {{ user.Nome | Textilize:'TESTE' }}; Sobrenome: {{ user.Sobrenome | pega_padrao:'Valor1'}};");
      string result = template.Render(Hash.FromAnonymousObject(new
      {
        user = new
        {
          Nome = "Nero",
          Sobrenome = "Rodrigues"
        }
      }));
      File.WriteAllText("C:\\Temp\\DotNetLiquid.txt",result);
    }

    public static class TextFilter
    {
      public static string Textilize(string pInput,string pValor)
      {
        return string.Format("<b>{0}</b>", pInput);
      }
      public static string PegaPadrao(string pInput, string pValor)
      {
        return string.Format("<br>{0}</br>", pInput);
      }
    }
  }

  public class TemplateTeste
  {
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
  }


}
