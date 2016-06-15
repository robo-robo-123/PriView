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

namespace PriView.Browser
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class Browser2 : Page
  {
    public Browser2()
    {
      this.InitializeComponent();
      this.webView1.NavigationCompleted += webView1_NavigationCompleted;



    }

    async void webView1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
    {
      if (!args.IsSuccess)
      {
        // エラー発生
        string errMsg = args.WebErrorStatus.ToString();
        int errCode = (int)args.WebErrorStatus;
        string msg = string.Format("サーバ側エラー：{0}（{1}）", errMsg, errCode);
        await new Windows.UI.Popups.MessageDialog(msg).ShowAsync();
      }
    }
/*
    private async void textBox1_KeyDown(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != Windows.System.VirtualKey.Enter)
        return;

      await NavigateAsync();
    }

    private async System.Threading.Tasks.Task NavigateAsync()
    {
      Uri newUri;
      if (Uri.TryCreate(this.textBox1.Text, UriKind.Absolute, out newUri)
              && newUri.Scheme.StartsWith("http"))
      {
        this.webView1.Navigate(newUri);
      }
      else
      {
        //（エラー処理）
        string errMsg = @"入力された文字列がURIとして不正か、""http""で始まっていません";
        await new Windows.UI.Popups.MessageDialog(errMsg).ShowAsync();
      }
    }*/

    private async void buttonGo_Tapped(object sender, TappedRoutedEventArgs e)
    {
      Uri newUri;
      if (Uri.TryCreate(this.textBox1.Text, UriKind.Absolute, out newUri)
          && newUri.Scheme.StartsWith("http"))
      {
        this.webView1.Navigate(newUri);
      }
      else
      {
        // エラー処理
        string errMsg = @"入力された文字列がURIとして不正か、""http""で始まっていません";
        await new Windows.UI.Popups.MessageDialog(errMsg).ShowAsync();
      }
    }

    private void Button_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(InputPassPage));
    }

    private void buttonHome_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(MainHubPage));
    }

    private void buttonRefresh_Tapped(object sender, TappedRoutedEventArgs e)
    {
            this.webView1.Refresh();
    }

    private void buttonGoBack_Tapped(object sender, TappedRoutedEventArgs e)
    {
      try
      {
        this.webView1.GoBack();
      }
      catch (Exception ex)
      {
        return;
      }
    }


    private void buttonGoForward_Tapped(object sender, TappedRoutedEventArgs e)
    {

      try
      {
        this.webView1.GoForward();
      }
      catch (Exception ex)
      {
        return;
      }
    }
    private async void webOpen(string url)
    {
      var uri = new Uri(url);
      webView1.Source = uri;

    }

    private void webView1_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
    {
      args.Handled = true;
      webOpen(args.Uri.AbsoluteUri);
    }
  }
}
