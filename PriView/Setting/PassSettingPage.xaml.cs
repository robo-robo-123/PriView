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
using Windows.UI.Core;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace PriView.Setting
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class PassSettingPage : Page
  {

    public string FirstTime { get; set; }
    public string SecondTime { get; set; }
    public string MorD { get; set; }

    public PassSettingPage()
    {
      this.InitializeComponent();

      this.InitializeComponent();
      FirstTime = null; SecondTime = null;
      //MorD = string1;

      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (Frame.CanGoBack)
        {
          Frame.GoBack();
          args.Handled = true;
        }
      };

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      FirstTime = PassBox1.Text;
    }

    private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
    {
      SecondTime = PassBox2.Text;
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {

      string result = null;
      Data.PassCheck p1 = new Data.PassCheck(FirstTime, SecondTime, PassBox1.Text, PassBox2.Text, ref result, "Main");

      PassResult.Text = result;

    }

    //ページ読み込み時に、メインかダミーかを引数で選ぶようにする。

  }
}
