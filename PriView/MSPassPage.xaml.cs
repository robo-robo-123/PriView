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
using Windows.UI.Popups;
using Windows.Security.Credentials;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.ApplicationModel.Activation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace PriView
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MSPassPage : Page
  {
    public MSPassPage()
    {
      this.InitializeComponent();
    }

    private void OnLaunched(LaunchActivatedEventArgs args)
    {
      Path_Auth();

    }

    private async void Path_Auth()
    {
      // check if KeyCredentialManager can be used
      if (await KeyCredentialManager.IsSupportedAsync() == false)
      {
        await (new MessageDialog("KeyCredentialManager not supported")).ShowAsync();
        return;
      }

      // retrieve private key for sign
      KeyCredentialRetrievalResult res =
        await KeyCredentialManager.OpenAsync("key1");

      if (res.Status == KeyCredentialStatus.Success)
      {
        //
        // If it is present, we request sign for some data
        //

        // convert string to binary buffer
        var inputbuf =
          CryptographicBuffer.ConvertStringToBinary(
            "Test Data 1", BinaryStringEncoding.Utf8);

        // sign using retrieved private key
        // (Windows Hello is diplayed here !)
        KeyCredentialOperationResult signRes = await res.Credential.RequestSignAsync(inputbuf);
        //KeyCredentialAttestationResult signRes = await res.Credential.GetAttestationAsync();

        /*
        // get the base64 encoded data to cryptographically sign
        string base64encSignature =
          CryptographicBuffer.EncodeToBase64String(signRes.Result);
        await (new MessageDialog(base64encSignature)).ShowAsync();
        */
        if (signRes.Status == KeyCredentialStatus.Success)
        {
          // ナビゲーション フレームを使用して前のページに戻ります
          if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
          else if(this.Frame != null)
            //this.Frame.Navigate(typeof(Browser.MainBrowser));
            this.Frame.Navigate(typeof(Browser.Browser1));

        }
        else
        {
          // get the base64 encoded data to cryptographically sign
          //string base64encSignature = CryptographicBuffer.EncodeToBase64String(signRes.Result);
          await (new MessageDialog("キャンセルしました")).ShowAsync();
          //await (new MessageDialog(base64encSignature)).ShowAsync();
        }
      }
      else if (res.Status == KeyCredentialStatus.NotFound)
      {
        //
        // If the credential is not found, we create it.
        //

        // Create the credential
        // (Windows Hello is diplayed here !)
        KeyCredentialRetrievalResult createRes =
          await KeyCredentialManager.RequestCreateAsync("key1",
          KeyCredentialCreationOption.ReplaceExisting);

        if (createRes.Status == KeyCredentialStatus.Success)
        {
          // ナビゲーション フレームを使用して前のページに戻ります
          if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
          else if (this.Frame != null)
            //this.Frame.Navigate(typeof(Browser.MainBrowser));
            this.Frame.Navigate(typeof(Browser.Browser1));

        }
        else
        {
          // get the base64 encoded data to cryptographically sign
          //string base64encSignature = CryptographicBuffer.EncodeToBase64String(signRes.Result);
          await (new MessageDialog("キャンセルしました")).ShowAsync();
          //await (new MessageDialog(base64encSignature)).ShowAsync();
        }

        /*
        // if the status is success, retrieve the public key.
        if (createRes.Status == KeyCredentialStatus.Success)
        {
          var pubKey = createRes.Credential.RetrievePublicKey();
          AsymmetricKeyAlgorithmProvider alg =
            AsymmetricKeyAlgorithmProvider.OpenAlgorithm(
              AsymmetricAlgorithmNames.RsaPkcs1);
          CryptographicKey ckey = alg.ImportPublicKey(pubKey);
          // for ex, CryptographicEngine.VerifySignature() using this CryptographicKey
          // (This time, just showing the keysize)
          var dialog = new MessageDialog(string.Format("Key size is {0}", ckey.KeySize));
          await dialog.ShowAsync();
        }
        */
      }
      else
      {
        await (new MessageDialog("Unknown error !")).ShowAsync();
      }
    }

    private void PassButton_Click(object sender, RoutedEventArgs e)
    {
      Path_Auth();
    }
  }
}
