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

  class HistoryDataStore
  {
    static ObservableCollection<string> stock = new ObservableCollection<string>();
    public HistoryDataStore(ObservableCollection<string> history)
    {
      stock = history;
    }
    public HistoryDataStore(ref ObservableCollection<string> history)
    {
        history = stock;
    }
  }

  class FavoriteDataStore
  {
    static List<string> stock2 = new List<string>();
    public FavoriteDataStore(List<string> favorite)
    {
      stock2 = favorite;
    }
    public FavoriteDataStore(ref List<string> favorite)
    {
      favorite = stock2;
    }
  }

  class LastviewDataStore
  {
    static List<string> stock;
    public LastviewDataStore(List<string> LastView)
    {
      stock = LastView;
    }
    public LastviewDataStore(ref List<string> LastView)
    {
      LastView = stock;
    }
  }

  public class WebData
  {

//    public int ID { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Date { get; set; }
    public WebData(string title, string url, string date)
    {
      Title = title;
      Url = url;
      Date = date;
      
    }
  }

  class PopulateHistories
  {
    String filePath = "HistoryData.csv";
    StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;

    async public void populateHistories()
    {
      ObservableCollection<string> stock = new ObservableCollection<string>();
      List<WebData> history = new List<WebData>();

      try
      {
        StorageFile file = await roamingFolder.GetFileAsync(filePath);
        IList<String> strList = await FileIO.ReadLinesAsync(file);
        foreach (String str in strList)
        {
          string[] msg1 = str.Split('\t');
          //history.Add(new WebData(msg1[0], msg1[1], msg1[2]));
          string msg2 = string.Join("\n", msg1);
          stock.Add(msg2);
        }
        var p1 = new Logic.HistoryDataStore(stock);
      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
//        stock.Add("null");
//        var p1 = new Logic.HistoryDataStore(stock);
      }
    }

    public void populateHistories2(string url_info)
    {
      ObservableCollection<string> stock = new ObservableCollection<string>();
      var p1 = new Logic.HistoryDataStore(ref stock);
      string[] msg1 = url_info.Split('\t');
      string msg2 = string.Join("\n", msg1);
      stock.Add(msg2);
      var p2 = new Logic.HistoryDataStore(stock);
    }

  }


  class PopulateFavorites
  {
//    static List<string> favorites;
    async public void populateFavorites()
    {
      List<string> favorites = new List<string>();
      List<WebData> favorite = new List<WebData>();
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
          //favorite.Add(new WebData(msg1[0], msg1[1], msg1[2]));
          string msg2 = string.Join("\n", msg1);
          favorites.Add(msg2);
        }
        var p1 = new Logic.FavoriteDataStore(favorites);
      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
//        favorites.Add("null");
//        var p1 = new Logic.FavoriteDataStore(favorites);
      }
    }

    public void populateFavorites2(string url_info)
    {
      List<string> stock = new List<string>();
      var p1 = new Logic.FavoriteDataStore(ref stock);
      string[] msg1 = url_info.Split('\t');
      string msg2 = string.Join("\n", msg1);
      stock.Add(msg2);//初回時にファイルが作られてない場合があるよ
      var p2 = new Logic.FavoriteDataStore(stock);
    }

  }


  class PopulateLastview
  {

    async public void populateLastview()
    {
      List<string> LastView = new List<string>();
      //         stock = new List<string>();
      String filePath = "LastViewData.csv";

      StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
      try
      {

        StorageFile file = await  roamingFolder.GetFileAsync(filePath);

        IList<String> strList = await FileIO.ReadLinesAsync(file);

        LastView.Clear();

        foreach (String str in strList)
        {
          string[] msg1 = str.Split('\t');
          string msg2 = string.Join("\n", msg1);
          LastView.Add(msg2);
          
        }
        var p1 = new Logic.LastviewDataStore(LastView);
//        await file.DeleteAsync();
      }
      catch (FileNotFoundException ex)
      {
        // ファイル無し
//        LastView.Add(null);
        var p1 = new Logic.LastviewDataStore(LastView);
      }
    }
  }

}

