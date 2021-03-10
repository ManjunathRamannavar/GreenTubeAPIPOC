using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PetStoreApiVerification.Models;
using PetStoreApiVerification.Utiliti;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PetStoreApiVerification.Features
{
    [Binding]
    public class UsersSteps
    {

        private readonly Settings settings;

        User userObj;

        public UsersSteps(Settings settings)
        {
            this.settings = settings;
        }

        [Given(@"I will call the base uri  ""(.*)""")]
        public void GivenIWillCallTheBaseUri(string baseuri)
        {
            settings.client = new RestClient(baseuri);
        }


        [When(@"I perform POST operation for ""(.*)""")]
        public void WhenIPerformPOSTOperationFor(string endpoint)
        {
            settings.request = new RestRequest(endpoint, Method.POST);
            settings.request.RequestFormat = DataFormat.Json;
            settings.request.AddJsonBody(new User() { id = 3, username = "Suresh Raj", firstName = "Suresh", lastName = "Raj", email = "sureshraj@gmail.com", password = "suresh123", phone = "9828383838", userStatus = 10 });

        }

        [When(@"I perform POST operation for ""(.*)"" with body")]
        public void WhenIPerformPOSTOperationForWithBody(string endpoint, Table table)
        {
            settings.request = new RestRequest(endpoint, Method.POST);
            settings.request.RequestFormat = DataFormat.Json; ;

            var userValue = table.CreateInstance<User>();
            settings.request.AddJsonBody(new User() { id = userValue.id, username = userValue.username.ToString(), firstName = userValue.firstName.ToString(), lastName = userValue.lastName.ToString(), email = userValue.email.ToString(), password = userValue.password.ToString(), phone = userValue.phone.ToString(), userStatus = userValue.userStatus });

        }

        [When(@"I perform PUT operation for ""(.*)"" to update details of username ""(.*)""")]
        public void WhenIPerformPUTOperationForToUpdateDetailsOfUsername(string endpoint, string uname, Table table)
        {
            settings.request = new RestRequest(endpoint, Method.PUT);
            settings.request.AddUrlSegment("username", uname);
            settings.request.RequestFormat = DataFormat.Json;

            var userValue = table.CreateInstance<User>();
            settings.request.AddJsonBody(new User() { id = userValue.id, username = userValue.username.ToString(), firstName = userValue.firstName.ToString(), lastName = userValue.lastName.ToString(), email = userValue.email.ToString(), password = userValue.password.ToString(), phone = userValue.phone.ToString(), userStatus = userValue.userStatus });

        }


        [When(@"I perform Delete operation for ""(.*)"" By providing username ""(.*)""")]
        public void WhenIPerformDeleteOperationForByProvidingUsername(string endpoint, string uname)
        {
            settings.request = new RestRequest(endpoint, Method.DELETE);
            settings.request.AddUrlSegment("username", uname);
            settings.request.RequestFormat = DataFormat.Json;

        }

        [When(@"I perform GET operation for  ""(.*)"" by providing username ""(.*)""")]
        public void WhenIPerformGETOperationForByProvidingUsername(string endpoint, string username)
        {
            settings.request = new RestRequest(endpoint, Method.GET);
            settings.request.AddUrlSegment("username", username);
            settings.request.RequestFormat = DataFormat.Json;
        }

        [When(@"I perform POST operation for ""(.*)"" with body (.*),(.*),(.*),(.*),(.*),(.*),(.*) and (.*)")]
        public void WhenIPerformPOSTOperationForWithBodyAnd(string endpoint, string uid, string userName, string firstName, string lastName, string email, string password, string phone, string userstatus)
        {
            int num = System.Convert.ToInt32(uid);
            int ustatus = System.Convert.ToInt32(userstatus);
            settings.request = new RestRequest(endpoint, Method.POST);
            settings.request.RequestFormat = DataFormat.Json; ;

            IRestRequest postReq = settings.request.AddJsonBody(new User() { id = num, username = userName, firstName = firstName, lastName = lastName, email = email, password = password, phone = phone, userStatus = ustatus });
            //Creating User object reference userObj for storing the POST request attributes
            userObj = new User() { id = num, username = userName, firstName = firstName, lastName = lastName, email = email, password = password, phone = phone, userStatus = ustatus };
            Console.WriteLine("==================");
            settings.response = settings.client.Execute(settings.request);
            JObject postResponse = JObject.Parse(settings.response.Content);
            Console.WriteLine(postResponse);

            Console.WriteLine("==================");


        }


        [Then(@"I should see that the record created with the POST matches with the response of the GET with status code (.*)")]
        public void ThenIShouldSeeThatTheRecordCreatedWithThePOSTMatchesWithTheResponseOfTheGETWithStatusCode(int status)
        {
            settings.response = settings.client.Execute(settings.request);
            JObject getObjs = JObject.Parse(settings.response.Content);
            int StatusCode = (int)settings.response.StatusCode;
            //Assert that correct Status is returned
            Console.WriteLine(getObjs);
            // Verifying that each attribute of POST request matches with the corresponding attribute of GET resposnse
            Assert.AreEqual(userObj.id, getObjs.GetValue("id"), "POST request record id does not match with GET response id");
            Assert.AreEqual(userObj.username, getObjs.GetValue("username"), "POST request record username does not match with GET response username");
            Assert.AreEqual(userObj.firstName, getObjs.GetValue("firstName"), "POST request record firstName does not match with GET response firstName");
            Assert.AreEqual(userObj.lastName, getObjs.GetValue("lastName"), "POST request record lastName does not match with GET response lastName");
            Assert.AreEqual(userObj.email, getObjs.GetValue("email"), "POST request record email does not match with GET response email");
            Assert.AreEqual(userObj.password, getObjs.GetValue("password"), "POST request record password does not match with GET response password");
            Assert.AreEqual(userObj.phone, getObjs.GetValue("phone"), "POST request record phone does not match with GET response phone");
            Assert.AreEqual(userObj.userStatus, getObjs.GetValue("userStatus"), "POST request record userStatus does not match with GET response userStatus");
            
            // Verifying the GET response status code
            Assert.AreEqual(status, StatusCode, "Status code is not " + status);


        }

        [Then(@"I should see the response with status code (.*)")]
        public void ThenIShouldSeeTheResponseWithStatusCode(int status)
        {
            settings.response = settings.client.Execute(settings.request);
            JObject objs = JObject.Parse(settings.response.Content);
            int StatusCode = (int)settings.response.StatusCode;
            //Assert that correct Status is returned
            Console.WriteLine(objs);
            Assert.AreEqual(status, StatusCode, "Status code is not " + status);
        }



        [Then(@"I should see the response as successfully deleted and status code (.*)")]
        public void ThenIShouldSeeTheResponseAsSuccessfullyDeletedAndStatusCode(int status)
        {
            settings.response = settings.client.Execute(settings.request);
            int StatusCode = (int)settings.response.StatusCode;
            Assert.AreEqual(status, StatusCode, "Status code is not " + status);
        }



        [Then(@"I should see the response as successful with status code as (.*) ok")]
        public void ThenIShouldSeeTheResponseAsSuccessfulWithStatusCodeAsOk(int status)
        {
            settings.response = settings.client.Execute(settings.request);
            JObject objs = JObject.Parse(settings.response.Content);
            int StatusCode = (int)settings.response.StatusCode;
            //Assert that correct Status is returned
            Console.WriteLine(objs);
            Assert.AreEqual(status, StatusCode, "Status code is not " + status);
        }


    }
}
