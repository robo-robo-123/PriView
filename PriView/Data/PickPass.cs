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
  class PickPass
  {
    PasswordVault vaultMain = new PasswordVault();
    PasswordVault vaultDummy = new PasswordVault();

    public PickPass(ref string MainPass)
    {
      try
      {
        PasswordCredential cred = vaultMain.Retrieve("user", "Main");
      }
      catch (Exception ex)
      {
        MainPass = null;
        return;
      }
      PasswordCredential cred1 = vaultMain.Retrieve("user", "Main");
      MainPass = cred1.Password;
    }


    public PickPass(ref string MainPass, ref string DummyPass)
    {
      try
      {
        PasswordCredential cred = vaultMain.Retrieve("user", "Main");
      }
      catch (Exception ex)
      {
        MainPass = null;
        return;
      }
      PasswordCredential cred1 = vaultMain.Retrieve("user", "Main");
      MainPass = cred1.Password;
      try
      {
        PasswordCredential cred = vaultDummy.Retrieve("user", "Dummy");
      }
      catch (Exception ex)
      {
        DummyPass = null;
        return;
      }
      PasswordCredential cred2 = vaultDummy.Retrieve("user", "Dummy");
      DummyPass = cred2.Password;
    }
  }
}
