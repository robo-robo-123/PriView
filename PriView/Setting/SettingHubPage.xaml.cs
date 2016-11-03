using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace PriView.Setting
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class SettingHubPage : Page
  {
    public SettingHubPage()
    {
      this.InitializeComponent();
      this.generalFrame.Navigate(typeof(Setting.GeneralSettingsFlyout));
      //this.passFrame.Navigate(typeof(Setting.MainSettingPage));
      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (Frame.CanGoBack)
        {
          Frame.GoBack();
          args.Handled = true;
        }
      };

    }

    /*
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);

  // システムの［戻る］ボタンに対応するイベントハンドラーを結び付ける
  Windows.UI.Core.SystemNavigationManager.GetForCurrentView()
    .BackRequested += MainPage_BackRequested;
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      base.OnNavigatingFrom(e);

  // システムの［戻る］ボタンに対応するイベントハンドラーを解除する
  Windows.UI.Core.SystemNavigationManager.GetForCurrentView()
    .BackRequested -= MainPage_BackRequested;
    }

    // システムの［戻る］ボタンが押された時のイベントハンドラー
    private void MainPage_BackRequested(object sender,
                  Windows.UI.Core.BackRequestedEventArgs e)
    {
      if (this.Frame.CanGoBack)
      {
        this.Frame.GoBack();
        e.Handled = true;
      }
    }
    */

  }
}
