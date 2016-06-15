using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriView.Data
{
  class PassCheck
  {
        public string FirstTimeC { get; set; }
    public string SecondTimeC { get; set; }
    public string MorD { get; set; }

    public PassCheck(string FirstTime, string SecondTime, string PassBox1, string PassBox2, ref string result, string MorD)
    {

      var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();

      if (String.IsNullOrEmpty(FirstTime) == false)
      {
        foreach (char c in FirstTime)
        {
          if (0 <= c.CompareTo('0') && c.CompareTo('9') <= 0)
          {
            FirstTimeC += c;
          }
        }
      }

      if (String.IsNullOrEmpty(SecondTime) == false)
      {
        foreach (char c in SecondTime)
        {
          if (0 <= c.CompareTo('0') && c.CompareTo('9') <= 0)
          {
            SecondTimeC += c;
          }
        }
      }

      if (String.IsNullOrEmpty(PassBox1) == true && String.IsNullOrEmpty(PassBox2) == true)
      {
        var Passnull = new Data.PassStore(MorD);
        result = resourceLoader.GetString("PassRelease");
      }

      else if (FirstTime != FirstTimeC || SecondTime != SecondTimeC)
      {
        result = resourceLoader.GetString("OnlyNumbers");
        FirstTime = null; FirstTimeC = null;
        SecondTime = null; SecondTimeC = null;

      }
      else if (String.IsNullOrEmpty(FirstTime) == false && FirstTime.Length > 4)
      {
        result = resourceLoader.GetString("Only4");
        FirstTime = null; FirstTimeC = null;
        SecondTime = null; SecondTimeC = null;
      }
      //     Data.PassStore Pass = new Data.PassStore();
      else if (FirstTime == SecondTime)
      {
        var Passok = new Data.PassStore(FirstTime, MorD);
        result = resourceLoader.GetString("PassRegistration");
        FirstTime = null; FirstTimeC = null;
        SecondTime = null; SecondTimeC = null;


      }
      else
      {
        var Passnull = new Data.PassStore(MorD);
        result = resourceLoader.GetString("Error");
        FirstTime = null; FirstTimeC = null;
        SecondTime = null; SecondTimeC = null;
      }
    }
  }
}
