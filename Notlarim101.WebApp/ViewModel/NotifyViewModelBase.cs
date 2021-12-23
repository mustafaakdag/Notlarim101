using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim101.WebApp.ViewModel
{
    public class NotifyViewModelBase<T> //Notify=haber vermek.
    {
        public List<T> İtems { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeout { get; set; }

        public NotifyViewModelBase()
        {
            Header = "Yölendirliyorsunuz...";
            Title = "Geçersiz işlem";
            IsRedirecting = true;
            RedirectingUrl = "/Home/Index";
            RedirectingTimeout = 10000;
            İtems = new List<T>();
        }
    }
}