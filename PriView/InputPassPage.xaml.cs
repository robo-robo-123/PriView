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

namespace PriView
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class InputPassPage : Page
  {

    //独自の定義
    char[] ans = new char[4] { '-', '-', '-', '-' };
    char[] disp = new char[4] { '-', '-', '-', '-' };
    string r_ans, r_disp;
    string r_pass = null, d_pass = null;
    public string MainPass { get; set; }
    public string DummyPass { get; set; }
    string flag = "";

    public InputPassPage()
    {
      this.InitializeComponent();


      r_ans = String.Join("", ans);
      this.DataContext = r_ans;
      Data.PickPass p1 = new Data.PickPass(ref r_pass, ref d_pass);
      MainPass = r_pass;
      DummyPass = d_pass;
     // this.SizeChanged += InputPassPage_SizeChanged;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      flag = (string)e.Parameter;
      //replyBox.Text = item.ToString();
      //show();
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
      char number = '1';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button2_Click(object sender, RoutedEventArgs e)
    {
      char number = '2';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button3_Click(object sender, RoutedEventArgs e)
    {
      char number = '3';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button4_Click(object sender, RoutedEventArgs e)
    {
      char number = '4';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button5_Click(object sender, RoutedEventArgs e)
    {
      char number = '5';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button6_Click(object sender, RoutedEventArgs e)
    {
      char number = '6';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button7_Click(object sender, RoutedEventArgs e)
    {
      char number = '7';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button8_Click(object sender, RoutedEventArgs e)
    {
      char number = '8';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void Button9_Click(object sender, RoutedEventArgs e)
    {
      char number = '9';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void back_Click(object sender, RoutedEventArgs e)
    {
      if (ans[3] != '-')
      {
        ans[3] = '-';
        disp[3] = '-';
      }
      else if (ans[2] != '-')
      {
        ans[2] = '-';
        disp[2] = '-';
      }
      else if (ans[1] != '-')
      {
        ans[1] = '-';
        disp[1] = '-';
      }
      else if (ans[0] != '-')
      {
        ans[0] = '-';
        disp[0] = '-';
      }

      r_disp = String.Join("", disp);
      r_ans = String.Join("", ans);

      this.DataContext = r_disp;
    }

    private void Button0_Click(object sender, RoutedEventArgs e)
    {
      char number = '0';
      Data.NumberImput p1 = new Data.NumberImput(ans, ref r_ans, disp, ref r_disp, number);
      this.DataContext = r_disp;
    }

    private void enter_Click(object sender, RoutedEventArgs e)
    {

      //     int iCompare 
      if (r_ans == r_pass || (r_pass == null && r_ans == "----"))
      {
          this.Frame.Navigate(typeof(Browser.MainBrowser), passbox.Text);
      }
      else if (r_ans == d_pass)
      {
          this.Frame.Navigate(typeof(Browser.MainBrowser), passbox.Text);
      }
      else
      {
        this.DataContext = "Error!";
        //            r_ans = "----";
      }


    }

  }
}
