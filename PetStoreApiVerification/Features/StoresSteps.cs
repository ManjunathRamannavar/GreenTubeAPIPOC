using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetStoreApiVerification.Models;
using PetStoreApiVerification.Utilities;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PetStoreApiVerification.Features
{
    [Binding]
    public class StoresSteps
    {
        private readonly Settings settings;
        private readonly Endpoints endpoints;


        string postResponse;

        public StoresSteps(Settings settings, Endpoints endpoints)
        {
            this.settings = settings;
            this.endpoints = endpoints;
        }

        [When(@"I perform POST operation to place the order for pet")]
        public void WhenIPerformPOSTOperationToPlaceTheOrderForPet(Table table)
        {
            settings.request = new RestRequest(endpoints.POSTOrder, Method.POST);
            settings.request.RequestFormat = DataFormat.Json;
           
            Store value = table.CreateInstance<Store>();
            bool comp = value.complete;
            var dt = DateTime.Now;
            settings.request.AddJsonBody(new Store() { id = value.id, petId = value.petId, quantity = value.quantity, shipDate = dt, status = value.status, complete = comp });

        }

        [When(@"I perform GET operation to find purchase order by ID as (.*)")]
        public void WhenIPerformGETOperationToFindPurchaseOrderByIDAs(int orderId)
        {
            settings.request = new RestRequest(endpoints.GETOrderByID, Method.GET);
            settings.request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
            settings.request.RequestFormat = DataFormat.Json;
          
        }

        [When(@"I perform POST operation for Order with examples as ""(.*)"",""(.*)"",""(.*)"",""(.*)"",""(.*)""")]
        public void WhenIPerformPOSTOperationForOrderWithExamplesAs(int id, int petId, int quantity, string status, string complete)
        {
            settings.request = new RestRequest(endpoints.POSTOrder, Method.POST);
            
            var dt = DateTime.Now;
            bool comp = bool.Parse(complete);
            Store order = new Store() { id = id, petId = petId, quantity = quantity, shipDate = dt, status = status, complete = comp };
            string postRequest = JsonConvert.SerializeObject(order);
            settings.request.AddParameter("application/json", postRequest, ParameterType.RequestBody);

            settings.response=settings.client.Execute(settings.request);
            postResponse = settings.response.Content;
        }

        [When(@"I perform GET operation for  order by providing orderId (.*) to check the PoST request succesfully created order")]
        public void WhenIPerformGETOperationForOrderByProvidingOrderIdToCheckThePoSTRequestSuccesfullyCreatedOrder(int orderId)
        {
            settings.request = new RestRequest(endpoints.GETOrderByID, Method.GET);
            settings.request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
            settings.request.RequestFormat = DataFormat.Json;
            settings.response = settings.client.Execute(settings.request);
        }

        [Then(@"I should see that the record created with the Store POST matches with the response of the store GET with status code (.*)")]
        public void ThenIShouldSeeThatTheRecordCreatedWithTheStorePOSTMatchesWithTheResponseOfTheStoreGETWithStatusCode(int status)
        {
           
            int StatusCode = (int)settings.response.StatusCode;
          
            Assert.AreEqual(postResponse, settings.response.Content, "POST request not match with Get Response");
            Assert.AreEqual(status, StatusCode, "Statuscode is not a " + status);

        }



    }
}
