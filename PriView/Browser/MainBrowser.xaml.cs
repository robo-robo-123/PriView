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
using Windows.Storage;
using System.Collections.ObjectModel;
using Windows.UI.Core;

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

      var p1 = new Logic.HistoryDataStore(ref histories);
      cvs1.Source = histories;

      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += navigationHelper_LoadState;
      this.navigationHelper.SaveState += navigationHelper_SaveState;

      webView1.ContentLoading += webView1_ContentLoading;
      webView1.NavigationStarting += webView1_NavigationStarting;
      this.webView1.NavigationCompleted += webView1_NavigationCompleted;


      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (Frame.CanGoBack)
        {
          Frame.GoBack();
          args.Handled = true;
        }
      };
      //      
      //      this.Frame.Navigate(typeof(InputPassPage)/*, (System.Uri)e.PageState["PageURL"]*/);
    }



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

   void webView1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
    {
      string url = "";
      try { url = args.Uri.ToString(); }
      finally
      {

        URIBlock.Text = url;
        UrlBox.Text = url;
        //appendLog(String.Format("Starting navigation to: \"{0}\".\n", url));
        pageIsLoading = true;



      }
    }


   async void webView1_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
   {

/*     Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
     string Flag = (string)localSettings.Values["exampleSetting"];

     if (Flag == "false") { return; }

     else
     {*/
       string url = (args.Uri != null) ? args.Uri.ToString() : "<null>";
       //            appendLog(String.Format("Loading content for \"{0}\".\n", url));

       String filePath = "HistoryData.csv";
       String filePath2 = "LastViewData.csv";

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

      //       var s2 = new Logic.PopulateLastview();
      //       s2.populateLastview();
      //     }
      // PopulateHistories();
      //      UrlBox.Text = "yomikomiOK!";

      //       String filePath = "LastViewData.csv";
      // write file
      //       StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
      StorageFile file2 = await roamingFolder.CreateFileAsync(filePath2,
           CreationCollisionOption.OpenIfExists);
       string data = (string)webView1.DocumentTitle + "\t" + URIBlock.Text + "\t" + DateTime.Now + "\n";
       await FileIO.WriteTextAsync(file2, data);

       var s2 = new Logic.PopulateLastview();
       s2.populateLastview();

   }

    /*
   void InputPassPage_SizeChanged(object sender, SizeChangedEventArgs e)
   {

     if (e.NewSize.Width < 000)
     {
       //        var Center = new HorizontalAlignment();

       

       adGrid.Width = 250;
       adGrid.Height = 125;
       adGrid.Margin = new Thickness(0, 0.0, 0, 500);
       adGrid.HorizontalAlignment = HorizontalAlignment.Center;


 //      numberGrid.HorizontalAlignment = HorizontalAlignment.Center;


     }
     else
     {

       adGrid.Width = 160;
       adGrid.Height = 600;
       adGrid.Margin = new Thickness(0, 0, 0, 0);
       adGrid.HorizontalAlignment = HorizontalAlignment.Left;

//       numberGrid.HorizontalAlignment = HorizontalAlignment.Right;
     }

   }
   */
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


       private async void webView1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
       {

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

      var p2 = new Logic.FavoriteDataStore(ref favorites_load);
      cvs2.Source = favorites_load;

      if ( e.NavigationParameter != null)
      {
      string msg = e.NavigationParameter.ToString();

      string[] msg2 = msg.Split('\n');

        try
        {
          UrlBox.Text = msg2[1];
        }
        catch
        {
          UrlBox.Text = "";
        }
        
        await NavigateAsync();

      }


    }


