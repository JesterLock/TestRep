//using AutotestSupport;
using RandoDotNet;
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Core
{
    [TestClass]
    public class UnitTest1
    {

        private RemoteWebDriver driver;
        //static public String testName = String.Empty;



        [TestInitialize]
        public void TestInitialize()
        {
            driver = new FirefoxDriver();
            //driver = new InternetExplorerDriver();
            //ScenarioContext.Current["driver"] = driver;   //для SpecFlow
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Navigate().GoToUrl(Steps.baseUrl);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void Authorization()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            //Assert.IsTrue(AutotestSupport.AutotestSupport.IsElementPresentByCssSelector(driver, Steps.accountMenuSelector) || AutotestSupport.AutotestSupport.IsElementPresentByCssSelector(driver, Steps.exitButtonSelector));
        }

        [TestMethod]
        public void CreateNewsWithAttachment()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateNewsWithAttachment(driver);
        }

        /*[TestMethod]
        public void CreateNews()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateNews(driver);
            Thread.Sleep(Steps.shortRest);
            Assert.AreEqual(Steps.newsText, driver.FindElementByCssSelector(Steps.newsItemTextSelector).Text);
        }

        [TestMethod]
        public void CreatePoll()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreatePoll(driver);
            Assert.IsTrue(Steps.pollText == driver.FindElementByCssSelector(Steps.pollItemHeaderSelector).Text && Steps.pollFirstOption == driver.FindElementByCssSelector(Steps.pollItemFirstOptionSelector).Text && Steps.pollSecondOption == driver.FindElementByCssSelector(Steps.pollItemSecondOptionSelector).Text);
        }

        [TestMethod]
        public void CreateVoting()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateVoting(driver);
            Steps.AcceptVoting(driver);
            driver.FindElementByCssSelector(Steps.votingSideMenuItemSelector).Click();
            Assert.IsTrue(driver.FindElementByCssSelector(Steps.votingItemHeaderSelector).Text == Steps.votingText && driver.FindElementByCssSelector(Steps.votingVoteItemSelector).Displayed && driver.FindElementByCssSelector(Steps.votingNoVoteItemSelector).Displayed);
        }

        [TestMethod]
        public void ViewNewsDetails()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateNews(driver);
            Thread.Sleep(Steps.extendedRest);
            Steps.ViewNewsDetails(driver);
            Assert.AreEqual(Steps.newsText, driver.FindElementByCssSelector(Steps.newsDetailsCardTextSelector).Text);
        }

        [TestMethod]
        public void ViewPollDetails()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreatePoll(driver);
            Steps.ViewPollDetails(driver);
            Assert.IsTrue(driver.FindElementByCssSelector(Steps.pollDetailsCardHeaderSelector).Text == Steps.pollText && driver.FindElementByCssSelector(Steps.pollDetailsCardFirstOptionSelector).Text == Steps.pollFirstOption && driver.FindElementByCssSelector(Steps.pollDetailsCardSecondOptionSelector).Text == Steps.pollSecondOption);
        }

        [TestMethod]
        public void ViewVotingDetails()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateVoting(driver);
            Thread.Sleep(Steps.endTurn);
            Steps.AcceptVoting(driver);
            Steps.ViewVotingDetails(driver);
            Assert.IsTrue(driver.FindElementByCssSelector(Steps.votingDetailsCardHeaderSelector).Text == Steps.votingText && driver.FindElementByCssSelector(Steps.votingDetailsCardFirstQuestionHeader).Text == "1. " + Steps.votingFirstHeader && driver.FindElementByCssSelector(Steps.votingDetailsCardFirstQuestionText).Text == Steps.votingFirstText && driver.FindElementByCssSelector(Steps.votingDetailsCardSecondQuestionHeader).Text == "2. " + Steps.votingSecondHeader && driver.FindElementByCssSelector(Steps.votingDetailsCardSecondQuestionText).Text == Steps.votingSecondText && driver.FindElementByCssSelector(Steps.votingDetailsCardVoteSelector).Displayed && driver.FindElementByCssSelector(Steps.votingDetailsCardNoVoteSelector).Displayed);
        }

        [TestMethod]
        public void AddOwnedProperty()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.AddOwnedProperty(driver);
            Assert.IsTrue(Array.TrueForAll<string>(Steps.randomAddressArray, item => Steps.propertySelectorAddress.ToUpper().Contains(item.ToUpper())));
        }

        [TestMethod]
        public void CreateChat()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateChat(driver);
            Thread.Sleep(Steps.extendedRest);
            Assert.AreEqual("Тема: " + Steps.chatName, driver.FindElementByCssSelector(Steps.chatNameSelector).Text);
        }

        [TestMethod]
        public void AddChatMessage()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateChat(driver);
            Thread.Sleep(Steps.extendedRest);
            Steps.AddMessageToChat(driver);
            Steps.Deauthorization(driver);
            Steps.Authorization(driver, Steps.ownerLogin);
            driver.FindElementByCssSelector(Steps.chatSideMenuItemSelector).Click();
            Thread.Sleep(Steps.extendedRest);
            Assert.AreEqual(Steps.newChatMessage, driver.FindElementByCssSelector(Steps.lastChatMessageSelector).Text);
        }

        [TestMethod]
        public void CheckSelfActivity()     //чот фигня какая-то - в активности сразу после опубликованного голосования вываливается дополнительный опрос по голосованию
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.CreateNews(driver);
            Steps.CreatePoll(driver);
            Steps.CreateVoting(driver);
            Steps.AcceptVoting(driver);
            driver.FindElementByCssSelector(Steps.activitySideMenuItemSelector).Click();
            Thread.Sleep(Steps.shortRest);
            //вместе с голосованием создаётся опрос, соответствующий первому этапу голосования; пока непонятно, должно так быть или нет; если уберут - поправить селекторы проверяемых элементов; если он должен отображаться - добавить проверку на него
            Assert.IsTrue(driver.FindElementByCssSelector(Steps.activityFirstItemTypeSelector).Text == "Новость" && driver.FindElementByCssSelector(Steps.activityFirstItemCaptionSelector).Text == Steps.newsText && driver.FindElementByCssSelector(Steps.activitySecondItemTypeSelector).Text == "Опрос" && driver.FindElementByCssSelector(Steps.activitySecondItemCaptionSelector).Text == Steps.pollText && driver.FindElementByCssSelector(Steps.activityThirdItemTypeSelector).Text == "Голосование" && driver.FindElementByCssSelector(Steps.activityThirdItemCaptionSelector).Text == Steps.votingText);
        }

        [TestMethod]
        public void ChangeSelectedProperty()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.ChangeSelectedProperty(driver);
            Assert.IsTrue(Array.TrueForAll<string>(Steps.selectedPropertyAddressesArray, item => Steps.propertyToSwitchTo.ToUpper().Contains(item.ToUpper())));
        }

       [TestMethod]
       public void AddLease()
        {
            Steps.Authorization(driver, Steps.ownerLogin);
            Steps.SwitchToRandomOwnedProperty(driver);
            Thread.Sleep(Steps.shortRest);
            string[] ownerName = Steps.ReadUserName(driver);
            Steps.Deauthorization(driver);
            Steps.Authorization(driver, Steps.secondlogin);
            Steps.AddLeasedProperty(driver, ownerName[0]);
            string[] leaserName = Steps.ReadUserName(driver);
            Steps.Deauthorization(driver);
            Steps.Authorization(driver, Steps.ownerLogin);
            string[] rawNeighbourName = Steps.ReadNeighbourName(driver);
            string[] neighbourName = new string[3];
            for (int i = 0; i < neighbourName.Length - 2; i++)
            {
                leaserName[i] = neighbourName[i];
            }
            foreach (string element in leaserName)
            {
                AutotestSupport.AutotestSupport.CustomLog(element + ";");
            }
            AutotestSupport.AutotestSupport.CustomLog("----------");
            foreach (string element in neighbourName)
            {
                AutotestSupport.AutotestSupport.CustomLog(element + ";");
            }
            Assert.AreEqual(leaserName, neighbourName);
        }*/
    }
}
