using PriView.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


// ハブ ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=321224 を参照してください

namespace PriView
{
  /// <summary>
  /// グループ化されたアイテムのコレクションを表示するページです。
  /// </summary>
  public sealed partial class MainHubPage : Page
  {
    private NavigationHelper navigationHelper;
    private ObservableDictionary defaultViewModel = new ObservableDictionary();

    List<string> histories;
    List<string> lastview;
    List<string> favorites;

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

    public MainHubPage()
    {
      this.InitializeComponent();


      favorites = new List<string>();
      histories = new List<string>();
      lastview = new List<string>();

      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += navigationHelper_LoadState;
      this.navigationHelper.SaveState += navigationHelper_SaveState;




//      ListView1.SelectionChanged += ListView1_SelectionChanged;


    }

    

   async private void PopulateHistories()
    {
      String filePath = "HistoryData.csv";
      // write file
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      try
      {
        StorageFile file = await localFolder.GetFileAsync(filePath);
        IList<String> strList = await FileIO.ReadLinesAsync(file);
        foreach (String str in strList)
        {
          string[] msg1 = str.Split('\t');
          string msg2 = string.Join("\n", msg1);
          histories.Add(msg2);
        }
      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
        histories.Add("null");
      }
    }

   async private void PopulateFavorites()
    {
      String filePath = "FavoriteData.csv";
      // write file
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      try
      {
        StorageFile file = await localFolder.GetFileAsync(filePath);
        IList<String> strList = await FileIO.ReadLinesAsync(file);
        foreach (String str in strList)
        {
          string[] msg1 = str.Split('\t');
          string msg2 = string.Join("\n", msg1);
         favorites.Add(msg2);
        }
      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
        favorites.Add("null");
      }

    }

   async private void OncePopulateHistories()
   {
     String filePath = "StockData.csv";
     // write file
     StorageFolder localFolder = ApplicationData.Current.LocalFolder;
     try
     {
       StorageFile file = await localFolder.GetFileAsync(filePath);
       IList<String> strList = await FileIO.ReadLinesAsync(file);
       foreach (String str in strList)
       {

         string[] msg1 = str.Split('\t');
         string msg2 = string.Join("\n", msg1);

         histories.Add(msg2);
       }
     }
     catch (FileNotFoundException ex)
     {
       // ファイル無し
       ListViewItem item = new ListViewItem();
       item.Content = "null";
       histories.Add("null");
     }
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
      // TODO: バインド可能なグループのコレクションを this.DefaultViewModel["Groups"] に割り当てます

      //      OncePopulateHistories();
//      cvs1.Source = histories;




    }

    private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
    {
//      lastview.Find()
//      e.PageState["greetingOutputText"] = ;
    }

//    OnWindowCreated();

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

/*      var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
      if (!settings.Values.ContainsKey("HistorySetting")
          || (settings.Values.ContainsKey("HistorySetting") && (bool)settings.Values["HistorySetting"] == false))
      {

        this.Frame.Navigate(typeof(MainHubPage));

        var p1 = new Logic.HistoryDataStore(ref histories);
        cvs1.Source = histories;

        var p2 = new Logic.FavoriteDataStore(ref favorites);
        cvs2.Source = favorites;

        var p3 = new Logic.LastviewDataStore(ref lastview);
        cvs3.Source = lastview;

      }

      else
      {

        var p3 = new Logic.LastviewDataStore(ref lastview);
        cvs3.Source = lastview;

        var p1 = new Logic.HistoryDataStore(ref histories);
        cvs1.Source = histories;

        var p2 = new Logic.FavoriteDataStore(ref favorites);
        cvs2.Source = favorites;


//      }

        SettingsPane.GetForCurrentView().CommandsRequested += MainGroupPage_CommandsRequested;
*/
      navigationHelper.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {

      navigationHelper.OnNavigatedFrom(e);

//      SettingsPane.GetForCurrentView().CommandsRequested -= MainGroupPage_CommandsRequested;


    }

    #endregion

    private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Browser.Browser3));
    }

    private void Panel1_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Browser.Browser3));
    }

    private void Button_Tapped(object sender, TappedRoutedEventArgs e)
    {

    }

    private void ButtonPass_Tapped(object sender, TappedRoutedEventArgs e)
    {
//      SettingsPane.GetForCurrentView().CommandsRequested -= MainGroupPage_CommandsRequested;
      this.Frame.Navigate(typeof(InputPassPage));
    }


    void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
    {
      HubSection section = e.Section;
      //var group = section.DataContext;
      //this.Frame.Navigate(typeof(SectionPage), ((SampleDataGroup)group).UniqueId);
      //↓【第6回】画面遷移先を修正する
//      var feed = section.DataContext as DataModel.Feed;
      this.Frame.Navigate(typeof(HistoryPage));
    }


    private void Browser1_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Browser.Browser3));

    }

    private void Browser2_Tapped(object sender, TappedRoutedEventArgs e)
    {
//     this.Frame.Navigate(typeof(MainGroupPage));
    }

    private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void ListView1_ItemClick(object sender, ItemClickEventArgs e)
    {

      this.Frame.Navigate(typeof(Browser.Browser3), e.ClickedItem);      
    }

    private void ListView0_ItemClick(object sender, ItemClickEventArgs e)
    {
      if(e.ClickedItem == null)
      {
        this.Frame.Navigate(typeof(Browser.Browser3));
      }
      else 
      { 
      this.Frame.Navigate(typeof(Browser.Browser3), e.ClickedItem);
      }
    }

    private void ListView2_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.Frame.Navigate(typeof(Browser.Browser3), e.ClickedItem);
    }

    private void ListViewItem_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {

    }



    private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

      GridView gv = sender as GridView;
      if (gv != null)
      {
        if (gv.SelectedItem != null)
        {
          // We have selected items so show the AppBar and make it sticky
          BottomAppBar.IsSticky = true;
          BottomAppBar.IsOpen = true;
        }
        else
        {
          // No selections so hide the AppBar and don't make it sticky any longer
          BottomAppBar.IsSticky = false;
          BottomAppBar.IsOpen = false;
        }
      }
    }

    private void HubSection_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(HistoryPage));
    }

    private void FavoriteHubSection_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(FavoritePage));
    }
  }
}
