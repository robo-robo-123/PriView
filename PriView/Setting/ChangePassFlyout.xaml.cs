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

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください

namespace PriView.Setting
{
  public sealed partial class ChangePassFlyout : Page
  {
    public string FirstTime {get; set;}
    public string SecondTime { get; set; }
    public string MorD { get; set; }
    public string OriginalPass { get; set; }
    public string InputPass { get; set; }

    public ChangePassFlyout(string string1)
    {
      this.InitializeComponent();
      FirstTime = null; SecondTime = null;
      MorD = string1;

      string Pass = null;
      var p1 = new Data.PickPass(ref Pass);
      OriginalPass = Pass;
    }


    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      InputPass = PassBox.Text;
    }

    private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
    {
      FirstTime = PassBox1.Text;
    }

    private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
    {
     
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if (OriginalPass == InputPass)
      {
        string result = null;
        Data.PassCheck p2 = new Data.PassCheck(FirstTime, SecondTime, PassBox1.Text, PassBox2.Text, ref result, MorD);

        PassResult.Text = result;
      }
      else PassResult.Text = "元のパスワードが間違っています。";

    }

    private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
    {
      SecondTime = PassBox2.Text;
    }
  }
}
