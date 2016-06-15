using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using PriView.Common;
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
using Windows.Storage;

// グループ化されたアイテム ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234231 を参照してください

namespace PriView
{
  /// <summary>
  /// グループ化されたアイテムのコレクションを表示するページです。
  /// </summary>
  public sealed partial class MainGroupPage : Page
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
      get
      {
        return this.navigationHelper;
      }
    }



    public MainGroupPage()
    {
      this.InitializeComponent();
      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += navigationHelper_LoadState;
//      if (eventArgs.Request.ApplicationCommands.Count == 0)
//      {
//      SettingsPane.GetForCurrentView().CommandsRequested += MainGroupPage_CommandsRequested;
//      }
    }

/*
    void MainGroupPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
    {
      var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
      string setting;
      setting = resourceLoader.GetString("PassRegistration");
      if (args.Request.ApplicationCommands.Count > 0)
      { return; }

      SettingsCommand updateSetting =
          new SettingsCommand("AppUpdateSettings", setting, (handler) =>
          {
            MainSettingsFlyout updatesFlyout = new MainSettingsFlyout();
            updatesFlyout.Show();

          });
      if (args.Request.ApplicationCommands.Count == 0)
      {
        args.Request.ApplicationCommands.Add(updateSetting);

      }
      
    }
    */
        private async void UpdateHistory()
    {
      HistView.Items.Clear();

      String filePath = "HistoryData.csv";

      // write file
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;

      try
      {
        StorageFile file = await localFolder.GetFileAsync(filePath);

//        IList<String> strList = await FileIO.ReadLinesAsync(file);
        IList<String> strList = await FileIO.ReadLinesAsync(file);
//        string strList = await FileIO.ReadTextAsync(file);


//        string msg[];
 //       msg = strList.Split('\t');
/*        string msg;

        msg = strList.ToString();

        TitleBlock1.Text = msg;*/



        foreach (string str in strList)
        {
          ListViewItem item = new ListViewItem();
          item.Content = str;

          HistView.Items.Add(item);
        }
//        check1.Text = "seikou";
      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
        ListViewItem item = new ListViewItem();
        item.Content = "null";
//        check1.Text = "nani";
      }
    }

            private async void UpdateFavorite()
    {
      FavoriteView.Items.Clear();

      String filePath = "FavoriteData.csv";

      // write file
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;

      try
      {
        StorageFile file = await localFolder.GetFileAsync(filePath);

        IList<String> strList = await FileIO.ReadLinesAsync(file);

        foreach (String str in strList)
        {
          ListViewItem item = new ListViewItem();


          item.Content = str;

          FavoriteView.Items.Add(item);
        }
//        check1.Text = "seikou";
      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
        ListViewItem item = new ListViewItem();
        item.Content = "null";
//        check1.Text = "nani";
      }
    }

    


    /// <summary>
    /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
    /// 再作成する場合は、保存状態も指定されます。
    /// </summary>
    /// <param name="sender">
    /// イベントのソース (通常、<see cref="NavigationHelper"/>)
    /// </param>
    /// <param name="e">このページが最初に要求されたときに
    /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたナビゲーション パラメーターと、
    /// 前のセッションでこのページによって保存された状態の辞書を提供する
    /// イベント データ。ページに初めてアクセスするとき、状態は null になります。</param>
    private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
    {
      UpdateHistory();
      UpdateFavorite();
      // TODO: バインド可能なグループのコレクションを this.DefaultViewModel["Groups"] に割り当てます
//      textBlock1.Text = e.NavigationParameter.ToString();


/*      string r_pass = null;
      Data.PickPass p1 = new Data.PickPass(ref r_pass);
      if (r_pass == null) { check1.Text = "NULL"; }
      else { check1.Text = r_pass; }*/

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
/*
    private void backButton_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(InputPassPage));
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(InputPassPage));
    }
    */
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
//      SettingsPane.GetForCurrentView().CommandsRequested -= MainGroupPage_CommandsRequested;
      this.Frame.Navigate(typeof(InputPassPage));
    }

    private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
    {

//      System.Uri URL = ;

      if (HistView.SelectedItems.Count == 0)
        //処理を抜ける
        return;

      ListViewItem itemx = new ListViewItem();

      //1番目に選択されれいるアイテムをitemxに格納
//      itemx = (ListViewItem)HistView.SelectedItems[0];

      object msg;

      itemx = (Windows.UI.Xaml.Controls.ListViewItem)HistView.SelectedItems[0];
//      check1.Text = (string)msg;

//  var paramArray = msg.Split('\t');
/*  var item = new AtmarkItReader.DataModel.FeedItem(
                                            title: paramArray[0],
                                            link: paramArray[1],
                                            pubDate: paramArray[2]
                                          );*/
/*

      //選択されているアイテムを取得する
      string msg;
      msg = "郵便番号は " + itemx.Text + "\n";
      msg += "住所は " + itemx.SubItems[1].Text + "\n";
      msg += "氏名は " + itemx.SubItems[2].Text;

      MessageBox.Show(msg);}*/

//  string url = paramArray[0];



//      check1.Text = itemx;
//      this.Frame.Navigate(typeof(Browser.Browser3),msg);
    }

    private void Browser1_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Browser.Browser3));
    }

    private void Browser2_Tapped(object sender, TappedRoutedEventArgs e)
    {

    }

    private void ButtonPass_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(InputPassPage));
    }
/*
    private void backButton_Click(object sender, RoutedEventArgs e)
    {
      SettingsPane.GetForCurrentView().CommandsRequested -= MainGroupPage_CommandsRequested;
    }*/
  }
}
