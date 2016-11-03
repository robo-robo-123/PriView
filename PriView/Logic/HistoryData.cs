using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriView.Data
{
  [System.Runtime.Serialization.DataContract]
  public sealed class HistoryItem
  {

    [System.Runtime.Serialization.DataMember]
    public string Title { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string Link { get; set; }

    [System.Runtime.Serialization.DataMember]
    public string Date { get; set; }

    public HistoryItem() { }

    // newするときに記事のタイトル／リンク先URL／発行日時を与えることも可能
    public HistoryItem(string title, string link, string date)
    {
      this.Title = title;
      this.Link = link;
      this.Date = date;

    }
  }
/*        [System.Runtime.Serialization.DataMember]
    public System.Collections.ObjectModel.ObservableCollection<History> Historys { get; private set; }

        public HistoryData()
    {
      this.Historys = new System.Collections.ObjectModel.ObservableCollection<History>();

    }*/


/*
  }

  [System.Runtime.Serialization.DataContract]
  public class History
  {
    [System.Runtime.Serialization.DataMember]
    public string Title { get; set; }


    [System.Runtime.Serialization.DataMember]
    public System.Collections.ObjectModel.ObservableCollection<HistoryItem> Items { get; private set; }

    public History()
    {
      this.Items = new System.Collections.ObjectModel.ObservableCollection<HistoryItem>();
    }

    public History(string msg)
    {
      this.Items = new System.Collections.ObjectModel.ObservableCollection<HistoryItem>();
      this.Title = msg;

    }


  }

  [System.Runtime.Serialization.DataContract]
  public class HistoryItem
  {

  }*/

}
