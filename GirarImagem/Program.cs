using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GirarImagem
{
  class Program
  {
    static void Main(string[] args)
    {
      //var url = @"C:\Users\Nero\Downloads\testeRotacao.JPG";
      var url = @"C:\Users\Nero\Downloads\foto com erro_1.jpg";
      using (var img = Image.FromFile(url))
      {
        for (int i = 0; i < 10; i++)
        {
          try
          {
            img.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            var t = img.RawFormat.Guid;
            img.Save(url);
            Thread.Sleep(TimeSpan.FromSeconds(10));
          }
          catch (Exception)
          {
            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            img.Save(url);
          }
          try
          {

          }
          catch (Exception)
          {
            
          }
        }

      }
    }
  }
}
