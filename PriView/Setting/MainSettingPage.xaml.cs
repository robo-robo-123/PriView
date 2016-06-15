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

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace PriView.Setting
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MainSettingPage : Page
  {
    public string MainOriginalPass { get; set; }
    public string DummyOriginalPass { get; set; }
    public MainSettingPage()
    {
      this.InitializeComponent();
      string r_pass = null, d_pass = null;
      var p2 = new Data.PickPass(ref r_pass, ref d_pass);
      MainOriginalPass = r_pass;
      DummyOriginalPass = d_pass;
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if (MainOriginalPass == null)
      {
        Frame.Navigate(typeof(Setting.PassSettingPage));
      }
      else
      {
        Frame.Navigate(typeof(Setting.PassSettingPage));
      }
    }

    
    //遷移時に、メインかダミー化を指定

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      if (MainOriginalPass != null)
      {
        if (DummyOriginalPass == null)
        {
          Frame.Navigate(typeof(Setting.PassSettingPage));
          // FirstPassFlyout updatesFlyout = new FirstPassFlyout("Dummy");
        }
        else
        {
          Frame.Navigate(typeof(Setting.PassSettingPage));
          //ChangePassFlyout updatesFlyout = new ChangePassFlyout("Dummy");
        }
      }
      else
      {
        var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
      //  ErrorBox.Text = resourceLoader.GetString("ErrorBox");
      }
    }
    
  }
}
