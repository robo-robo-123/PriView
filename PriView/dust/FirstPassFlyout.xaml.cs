using System;
using System.Collections.ObjectModel;
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
using System.Text.RegularExpressions;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください

namespace PriView
{
  public sealed partial class FirstPassFlyout : Page
  {

    public string FirstTime {get; set;}
    public string SecondTime { get; set; }
    public string MorD { get; set; }

    public FirstPassFlyout(string string1)
    {
      this.InitializeComponent();
      FirstTime = null; SecondTime = null;
      MorD = string1;

      /*
      var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
      if (MorD == "Main")
      {
        PassSaveBlock.Text = resourceLoader.GetString("MainPassSet");
        AttentionBox.Text = resourceLoader.GetString("MainAttention");
      }
      else if (MorD == "Dummy")
      {
        PassSaveBlock.Text = resourceLoader.GetString("DummyPassSet");
        AttentionBox.Text = resourceLoader.GetString("DummyAttention");
      }
      */

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
      Data.PassCheck p1 = new Data.PassCheck(FirstTime, SecondTime, PassBox1.Text, PassBox2.Text, ref result, MorD);

     PassResult.Text = result;

    }
  }
}
