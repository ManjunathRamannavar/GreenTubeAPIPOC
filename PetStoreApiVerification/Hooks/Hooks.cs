using AventStack.ExtentReports;
using AventStack.ExtentReports.Configuration;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using PetStoreApiVerification.Utiliti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PetStoreApiVerification.Hooks
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private readonly IObjectContainer _objectContainer;


        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;

        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            var htmlReporter = new ExtentHtmlReporter("C:\\Users\\TYSS\\source\\repos\\PetStoreApiVerification\\PetStoreApiVerification\\Reports\\ExtentReport.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }


        [BeforeScenario]
        public void Initialize()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);

        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
        }

    }
}
