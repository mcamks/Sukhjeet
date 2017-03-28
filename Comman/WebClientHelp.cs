using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BluSignalHelpMethod
{
  public  class WebClientHelp
    {
        public string GetWebClientResponse(string webClientAddress)
        {
            string s = "";
            using (WebClient webClient = new WebClient())
            {
                 s = webClient.DownloadString(webClientAddress);
            }
            return s;
        }
    }
}
