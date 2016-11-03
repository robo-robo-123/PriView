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
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Activation;
using Microsoft.Advertising;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace PriView.Browser
{
  /// <summary>
  /// 多くのアプリケーションに共通の特性を指定する基本ページ。
  /// </summary>
  public sealed partial class Browser1 : Page
  {

    //private NavigationHelper navigationHelper;
    private ObservableDictionary defaultViewModel = new ObservableDictionary();
    public ViewModel.GeneralViewModel ViewModel { get; } = new ViewModel.GeneralViewModel();

    public int flag { set; get; }

    //ObservableCollection<string> stock;
    List<string> favorites;
    List<string> favorites_load;

    ObservableCollection<string> histories;
    Logic.PopulateHistories s1 = new Logic.PopulateHistories();
    Logic.PopulateFavorites s2 = new Logic.PopulateFavorites();


    public Browser1()
    {
      this.InitializeComponent();

//      TitleBlock.Text = "読み込み中";
      //stock = new ObservableCollection<string>();

      favorites = new List<string>();
      favorites_load = new List<string>();
      histories = new ObservableCollection<string>();

      //var p1 = new Logic.HistoryDataStore(ref histories);
      //cvs1.Source = histories;

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
      e.Handled = true;
      try
      {
        this.webView1.GoBack();
      }
        catch (Exception ex)
      {
        return;
      }
    }


    private void pageRoot_Loaded(object sender, RoutedEventArgs e)
    {
      s1.populateHistories();

      var p1 = new Logic.HistoryDataStore(ref histories);
      cvs1.Source = histories;

      //var s2 = new Logic.PopulateFavorites();
      s2.populateFavorites();

      var p2 = new Logic.FavoriteDataStore(ref favorites_load);
      cvs2.Source = favorites_load;

    }

    /// このセクションに示したメソッドは、NavigationHelper がページの
    /// ナビゲーション メソッドに応答できるようにするためにのみ使用します。
    /// 
    /// ページ固有のロジックは、
    /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
    /// および <see cref="GridCS.Common.NavigationHelper.SaveState"/> のイベント ハンドラーに配置する必要があります。
    /// LoadState メソッドでは、前のセッションで保存されたページの状態に加え、
    /// ナビゲーション パラメーターを使用できます。

      //最後に保存したページを、セーブするようにする
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      ViewModel.Last_url = webView1.Source.ToString();

    }

    //page読み込み時
    //最後に表示したページを保存して、それを読み込ませるようにする
    //失敗したら、デフォルトのページ
    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
      try
      {
        //this.webView1.Navigate(ViewModel.Default_url);
        UrlBox.Text = ViewModel.Last_url;
        await NavigateAsync();
        //this.webView1.Navigate(ViewModel.Last_url);
      }
      catch(Exception ex)
      {
        UrlBox.Text = ex.Message;
      }

    }


    /*
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
    */



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


    //web
    void webView1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
    {
      string url = "";
      try { url = args.Uri.ToString(); }
      finally
      {
        URIBlock.Text = url;
        UrlBox.Text = url;
      }
    }


   async void webView1_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
   {
       string url = (args.Uri != null) ? args.Uri.ToString() : "<null>";

       String filePath = "HistoryData.csv";
       //String filePath2 = "LastViewData.csv";

       // write file
       StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
       StorageFile file = await roamingFolder.CreateFileAsync(filePath, CreationCollisionOption.OpenIfExists);
      //       StorageFile file2 = await roamingFolder.CreateFileAsync(filePath2,
      //  CreationCollisionOption.OpenIfExists);
      try
      {
        await FileIO.AppendTextAsync(file, (string)webView1.DocumentTitle + "\t" + url + "\t" + DateTime.Now + "\n");
        //       await FileIO.AppendTextAsync(file, (string)webView1.DocumentTitle + "\t" + url + "\t" + DateTime.Now + "\n");
      }
      catch (Exception ex) { }

      //毎回やるっておかしいよね？
      //var s1 = new Logic.PopulateHistories();
       s1.populateHistories2((string)webView1.DocumentTitle + "\t" + url + "\t" + DateTime.Now + "\n");

      var p1 = new Logic.HistoryDataStore(ref histories);
      cvs1.Source = histories;

      /*
      var p2 = new Logic.FavoriteDataStore(ref favorites_load);
      cvs2.Source = favorites_load;

       var s2 = new Logic.PopulateLastview();
       s2.populateLastview();
       */
      ViewModel.Last_url = webView1.Source.ToString();


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


    //MVVM使って、登録したものを読み込ませるだけにする
    async private void buttonHome_Tapped(object sender, TappedRoutedEventArgs e)
    {
      try
      {
        UrlBox.Text = ViewModel.Default_url;
      }
      catch(Exception ex)
      {
        UrlBox.Text = "http://yahoo.co.jp/";
      }
      await NavigateAsync();
    }

    //lock画面へ移動
    private void ButtonPass_Tapped(object sender, TappedRoutedEventArgs e)
    {
      Frame rootFrame = Window.Current.Content as Frame;
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
        StorageFile file = await roamingFolder.CreateFileAsync(filePath, CreationCollisionOption.OpenIfExists);

        await FileIO.AppendTextAsync(file, (string)webView1.DocumentTitle + "\t" + URIBlock.Text + "\t" + DateTime.Now + "\n");

        s2.populateFavorites2((string)webView1.DocumentTitle + "\t" + URIBlock.Text + "\t" + DateTime.Now + "\n");
        var p2 = new Logic.FavoriteDataStore(ref favorites_load);
        cvs2.Source = favorites_load;

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

    private async void GridView_ItemClick(object sender, ItemClickEventArgs e)
    {
      var str = e.ClickedItem.ToString();
      string[] msg1 = str.Split('\n');
      UrlBox.Text = msg1[1];
      await NavigateAsync();
    }

    //setting_page
    private void settingButton__Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Setting.SettingHubPage));
    }

    //share
    //MVVM使って移動させる。
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
