using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreApiVerification.Utilities
{
    public class Endpoints
    {
        public string BaseURI = "https://petstore.swagger.io/v2/";
        public string POSTUploadImage = "/pet/{petId}/uploadImage";
        public string POSTNewPet = "/pet";
        public string POSTUpdatePet = "/pet/{petId}";
        public string PUTPet = "/pet";
        public string DELETEPet = "/pet/{petId}";
        public string GETPetByStatus = "/pet/findByStatus";
        public string GETPetByID = "/pet/{petId}";
        public string POSTOrder = "/store/order";
        public string GETOrderByID = "/store/order/{orderId}";
        public string GETOrderStatus = "/store/inventory";
        public string POSTUserWithArray = "/user/createWithArray";
        public string POSTUserWithList = "/user/createWithList";
        public string POSTUser = "/user";
        public string PUTUser = "/user/{username}";
        public string DELETEUser = "/user/{username}";
        public string GETUser = "/user/{username}";
        public string GETLogsLoginUser = "/user/login";
        public string GETLOgsOutUser = "/user/logout";

    }
}
