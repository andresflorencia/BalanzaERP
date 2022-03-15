using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;

namespace BalanzaERP.Utils
{
    public class WebService
    {
        //const string URL = "https://online.alimentosfrescos.info/wsmovil/";
        
        //const string URL = "http://127.0.0.1/wsmovil/";
        //const string URL = "https://testonline.alimentosfrescos.info/wsmovil/";

        public string sendURL(string strServicio, NameValueCollection pDatos)
        {
            string strRet = "";
            try
            {
                using (WebClient cliente = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12;
                    strRet = Encoding.Default.GetString(cliente.UploadValues(Global.URL + strServicio, pDatos));
                }
            }
            catch(Exception e)
            {
                strRet = e.Message;
            }
            return strRet;
        }

        public string getURL(string strServicio)
        {
            string strRet = "";
            try
            {
                using (WebClient cliente = new WebClient())
                {
                    strRet = cliente.DownloadString(Global.URL + strServicio);
                }
            }
            catch { strRet = ""; }
            return strRet;
        }
    }
}
