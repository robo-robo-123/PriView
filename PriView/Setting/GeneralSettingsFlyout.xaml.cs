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

      Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
      localSettings.Values["exampleSetting"] = contentString;

    }

    async private void historyDeleteButton_Click(object sender, RoutedEventArgs e)
    {
      String filePath = "HistoryData.csv";
      StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        StorageFile file = await roamingFolder.GetFileAsync(filePath);
        await file.DeleteAsync();
      
    }

    async private void favoriteDeleteButton_Click(object sender, RoutedEventArgs e)
    {
      String filePath = "FavoriteData.csv";
      StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
      StorageFile file = await roamingFolder.GetFileAsync(filePath);
      await file.DeleteAsync();
    }

    private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
    {
      if(historySwitch.IsOn == false)
      {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        localSettings.Values["historySetting"] = "false";
      }
    }
  }
}
