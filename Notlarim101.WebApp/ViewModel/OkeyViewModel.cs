using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim101.WebApp.ViewModel
{
    public class OkeyViewModel: NotifyViewModelBase<string>
    {
        public OkeyViewModel()
        {
            Title = "İşlem başarılı";
        }
    }
}