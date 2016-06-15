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
using Windows.UI.ApplicationSettings;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください

namespace PriView
{
  public sealed partial class MainSettingsFlyout : Page
  {
    public string MainOriginalPass { get; set; }
    public string DummyOriginalPass { get; set; }

    public MainSettingsFlyout()
    {
      this.InitializeComponent();
//      SettingsPane.GetForCurrentView().CommandsRequested += MainGroupPage_CommandsRequested;
      string r_pass = null, d_pass = null;
      var p2 = new Data.PickPass(ref r_pass, ref d_pass);
      MainOriginalPass = r_pass;
      DummyOriginalPass = d_pass;
/*      var p1 = new Data.PickPass(ref r_pass);
      MainOriginalPass = r_pass;*/

    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if (MainOriginalPass == null)
      {
        Frame.Navigate(typeof(GeneralSettingsFlyout));
        //FirstPassFlyout updatesFlyout = new FirstPassFlyout("Main");
        // updatesFlyout.ShowIndependent();
      }
      else
      {
        Frame.Navigate(typeof(GeneralSettingsFlyout));
        //ChangePassFlyout updatesFlyout = new ChangePassFlyout("Main");
        //  updatesFlyout.ShowIndependent();
      }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
     if (MainOriginalPass != null)
      {
        if (DummyOriginalPass == null)
        {
          FirstPassFlyout updatesFlyout = new FirstPassFlyout("Dummy");
        //  updatesFlyout.ShowIndependent();
        }
        else
        {
          ChangePassFlyout updatesFlyout = new ChangePassFlyout("Dummy");
        //  updatesFlyout.ShowIndependent();
        }
      }
     else
     {
       var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
       ErrorBox.Text = resourceLoader.GetString("ErrorBox");
     }
    }
  }
}
