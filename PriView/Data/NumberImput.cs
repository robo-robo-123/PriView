using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriView.Data
{
  class NumberImput
  {

    public NumberImput(char[] ans, ref string r_ans, char[] disp, ref string r_disp, char number)
    {

      if (ans[0] == '-') 
      {
        ans[0] = number;
        disp[0] = '・';
      }
      else if (ans[1] == '-')
      {
        ans[1] = number;
        disp[1] = '・';
      }
      else if (ans[2] == '-')
      {
        ans[2] = number;
        disp[2] = '・';
      }
      else if (ans[3] == '-')
      {
        ans[3] = number;
        disp[3] = '・';
      }

      r_disp = String.Join("", disp);
      r_ans = String.Join("", ans);
    }
  }
}