/*    void GeneralSettingsFlyout_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
    {
      var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();

      //      if (args.Request.ApplicationCommands.Count > 0) { return; }
      SettingsCommand updateSetting =
          new SettingsCommand("Settings", setting, (handler) =>
          {
            GeneralSettingsFlyout GeneralSettingsFlyout = new GeneralSettingsFlyout();
            GeneralSettingsFlyout.Show();

          });

      args.Request.ApplicationCommands.Add(updateSetting);

    }

    void MainGroupPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
    {
      var resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
      string setting;
      setting = resourceLoader.GetString("PassRegistration");
      string generalsetting;
      generalsetting = resourceLoader.GetString("GeneralSetting");
      if (args.Request.ApplicationCommands.Count > 0) {  return; }
      SettingsCommand updateSetting =
          new SettingsCommand("AppUpdateSettings", setting, (handler) =>
          {
            MainSettingsFlyout updatesFlyout = new MainSettingsFlyout();
            updatesFlyout.Hide();
          });
      args.Request.ApplicationCommands.Add(updateSetting);

      SettingsCommand GeneralSetting =
    new SettingsCommand("GeneralSettings", generalsetting, (handler) =>
    {
      GeneralSettingsFlyout GeneralSettingsFlyout = new GeneralSettingsFlyout();
      GeneralSettingsFlyout.Show();
    });
      args.Request.ApplicationCommands.Add(GeneralSetting);

    }
*/
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
      Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
      string URL = (string)localSettings.Values["exampleSetting"];

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
      catch(Exception ex)
      { }



      navigationHelper.OnNavigatedTo(e);
    }

    async protected override void OnNavigatedFrom(NavigationEventArgs e)
    {






      navigationHelper.OnNavigatedFrom(e);
      //SettingsPane.GetForCurrentView().CommandsRequested -= MainGroupPage_CommandsRequested;
    }

    #endregion

    async private void buttonHome_Tapped(object sender, TappedRoutedEventArgs e)
    {
/*      System Uri aiueo = 
      webView1.Source = (System Uri)"http://yahoo.co.jp/";*/
/*      System.Uri URI = webView1.Source;
      this.Frame.Navigate(typeof(MainHubPage), "test");*/
//      this.Frame.Navigate(typeof(MainHubPage));

//      UrlBox.Text = "http://yahoo.co.jp/";

      Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
      string URL = (string)localSettings.Values["exampleSetting"];

      UrlBox.Text = URL;

      await NavigateAsync();
    }

    private void ButtonPass_Tapped(object sender, TappedRoutedEventArgs e)
    {
      string flag ;
      flag = URIBlock.Text;


      //      
      Frame rootFrame = Window.Current.Content as Frame;
      rootFrame.Navigate(typeof(InputPassPage),flag);
    }

   async private void buttonFavorite_Tapped(object sender, TappedRoutedEventArgs e)
    {

/*     if(GeneralSettingsFlyout.historySwitch.IsOn == true)
     { }*/
      
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



   private void AppBarSeparator_Tapped(object sender, TappedRoutedEventArgs e)
   {

   }

   private async void buttonGo_Tapped(object sender, TappedRoutedEventArgs e)
   {
     await NavigateAsync();
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

   private async void textBox1_KeyDown(object sender, KeyRoutedEventArgs e)
   {
     if (e.Key != Windows.System.VirtualKey.Enter)
       return;

     try
     {  await NavigateAsync();
     
     }
     catch(Exception ex)
     { return; }
    
   }

   private void Button_Click(object sender, RoutedEventArgs e)
   {

   }

   private void HomeButton_Tapped(object sender, TappedRoutedEventArgs e)
   {

   }

  async  private void buttonMenu_Tapped(object sender, TappedRoutedEventArgs e)
   {

/*     String filePath = "LastViewData.csv";
     // write file
     StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
     StorageFile file = await roamingFolder.CreateFileAsync(filePath,
         CreationCollisionOption.OpenIfExists);
     string data = (string)webView1.DocumentTitle + "\t" + URIBlock.Text + "\t" + DateTime.Now + "\n";
     await FileIO.WriteTextAsync(file, data);

     var s2 = new Logic.PopulateLastview();
     s2.populateLastview();*/

     //this.Frame.Navigate(typeof(HubPage));
   }

    private async void webOpen(string url)
    {
      var uri = new Uri(url);
      webView1.Source = uri;

    }


    private void buttonHistory_Tapped(object sender, TappedRoutedEventArgs e)
    {

    }

    private void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.Frame.Navigate(typeof(Browser.MainBrowser), e.ClickedItem);
    }

    private void webView1_NewWindowRequested_1(WebView sender, WebViewNewWindowRequestedEventArgs args)
    {
      args.Handled = true;
      webOpen(args.Uri.AbsoluteUri);
    }

    private void settingButton__Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Setting.SettingHubPage));
    }
  }
}
