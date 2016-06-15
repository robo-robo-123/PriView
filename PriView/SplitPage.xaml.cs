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

namespace PriView
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class SplitPage : Page
  {
    public SplitPage()
    {
      this.InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {

    // アプリ開始時には、ページ【1】を表示する
    RadioButton1.IsChecked = true;
    }

    // ハンバーガーメニューで［ページ【1】］が新たに選択された
    private void RadioButton1_Checked(object sender, RoutedEventArgs e)
    {
      MainContentFrame.Navigate(typeof(Browser.MainBrowser));
      Splitter.IsPaneOpen = false;
    }

    // ハンバーガーメニューで［ページ【2】］が新たに選択された
    private void RadioButton2_Checked(object sender, RoutedEventArgs e)
    {
     // MainContentFrame.Navigate(typeof(HubPage));
     // Splitter.IsPaneOpen = false;
    }

    // ハンバーガーメニューで［ページ【3】］が新たに選択された
    private void RadioButton3_Checked(object sender, RoutedEventArgs e)
    {
      MainContentFrame.Navigate(typeof(Setting.SettingHubPage));
      Splitter.IsPaneOpen = false;
    }

  }
}
