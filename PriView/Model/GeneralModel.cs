using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PriView.Model
{
  public class GeneralModel : Common.BindableBase
  {
    public static GeneralModel Instance { get; } = new GeneralModel();


    private string last_url;
    public string Last_url
    {
      get { return this.last_url; }
      set { this.SetProperty(ref this.last_url, value); }
    }

    private bool first_page;
    public bool First_page
    {
      get { return this.first_page; }
      set { this.SetProperty(ref this.first_page, value); }
    }

    private string default_url;
    public string Default_url
    {
      get { return this.default_url; }
      set { this.SetProperty(ref this.default_url, value); }
    }


    public void SaveCount()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      settings.Values["last_url"] = this.Last_url;
      settings.Values["default_url"] = this.Default_url;

    }

    public void LoadCount()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      var temp = default(object);
      //first
      if (settings.Values.TryGetValue("last_url", out temp))
      {
        this.Last_url = (string)temp;
      }
      if (settings.Values.TryGetValue("default_url", out temp))
      {
        this.Default_url = (string)temp;
      }

    }
  }
}
