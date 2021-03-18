using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            DateTime dt = DateTime.Now;
            Store value = table.CreateInstance<Store>();
            bool comp = value.complete;

            settings.request.AddJsonBody(new Store() { id = value.id, petId = value.petId, quantity = value.quantity, shipDate = dt, status = value.status, complete = comp });

        }

        [When(@"I perform GET operation to find purchase order by ID as (.*)")]
        public void WhenIPerformGETOperationToFindPurchaseOrderByIDAs(int orderId)
        {
            settings.request = new RestRequest(endpoints.GETOrderByID, Method.GET);
            settings.request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
            settings.request.RequestFormat = DataFormat.Json;
        }

        

    }
}
