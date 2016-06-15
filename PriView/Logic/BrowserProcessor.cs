using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriView.Logic
{
  internal class BrowserProcessor
  {
    internal static PriView.Data.BrowsersData Add(DatasItems BrowsersData, Data.BrowsersData browsersData)
    {

      var newBrowser = new Data.Browser();
      browsersData.Browsers.Add(newBrowser);

/*      foreach (var DatasItems in BrowsersData)
      {
        newBrowser.Items.Add(
          new Data.BrowserItem(
            DatasItems.Title, // 記事タイトル
            DatasItems.URL, // 記事のURL
            DatasItems.Date//("yyyy/MM/dd HH:mm:ss") // 記事の発行日時
            )
          );
      }*/


        newBrowser.Items.Add(
          new Data.BrowserItem(
            BrowsersData.Title, // 記事タイトル
            BrowsersData.URL, // 記事のURL
            BrowsersData.Date//("yyyy/MM/dd HH:mm:ss") // 記事の発行日時
            )
          );

        return browsersData;


    }

  }

  class Datas
  {
    
    public  string MainTitle;

    string T; System.Uri U; System.DateTime D;

    

    public List<DatasItems> Items = new List<DatasItems>();
//   DatasItems Items = new DatasItems(T,U,D);

  }
   public class DatasItems
   {      

    public  string Title; 
    public  string URL;
    public System.DateTime Date;

    public DatasItems(string T, string U, System.DateTime D)
    {
      Title = T;
      URL = U;
      Date = D;
    }
  }

}
