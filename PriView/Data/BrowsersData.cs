using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriView.Data
{
  [System.Runtime.Serialization.DataContract]
  public sealed class BrowsersData
  {
    [System.Runtime.Serialization.DataMember]
    public System.Collections.ObjectModel.ObservableCollection<Browser> Browsers { get; private set; }

    public BrowsersData()
    {
      this.Browsers = new System.Collections.ObjectModel.ObservableCollection<Browser>();

    }
  }

  [System.Runtime.Serialization.DataContract]
  public class Browser
  {
    [System.Runtime.Serialization.DataMember]
    public string Title {get;set;}


    [System.Runtime.Serialization.DataMember]
    public System.Collections.ObjectModel.ObservableCollection<BrowserItem> Items { get; private set; }

    public Browser()
    {
      this.Items = new System.Collections.ObjectModel.ObservableCollection<BrowserItem>();

    }

    public Browser(string title)
    {
      this.Items = new System.Collections.ObjectModel.ObservableCollection<BrowserItem>();
      this.Title = title;

    }
  }

  [System.Runtime.Serialization.DataContract]
public class BrowserItem
{
  [System.Runtime.Serialization.DataMember]
  public string Title { get; set; }

  [System.Runtime.Serialization.DataMember]
  public string Link { get; set; }

  [System.Runtime.Serialization.DataMember]
  public System.DateTime PubDate { get; set; }

  public BrowserItem() { }

  // newするときに記事のタイトル／リンク先URL／発行日時を与えることも可能
  public BrowserItem(string title, string link, System.DateTime pubDate)
  {
    this.Title = title;
    this.Link = link;
    this.PubDate = pubDate;
  }
}



  

}
