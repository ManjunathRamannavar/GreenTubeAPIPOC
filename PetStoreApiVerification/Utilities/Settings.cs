using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreApiVerification.Utiliti
{
    public class Settings
    {
        public Uri baseuri { get; set; }
        public RestClient client { get; set; }
        public  RestRequest request { get; set; }
        public IRestResponse response { get; set; }
    }
}
