using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  [Serializable]
  public class MessageText
  {
    private string _Value;

    public string Value
    {
      get
      {
        return _Value;
      }

      set
      {
        _Value = value;
      }
    }
  }
}
