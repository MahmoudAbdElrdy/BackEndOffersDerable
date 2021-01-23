using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthFaildResponse
  {
    public IEnumerable<string> Errors { get; set; }
  }
}
