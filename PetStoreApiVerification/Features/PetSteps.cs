using Nest;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PetStoreApiVerification.Models;
using PetStoreApiVerification.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PetStoreApiVerification.Features
{
    [Binding]
    public class PetSteps
    {
        private readonly Settings settings;
        private readonly Endpoints endpoints;

      
        public PetSteps(Settings settings, Endpoints endpoints)
        {
            this.settings = settings;
            this.endpoints = endpoints;
        }

        [When(@"I perform POST operation for PET")]
        public void WhenIPerformPOSTOperationForPET()
        {
            settings.request = new RestRequest(endpoints.POSTNewPet, Method.POST);
            settings.request.RequestFormat = DataFormat.Json;
            settings.request.AddJsonBody(new Pet() { id = 20, category = new Category() { id = 200, name = "Indian" }, name = "Doggie", photoUrls = new List<string>() { "D:\\GreenTube\\PetStoreApiVerification\\Image\\download.jfif" }, tags = new List<Tag>() { new Tag() { id = 300, name = "White" } }, status = "available" });

        }

      
        [When(@"I perform GET operation for PET by providing Pet ID (.*)")]
        public void WhenIPerformGETOperationForPETByProvidingPetID(string pId)
        {
            int petId = int.Parse(pId);

            settings.request = new RestRequest(endpoints.GETPetByID, Method.GET);
            settings.request.AddUrlSegment("petId", petId);
            settings.request.RequestFormat = DataFormat.Json;

        }

        [Then(@"I should see the response with different status code (.*)")]
        public void ThenIShouldSeeTheResponseWithDifferentStatusCode(int status)
        {
            settings.response = settings.client.Execute(settings.request);
            JObject objs = JObject.Parse(settings.response.Content);
            int StatusCode = (int)settings.response.StatusCode;
            //Assert that correct Status is returned
            Console.WriteLine(objs);
            Assert.AreEqual(status, StatusCode, "Status code is not " + status);
        }



        [Then(@"I should see the response as successful with status code")]
        public void ThenIShouldSeeTheResponseAsSuccessfulWithStatusCode()
        {
            settings.response = settings.client.Execute(settings.request);
            JObject objs = JObject.Parse(settings.response.Content);
            int StatusCode = (int)settings.response.StatusCode;
            //Assert that correct Status is returned
            Console.WriteLine(objs);
            Assert.AreEqual(200, StatusCode, "Status code is not 200 " );
        }
    }
}
