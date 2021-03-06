﻿using PriView.Common;
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
using Windows.Storage;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Activation;
using Microsoft.Advertising;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace PriView.Browser
{
  /// <summary>
  /// 多くのアプリケーションに共通の特性を指定する基本ページ。
  /// </summary>
  public sealed partial class MainBrowser : Page
  {

    private NavigationHelper navigationHelper;
    private ObservableDictionary defaultViewModel = new ObservableDictionary();
    public int flag { set; get; }

    ObservableCollection<string> stock;
    List<string> favorites;
    List<string> favorites_load;

    ObservableCollection<string> histories;

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


    public MainBrowser()
    {
      this.InitializeComponent();

//      TitleBlock.Text = "読み込み中";
      stock = new ObservableCollection<string>();
      favorites = new List<string>();
      favorites_load = new List<string>();
      histories = new ObservableCollection<string>();

      //var p1 = new Logic.HistoryDataStore(ref histories);
      //cvs1.Source = histories;

      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += navigationHelper_LoadState;
      this.navigationHelper.SaveState += navigationHelper_SaveState;

      webView1.ContentLoading += webView1_ContentLoading;
      webView1.NavigationStarting += webView1_NavigationStarting;
      this.webView1.NavigationCompleted += webView1_NavigationCompleted;

      SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

      /*
            SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
            {
              if (Frame.CanGoBack)
              {
                Frame.GoBack();
                args.Handled = true;
              }
            };
      */
    }


    //event
    private void App_BackRequested(object sender, BackRequestedEventArgs e)
    {
      try
      {
        this.webView1.GoBack();
      }
        catch (Exception ex)
      {
        return;
      }
      e.Handled = true;
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
    async private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
    {

      var p1 = new Logic.HistoryDataStore(ref histories);
      cvs1.Source = histories;

      var s2 = new Logic.PopulateFavorites();
      s2.populateFavorites();

      var s1 = new Logic.PopulateHistories();
      s1.populateHistories();

      var p2 = new Logic.FavoriteDataStore(ref favorites_load);
      cvs2.Source = favorites_load;

      if (e.NavigationParameter != null)
      {
        string msg = e.NavigationParameter.ToString();

        string[] msg2 = msg.Split('\n');

        if (UrlBox.Text == "")
        {
          try
          {
            UrlBox.Text = msg2[1];
          }
          catch
          {
            string URL = "";
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            try
            {
              UrlBox.Text = (string)roamingSettings.Values["exampleSetting"];
            }
            catch (Exception ex)
            {
              UrlBox.Text = "http://yahoo.co.jp/";
            }
            //UrlBox.Text = URL;
            //UrlBox.Text = "";
          }
          await NavigateAsync();
        }

      }

      else
      {
        if (UrlBox.Text == "")
        {
          Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
          try
          {
            UrlBox.Text = (string)roamingSettings.Values["exampleSetting"];
          }
          catch (Exception ex)
          {
            UrlBox.Text = "http://yahoo.co.jp/";
          }
          await NavigateAsync();
        }
      }
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
      e.PageState["PageURL"] = webView1.Source;
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
      //     SettingsPane.GetForCurrentView().CommandsRequested += MainGroupPage_CommandsRequested;
      try
      {
        Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
        string URL = (string)roamingSettings.Values["exampleSetting"];

        Uri newUri;
        Uri.TryCreate(URL, UriKind.Absolute, out newUri);

        if (this.UrlBox.Text == "")
        {
          //  this.webView1.Navigate(newUri);
          //webView1.Source = URL;
        }
        else
        {
          //  Uri.TryCreate(this.UrlBox.Text, UriKind.Absolute, out newUri);
          // this.webView1.Navigate(newUri);
        }
      }
      catch (Exception ex)
      { }

      navigationHelper.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {

      navigationHelper.OnNavigatedFrom(e);
      //SettingsPane.GetForCurrentView().CommandsRequested -= MainGroupPage_CommandsRequested;
    }

    #endregion


    /*
    private bool _pageIsLoading;
    bool pageIsLoading
    {
      get { return _pageIsLoading; }
      set
      {
        _pageIsLoading = value;
    //    goButton.Content = (value ? "Stop" : "Go");
    //    progressRing1.Visibility = (value ? Visibility.Visible : Visibility.Collapsed);
        if (!value)
        {
     //     navigateBack.IsEnabled = webView1.CanGoBack;
     //     navigateForward.IsEnabled = webView1.CanGoForward;
        }
      }
    }
    */


    //web
    void webView1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
    {
      string url = "";
      try { url = args.Uri.ToString(); }
      finally
      {
        URIBlock.Text = url;
        UrlBox.Text = url;
        //appendLog(String.Format("Starting navigation to: \"{0}\".\n", url));
        //pageIsLoading = true;
      }
    }


   async void webView1_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
   {
       string url = (args.Uri != null) ? args.Uri.ToString() : "<null>";

       String filePath = "HistoryData.csv";
       //String filePath2 = "LastViewData.csv";

       // write file
       StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
       StorageFile file = await roamingFolder.CreateFileAsync(filePath,
           CreationCollisionOption.OpenIfExists);
      //       StorageFile file2 = await roamingFolder.CreateFileAsync(filePath2,
      //  CreationCollisionOption.OpenIfExists);
      try
      {
        await FileIO.AppendTextAsync(file, (string)webView1.DocumentTitle + "\t" + url + "\t" + DateTime.Now + "\n");
        //       await FileIO.AppendTextAsync(file, (string)webView1.DocumentTitle + "\t" + url + "\t" + DateTime.Now + "\n");
      }
      catch (Exception ex) { }

      var s1 = new Logic.PopulateHistories();
       s1.populateHistories();

      var p1 = new Logic.HistoryDataStore(ref histories);
      cvs1.Source = histories;

      var p2 = new Logic.FavoriteDataStore(ref favorites_load);
      cvs2.Source = favorites_load;

       var s2 = new Logic.PopulateLastview();
       s2.populateLastview();

   }

    private void webView1_NewWindowRequested_1(WebView sender, WebViewNewWindowRequestedEventArgs args)
    {
      args.Handled = true;
      webOpen(args.Uri.AbsoluteUri);
    }

    private void webOpen(string url)
    {
      var uri = new Uri(url);
      webView1.Source = uri;
    }

    private void webView1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
    {
    }



    //data
    async private void PopulateHistories()
       {
//         stock = new List<string>();
         try
         {
           String filePath = "HistoryData.csv";

           StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
             StorageFile file = await roamingFolder.GetFileAsync(filePath);
           IList<String> strList = await FileIO.ReadLinesAsync(file);
           foreach (String str in strList)
           {

             string[] msg1 = str.Split('\t');
             string msg2 = string.Join("\n", msg1);

             stock.Add(msg2);
           }
           var p1 = new Logic.HistoryDataStore(stock);

         }
         catch (FileNotFoundException ex)
         {
           // ファイル無し
           stock.Add("null");
         }
       }

       async private void PopulateFavorites()
       {
         try
         {

         String filePath = "FavoriteData.csv";
         // write file
         StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;

           StorageFile file = await roamingFolder.GetFileAsync(filePath);
           IList<String> strList = await FileIO.ReadLinesAsync(file);
           foreach (String str in strList)
           {
             string[] msg1 = str.Split('\t');
             string msg2 = string.Join("\n", msg1);
             favorites.Add(msg2);
           }
           var p1 = new Logic.FavoriteDataStore(favorites);
         }
         catch (FileNotFoundException ex)
         {
        // ファイル無し
        stock.Add("null");
        //ListViewItem item = new ListViewItem();
        //item.Content = "null";
        //favorites.Add("null");
      }

       }



    async private void buttonHome_Tapped(object sender, TappedRoutedEventArgs e)
    {
      /*      System Uri aiueo = 
            webView1.Source = (System Uri)"http://yahoo.co.jp/";*/
      /*      System.Uri URI = webView1.Source;
            this.Frame.Navigate(typeof(MainHubPage), "test");*/
      //      this.Frame.Navigate(typeof(MainHubPage));

      //      UrlBox.Text = "http://yahoo.co.jp/";

      string URL = "";
      Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
      try
      {
        UrlBox.Text = (string)roamingSettings.Values["exampleSetting"];
      }
      catch(Exception ex)
      {
        UrlBox.Text = "http://yahoo.co.jp/";
      }
      //UrlBox.Text = URL;

      await NavigateAsync();
    }

    private void ButtonPass_Tapped(object sender, TappedRoutedEventArgs e)
    {
      //string flag ;
      //flag = URIBlock.Text;

      Frame rootFrame = Window.Current.Content as Frame;
      //rootFrame.Navigate(typeof(InputPassPage),flag);
      rootFrame.Navigate(typeof(MSPassPage));
    }

   async private void buttonFavorite_Tapped(object sender, TappedRoutedEventArgs e)
    {

      this.dlg1.Title = "お気に入りの登録";
      dlgText.Text = "お気に入りの登録を行いますか？";
      var result = await this.dlg1.ShowAsync();
      if (result == ContentDialogResult.Primary)
      {
        String filePath = "FavoriteData.csv";

        // write file
        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        StorageFile file = await roamingFolder.CreateFileAsync(filePath,
            CreationCollisionOption.OpenIfExists);

        await FileIO.AppendTextAsync(file, (string)webView1.DocumentTitle + "\t" + URIBlock.Text + "\t" + DateTime.Now + "\n");

        // PopulateFavorites();
        var s2 = new Logic.PopulateFavorites();
        s2.populateFavorites();
      }
      else if (result == ContentDialogResult.Secondary)
      {
      }
      else
      {
      }


    }


    //page遷移
   private async void buttonGo_Tapped(object sender, TappedRoutedEventArgs e)
   {
     await NavigateAsync();
   }

    private async void textBox1_KeyDown(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != Windows.System.VirtualKey.Enter)
        return;
      try
      {
        await NavigateAsync();
      }
      catch (Exception ex)
      { return; }
    }

    private async System.Threading.Tasks.Task NavigateAsync()
   {
     Uri newUri;
     if (Uri.TryCreate(this.UrlBox.Text, UriKind.Absolute, out newUri)
             && newUri.Scheme.StartsWith("http"))
     {
       this.webView1.Navigate(newUri);
     }
     else if (this.UrlBox.Text == "")
      {

      }
     else
     {
       //（エラー処理）
       string errMsg = @"正しいURLを記入してください";
       await new Windows.UI.Popups.MessageDialog(errMsg).ShowAsync();
     }
   }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.Frame.Navigate(typeof(Browser.MainBrowser), e.ClickedItem);
    }

    //setting_page
    private void settingButton__Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Setting.SettingHubPage));
    }

    //share
    private void ShareButton_Tapped(object sender, TappedRoutedEventArgs e)
    {
      ShareSourceLoad();
      DataTransferManager.ShowShareUI();
    }

    private void ShareSourceLoad()
    {
      DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
      dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
    }

    private void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
    {
      DataRequest request = e.Request;
      request.Data.Properties.Title = "Share Web pages";
      request.Data.Properties.Description = "Share of Web page.";
      request.Data.SetText((string)webView1.DocumentTitle + "\t" + URIBlock.Text);
    }

  }
}
