using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Security.Credentials;



namespace PriView.Data
{
  class PassStore
  {

        PasswordVault vaultMain = new PasswordVault();
        PasswordVault vaultDummy = new PasswordVault();

        public PassStore(string MorD)
        {

          if (MorD == "Main")
          {
            try
            {
              PasswordCredential cred = vaultMain.Retrieve("user", "Main");
            }
            catch (Exception ex)
            {
              return;
            }
            PasswordCredential cred1 = vaultMain.Retrieve("user", "Main");
            vaultMain.Remove(cred1);
          }

          else if (MorD == "Dummy")
          {
            try
            {
              PasswordCredential cred = vaultDummy.Retrieve("user", "Dummy");
            }
            catch (Exception ex)
            {
              return;
            }
            PasswordCredential cred1 = vaultDummy.Retrieve("user", "Dummy");
            vaultDummy.Remove(cred1);
          }
        }


    public PassStore(string onePass, string MorD)
    {
      if (MorD == "Main")
      {
        PasswordCredential cred1 = new PasswordCredential("user", MorD, onePass);
        this.vaultMain.Add(cred1);
      }
      else if (MorD == "Dummy")
      {
        PasswordCredential cred2 = new PasswordCredential("user", MorD, onePass);
        this.vaultDummy.Add(cred2);
      }
    }

  }



}
