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
using Windows.UI.ApplicationSettings;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace PriView
{
  /// <summary>
  /// 多くのアプリケーションに共通の特性を指定する基本ページ。
  /// </summary>
  public sealed partial class InputPassPage : Page
  {

    private NavigationHelper navigationHelper;
    private ObservableDictionary defaultViewModel = new ObservableDictionary();

    //独自の定義
    char[] ans = new char[4] { '-', '-', '-', '-' };
    char[] disp = new char[4] { '-', '-', '-', '-' };
    string r_ans, r_disp;
    string r_pass = null, d_pass = null;
    public string MainPass { get; set; }
    public string DummyPass { get; set; }


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


    public InputPassPage()
    {
      this.InitializeComponent();
      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += navigationHelper_LoadState;
      this.navigationHelper.SaveState += navigationHelper_SaveState;

      r_ans = String.Join("", ans);
      this.DataContext = r_ans;
      Data.PickPass p1 = new Data.PickPass(ref r_pass, ref d_pass);
      MainPass = r_pass;
      DummyPass = d_pass;
      this.SizeChanged += InputPassPage_SizeChanged;

//      SettingsPane.GetForCurrentView().CommandsRequested -= MainGroupPage_CommandsRequested;


    }
/*
    void MainGroupPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
    {
      var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
      string setting;
      setting = resourceLoader.GetString("PassRegistration");
      if (args.Request.ApplicationCommands.Count > 0) { return; }
      SettingsCommand updateSetting =
          new SettingsCommand("AppUpdateSettings", setting, (handler) =>
          {
            MainSettingsFlyout updatesFlyout = new MainSettingsFlyout();
            updatesFlyout.Show();

          });

      args.Request.ApplicationCommands.Add(updateSetting);

    }
*/
    void InputPassPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {

      if (e.NewSize.Width < 850)
      {
//        var Center = new HorizontalAlignment();
       

        adGrid.Width = 250;
        adGrid.Height = 125;
        adGrid.Margin = new Thickness(0, 0.0, 0, 500);
        adGrid.HorizontalAlignment = HorizontalAlignment.Center;


        numberGrid.HorizontalAlignment = HorizontalAlignment.Center;

        
      }
      else
      {

        adGrid.Width = 320;
        adGrid.Height = 600;
        adGrid.Margin = new Thickness(25, 0, 0, 50);
        adGrid.HorizontalAlignment = HorizontalAlignment.Left;

        numberGrid.HorizontalAlignment = HorizontalAlignment.Right;
      }

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
        this.Frame.Navigate(typeof(SplitPage), passbox.Text);
      }
      else if (r_ans == d_pass)
      {
        this.Frame.Navigate(typeof(SplitPage), passbox.Text);
      }
      else
      {
        this.DataContext = "Error!";
        //            r_ans = "----";
      }
    }

  }
}
