using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriView.Model;

namespace PriView.ViewModel
{
  public class GeneralViewModel : Common.BindableBase//, IDisposable
  {
    private Model.GeneralModel Model { get; } = GeneralModel.Instance;

    //first_time
    public string Last_url
    {
      get { return this.Model.Last_url; }
      set { this.Model.Last_url = value; }
    }

    public string Default_url
    {
      get { return this.Model.Default_url; }
      set { this.Model.Default_url = value; }
    }
  }
}
