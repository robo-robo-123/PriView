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
using Windows.Storage;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください

namespace PriView.Setting
{
  public sealed partial class GeneralSettingsFlyout : Page
  {

    public ViewModel.GeneralViewModel ViewModel { get; } = new ViewModel.GeneralViewModel();


    public GeneralSettingsFlyout()
    {
      this.InitializeComponent();

     // UrlComboBox.Item.Add("http://yahoo.co.jp");
     // UrlComboBox.Item.Add("http://google.com");

      UrlComboBox.SelectionChanged += ComboBox_SelectionChanged;

    }


    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // Add code to perform some action here.
      var selectedItem = (ComboBoxItem)UrlComboBox.SelectedItem;
      if (selectedItem == null) return;
      string contentString = (string)selectedItem.Content;

      Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
      roamingSettings.Values["exampleSetting"] = contentString;

    }

    async private void historyDeleteButton_Click(object sender, RoutedEventArgs e)
    {
      this.dlg1.Title = "履歴の削除";
     dlgText.Text = "履歴の完全削除を行いますか？（削除はアプリの再起動後に有効になります。）";
      var result = await this.dlg1.ShowAsync();
      if (result == ContentDialogResult.Primary)
      {
        //  System.Diagnostics.Debug.WriteLine("Primary");
        String filePath = "HistoryData.csv";
        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        try
        {
          StorageFile file = await roamingFolder.GetFileAsync(filePath);
          await file.DeleteAsync();
        }
        catch
        {

        }

      }
      else if (result == ContentDialogResult.Secondary)
      {
      //  System.Diagnostics.Debug.WriteLine("Secondary");
      }
      else
      {
       // System.Diagnostics.Debug.WriteLine("None");
      }
    }

    async private void favoriteDeleteButton_Click(object sender, RoutedEventArgs e)
    {

      this.dlg1.Title = "お気に入りの削除";
      dlgText.Text = "お気に入りの完全削除を行いますか？（削除はアプリの再起動後に有効になります。）";
      var result = await this.dlg1.ShowAsync();
      if (result == ContentDialogResult.Primary)
      {
        String filePath = "FavoriteData.csv";
        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        try
        {
          StorageFile file = await roamingFolder.GetFileAsync(filePath); //ファイルがない場合の処理
          await file.DeleteAsync();
        }
        catch
        {

        }
      }
      else if (result == ContentDialogResult.Secondary)
      {
      }
      else
      {
      }
    }

    /*
    private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
    {
      if(historySwitch.IsOn == false)
      {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        localSettings.Values["historySetting"] = "false";
      }
    }
    */
  }
}
