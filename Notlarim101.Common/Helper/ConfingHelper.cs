using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim101.Common.Helper
{
   public class ConfingHelper
    {
        //public static string Get(string key)
        //{
        //    //Configuration manager web.config dosyasi icinde appSetting icinde oldstuduğumuz mail dosyalarini keylerine ulasmak icin kullanacağız.
        //    return ConfigurationManager.AppSettings[key];
        //}

        public static T Get<T>(string key)
        {
            //Port numarası gibi integer bir geri dönüş istenirse bunun için metodu generic bir hale getirerek gelen tipi istenen tipe değiştirerek gondeririz.
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
        }
    }
}
