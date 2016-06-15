using PriView.Common;
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

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace PriView.Browser
{
  /// <summary>
  /// 多くのアプリケーションに共通の特性を指定する基本ページ。
  /// </summary>
  public sealed partial class BasicPage1 : Page
  {

    private NavigationHelper navigationHelper;
    private ObservableDictionary defaultViewModel = new ObservableDictionary();

    /// <summary>
    /// これは厳密に型指定されたビュー モデルに変更できます。
    /// </summary>
    public ObservableDictionary DefaultViewModel
    {
      get { return this.defaultViewModel; }
    }

    /// <summary>
    /// NavigationHelper は、ナビゲーションおよびプロセス継続時間管理を
    /// 支援するために、各ページで使用します。
    /// </summary>
    public NavigationHelper NavigationHelper
    {
      get { return this.navigationHelper; }
    }


    public BasicPage1()
    {
      this.InitializeComponent();
      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += navigationHelper_LoadState;
      this.navigationHelper.SaveState += navigationHelper_SaveState;

      this.webView1.NavigationCompleted += webView1_NavigationCompleted;
    }

    /// <summary>
    /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
    /// 再作成する場合は、保存状態も指定されます。
    /// </summary>
    /// <param name="sender">
    /// イベントのソース (通常、<see cref="NavigationHelper"/>)>
    /// </param>
    /// <param name="e">このページが最初に要求されたときに
    /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたナビゲーション パラメーターと、
    /// 前のセッションでこのページによって保存された状態の辞書を提供する
    /// セッション。ページに初めてアクセスするとき、状態は null になります。</param>
    private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
    {
    }

    /// <summary>
    /// アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
    /// このページに関連付けられた状態を保存します。値は、
    /// <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
    /// </summary>
    /// <param name="sender">イベントのソース (通常、<see cref="NavigationHelper"/>)</param>
    /// <param name="e">シリアル化可能な状態で作成される空のディクショナリを提供するイベント データ
    ///。</param>
    private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
    {
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

    #region NavigationHelper の登録

    /// このセクションに示したメソッドは、NavigationHelper がページの
    /// ナビゲーション メソッドに応答できるようにするためにのみ使用します。
    /// 
    /// ページ固有のロジックは、
    /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
    /// および <see cref="GridCS.Common.NavigationHelper.SaveState"/> のイベント ハンドラーに配置する必要があります。
    /// LoadState メソッドでは、前のセッションで保存されたページの状態に加え、
    /// ナビゲーション パラメーターを使用できます。

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      navigationHelper.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      navigationHelper.OnNavigatedFrom(e);
    }

    #endregion

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
  
  
  }

}
