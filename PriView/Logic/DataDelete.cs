using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Collections.ObjectModel;

namespace PriView.Logic
{
  class DeleteHistories
  {

    async public void deleteHistories()
    {
      ObservableCollection<string> stock = new ObservableCollection<string>();
      List<WebData> history = new List<WebData>();
      //         stock = new List<string>();
      String filePath = "HistoryData.csv";

      StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
      try
      {

        StorageFile file = await roamingFolder.GetFileAsync(filePath);
        IList<String> strList = await FileIO.ReadLinesAsync(file);
        //        strList.Remove("aieu");
        foreach (String str in strList)
        {

          string[] msg1 = str.Split('\t');


          history.Add(new WebData(msg1[0], msg1[1], msg1[2]));


          string msg2 = string.Join("\n", msg1);

          stock.Add(msg2);
          //          stock.Remove(str); 
        }
        var p1 = new Logic.HistoryDataStore(stock);
        //        var p1 = new Logic.HistoryDataStore(history);

      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
        stock.Add("null");
        var p1 = new Logic.HistoryDataStore(stock);
      }
    }
  }


  class DeleteFavorites
  {
    //    static List<string> favorites;
    async public void populateFavorites()
    {
      List<string> favorites = new List<string>();
      String filePath = "FavoriteData.csv";
      // write file
      StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
      try
      {
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
        favorites.Add("null");
        var p1 = new Logic.FavoriteDataStore(favorites);
      }
    }
  }
}
