using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Core
{
    class Steps
    {
        public const string baseUrl = "http://ilive-web.cmsplanet.ru/";
        public const string cmsUrl = "http://ilive.cmsplanet.ru:5005/";
        public const string base1Url = "https://xn--b1ahg9b.xn--p1ai/";
        public const string cms1Url = "https://admin.live-in.ru/";

        const string devLogin = "Developer";
        const string devPassword = "TrEdramEFegaSWEw4MuC";
        public const string ownerLogin = "9632289456";
        public const string secondlogin = "9612642122";
        const string password = "!nW0rkPassw0rd";

        public static string newsText = string.Empty;
        public static string pollText = string.Empty;
        public static string pollFirstOption = string.Empty;
        public static string pollSecondOption = string.Empty;
        public static string votingText = string.Empty;
        public static string votingFirstHeader = string.Empty;
        public static string votingFirstText = string.Empty;
        public static string votingSecondHeader = string.Empty;
        public static string votingSecondText = string.Empty;
        public static string randomFlatNumber = string.Empty;
        public static string randomAddress = string.Empty;
        public static string chatName = string.Empty;
        public static string newChatMessage = string.Empty;
        public static string secondAddressRowCut = string.Empty;
        public static string propertySelectorAddress = string.Empty;
        public static string[] randomAddressArray = new string[20];
        public static string propertyToSwitchTo = string.Empty;
        static string selectedPropertyAddress = string.Empty;
        public static string[] selectedPropertyAddressesArray = new string[20];
        static string flatString = string.Empty;
        static string flatNumber = string.Empty;
        static string[] userName = new string[3];

        static readonly Random r = new Random();

        public const int endTurn = 1000;
        public const int shortRest = 5000;
        public const int extendedRest = 8000;

        const string logoSelector = "#react-root > div > div.container__auth > div > header > ul > li:nth-child(1) > a > img";
        const string loginPhoneNumberSelector = "#login";
        const string loginPasswordSelector = "#react-root > div > div.container__auth > div > div > section:nth-child(1) > div.auth-signup > div > div.input-container > input[type=\"password\"]";
        const string loginButtonSelector = "#react-root > div > div.container__auth > div > div > section:nth-child(1) > div.auth-signup > div > button";
        const string authorizationButtonSelector = "#react-root > header > div > nav > a.button-outline";

        const string globalSelector = "#react-root > div > header > div.header__container > ul.header__menu > li:nth-child(1)";
        const string homeSelector = "#react-root > div > header > div.header__container > ul.header__menu > li:nth-child(2)";
        const string municipalitySelector = "#react-root > div > header > div.header__container > ul.header__menu > li.my-region";
        const string regionSelector = "#react-root > div > header > div.header__container > ul.header__menu > li.my-city";

        public const string accountMenuSelector = "#react-root > div > header > div.header__container > ul.header__items > li:nth-child(4) > a";
        public const string exitButtonSelector = "#react-root > div > div.container__auth.container__auth--extended > header > ul > li.pull-right > div > button";

        const string newsSideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(1)";
        const string pollSideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(2)";
        public const string votingSideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(3)";
        const string propertySideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(4)";
        const string neighboursSideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(5)";
        public const string activitySideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(6)";
        public const string chatSideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(7)";
        const string notificationsSideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(8)";
        const string settingsSideMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(9)";

        const string profileMenuButtonSelector = "#react-root > div > header > div.header__container > ul.header__items > li:nth-child(4)";
        const string profileLogoutButtonSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.header__profile-menu > ul > a:nth-child(4) > li";

        const string createContentButtonSelector = ".button-publish";
        const string addAttachmentButtonSelector = "#modal-news-b01 > div > div.modal__content.modal__content--news-b01 > div.form-group > div > label";

        const string createNewsTextareaSelector = ".custom-textarea";
        const string createNewsAddFileSelector = "#main-content > div:nth-child(2) > div:nth-child(1) > div > div.post-create > div > footer > label > span";
        const string createNewsYKVisibleSelector = "#main-content > div:nth-child(2) > div:nth-child(1) > div > div.post-create > div > footer > div > div > label";
        const string createNewsCancelButtonSelector = "#main-content > div:nth-child(2) > div:nth-child(1) > div > div.post-create > div > footer > button.button-transparent";
        const string createNewsOKButtonSelector = "#main-content > div:nth-child(2) > div:nth-child(1) > div > div.post-create > div > footer > button.button-red";

        const string createPollTextareaSelector = "#main-content > div:nth-child(2) > div.content > div.post-create > div.post-create__input > textarea";
        const string firstPollOptionTextareaSelector = "#main-content > div:nth-child(2) > div.content > div.post-create > div.post-create__poll > ul > li:nth-child(1) > input";
        const string secondPollOptionTextareaSelector = "#main-content > div:nth-child(2) > div.content > div.post-create > div.post-create__poll > ul > li:nth-child(2) > input";
        const string savePollAsDraftButtonSelector = "#main-content > div:nth-child(2) > div.content > div.post-create > footer > button.button-transparent.button-left";
        const string createPollYKVisibleSelector = "#main-content > div:nth-child(2) > div.content > div.post-create > footer > div > div > label";
        const string createPollCancelButtonSelector = "#main-content > div:nth-child(2) > div.content > div.post-create > footer > button:nth-child(3)";
        const string createPollOKButtonSelector = "#main-content > div:nth-child(2) > div.content > div.post-create > footer > button.button-red";

        const string createVotingTextareaSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(1) > div > div.post-create__input > textarea";
        const string addVotingItemButtonSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(1) > div > div.post-create__vote > button";
        const string addVotingSaveAsDraftButtonSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(1) > div > footer > button.button-transparent.button-left";
        const string addVotingCancelButtonSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(1) > div > footer > button:nth-child(2)";
        const string addVotingOKButtonSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(1) > div > footer > button.button-red";

        const string addVotingItemPopupHeaderSelector = "#qName";
        const string addVotingItemPopupTextSelector = "#qDescription";
        const string addVotingItemPopupAddFileSelector = "#modal-add_question > div > div.modal__content > label";
        const string addVotingItemPopupCancelButtonSelector = "#modal-add_question > div > div.modal__footer > button.button-transparent";
        const string addVotingItemPopupOKButtonSelector = "#modal-add_question > div > div.modal__footer > button.button-red";

        public const string newsItemTextSelector = "#main-content > div:nth-child(2) > div:nth-child(1) > div > div:nth-child(2) > div > div.post-main > article";
        const string newsDetailsButtonSelector = "#main-content > div:nth-child(2) > div:nth-child(1) > div > div:nth-child(2) > div > div.post-main > footer > a";
        public const string newsDetailsCardTextSelector = "#main-content > div:nth-child(2) > div.content > div > div > div.post-main > article";

        public const string pollItemHeaderSelector = "#main-content > div:nth-child(2) > div.content > div.post-view > div > div.post > div.post-main > div > div.poll__header > h3";
        public const string pollItemFirstOptionSelector = "#main-content > div:nth-child(2) > div.content > div.post-view > div > div.post > div.post-main > div > form > div:nth-child(1) > label > button";
        public const string pollItemSecondOptionSelector = "#main-content > div:nth-child(2) > div.content > div.post-view > div > div.post > div.post-main > div > form > div:nth-child(2) > label > button";
        const string pollDetailsButtonSelector = "#main-content > div:nth-child(2) > div.content > div.post-view > div > div.post > div.post-main > footer > a > button";
        public const string pollDetailsCardHeaderSelector = "#main-content > div.content > div.post > div.post-main > div > div.poll__header > h3";
        public const string pollDetailsCardFirstOptionSelector = "#main-content > div.content > div.post > div.post-main > div > form > div:nth-child(1)";
        public const string pollDetailsCardSecondOptionSelector = "#main-content > div.content > div.post > div.post-main > div > form > div:nth-child(2)";

        public const string votingItemHeaderSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(2) > div > div.post-main > div > div.vote__header > h3";
        public const string votingVoteItemSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(2) > div > div.post-main > div > div.vote__status.wait > div > div:nth-child(1) > label";
        public const string votingNoVoteItemSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(2) > div > div.post-main > div > div.vote__status.wait > div > div:nth-child(3) > label";
        const string votingDetailsButtonSelector = "#main-content > div:nth-child(2) > div.content > div:nth-child(2) > div > div.post-main > footer > a > button";
        public const string votingDetailsCardHeaderSelector = "#main-content > div.content > div:nth-child(2) > div > div > h2";
        public const string votingDetailsCardFirstQuestionHeader = "#main-content > div.content > div:nth-child(3) > div.basic-block__content > div > div:nth-child(1) > h3";
        public const string votingDetailsCardFirstQuestionText = "#main-content > div.content > div:nth-child(3) > div.basic-block__content > div > div:nth-child(1) > p";
        public const string votingDetailsCardSecondQuestionHeader = "#main-content > div.content > div:nth-child(3) > div.basic-block__content > div > div:nth-child(2) > h3";
        public const string votingDetailsCardSecondQuestionText = "#main-content > div.content > div:nth-child(3) > div.basic-block__content > div > div:nth-child(2) > p";
        public const string votingDetailsCardVoteSelector = "#main-content > div.content > div:nth-child(2) > div > div > form > div:nth-child(1)";
        public const string votingDetailsCardNoVoteSelector = "#main-content > div.content > div:nth-child(2) > div > div > form > div:nth-child(2)";

        const string addPropertyButtonSelector = "#main-content > div.content > div:nth-child(1) > header > a";
        const string propertyAddressInputSelector = "#modal-add_house > div > div > div.modal__select-container > div > div > input[type=\"text\"]";
        const string propertyAddressFirstOptionSelector = "#modal-add_house > div > div > div.modal__select-container > div > div.select__list > li:nth-child(1)";
        const string propertyFlatNumberSelector = "#modal-add_house > div > div > div > div > div > div.input-container.flat-input > div.input-container.flat-input > input[type=\"text\"]";
        const string propertyAddingNextButtonSelector = "#modal-add_house > div > div > div > button";
        const string ownershipTypeChooseSelector = "#modal-add_user > div > div.modal__content > div:nth-child(3) > div > div.select__button";
        const string ownershipTypeOwnerSelector = "#modal-add_user > div > div.modal__content > div:nth-child(3) > div > div.select__list > div > li:nth-child(3)";
        const string propertyAddingAddButtonSelector = "#modal-add_user > div > div.modal__footer > button.button-red.button-small";
        const string goToNewAddressButtonSelector = "button.button-red:nth-child(3)";
        const string leaseDatePickerInputSelector = "#date-input";
        const string leaseDatePickerNextMonthSelector = "#datepicker-container > div > div > div.DayPicker-NavBar > span.DayPicker-NavButton.DayPicker-NavButton--next";
        const string leaseDatePickerFirstMondaySelector = "#datepicker-container > div > div > div.DayPicker-Months > div > div.DayPicker-Body > div:nth-child(2) > div:nth-child(1)";
        const string ownerNameFieldSelector = "#modal-add_user > div > div.modal__content > div:nth-child(5) > input[type=\"text\"]:nth-child(2)";
        const string ownerPhoneFieldSelector = "#phone";

        const string approveWaitPropertyTabSelector = "#main-content > div.right-menu-container > div > div > ul > li:nth-child(1) > a";
        const string allPropertyTabSelector = "#main-content > div.right-menu-container > div > div > ul > li:nth-child(2) > a";
        const string ownedPropertyTabSelector = "#main-content > div.right-menu-container > div > div > ul > li:nth-child(3) > a";
        const string leasedPropertyTabSelector = "#main-content > div.right-menu-container > div > div > ul > li:nth-child(4) > a";
        const string cohabitatedPropertyTabSelector = "#main-content > div.right-menu-container > div > div > ul > li:nth-child(5) > a";

        const string firstPropertyAddressFirstRow = "#main-content > div.content > div:nth-child(2) > div.basic-block__content > div.settings-item.settings-item--reverse.settings-item--address > div > b";
        const string firstPropertyAddressSecondRow = "#main-content > div.content > div:nth-child(2) > div.basic-block__content > div.settings-item.settings-item--reverse.settings-item--address > div > span";

        public const string propertySelectorSelector = "#react-root > div > header > div.header__container > ul.header__items > li:nth-child(1) > button";
        public const string rightMenuAddressSelector = "#main-content > div:nth-child(2) > div.right-menu-container > div > div:nth-child(1) > div.right-menu__text";
        public const string rightMenuAddressSecondRowSelector = "#main-content > div:nth-child(2) > div.right-menu-container > div > div:nth-child(1) > div.right-menu__text > span";

        const string createChatSelector = "#main-content > div.chat > div:nth-child(1) > div > div.chat__search > button";
        const string createFirstChatSelector = "#main-content > div.chat > div:nth-child(1) > div > button";
        const string chatNameInputSelector = "#main-content > div.chat > div:nth-child(1) > div.modal > div > div.modal__content > div.chat__list-add-user > div > div:nth-child(2) > input";
        const string chatSelectAllSelector = "#main-content > div.chat > div:nth-child(1) > div.modal > div > div.modal__content > div.chat__list-add-user > div > div.button-group > button:nth-child(1)";
        const string chatOKButtonSelector = "#main-content > div.chat > div:nth-child(1) > div.modal > div > div.modal__footer > button.button-red.js-hide-modal";
        public const string chatNameSelector = "#scrollItemId > div:nth-child(1) > div.chat-dialog-description > div:nth-child(2)";
        const string chatNewMessageInputSelector = "#main-content > div.chat > div.right-column > div.chat-footer > div:nth-child(2) > div.chat-footer-center > textarea";
        const string chatNewMessageButtonSelector = "#main-content > div.chat > div.right-column > div.chat-footer > div:nth-child(2) > div.chat-footer-right > button";
        public const string lastChatMessageSelector = "#main-content > div.chat > div.right-column > div.chat__message-list > div > div > div.chat__message.chat__message--gray > div > p";

        public const string activityThirdItemTypeSelector = "#main-content > div.content > div:nth-child(2) > div.post-label > button";
        public const string activityThirdItemCaptionSelector = "#main-content > div.content > div:nth-child(2) > div.post-main > div > div.vote__header > h3";
        public const string activitySecondItemTypeSelector = "#main-content > div.content > div:nth-child(3) > div.post-label > a > button";
        public const string activitySecondItemCaptionSelector = "#main-content > div.content > div:nth-child(3) > div.post > div.post-main > div > div.poll__header > h3";
        public const string activityFirstItemTypeSelector = "#main-content > div.content > div:nth-child(4) > div.post-label > a > button";
        public const string activityFirstItemCaptionSelector = "#main-content > div.content > div:nth-child(4) > div.post-main > article";

        const string activityPageUserNameSelector = "#main-content > div.right-menu-container > div > div.right-menu > div > h3";

        const string firstNeighbourSelector = "#main-content > div:nth-child(2) > div.content > div > div.basic-block__content > div:nth-child(1) > div.neighbor-item__info > a:nth-child(1)";



        const string cmsLoginFieldSelector = "#Name";
        const string cmsPasswordFieldSelector = "#Password";
        const string cmsLoginButtonSelector = ".btn";

        const string cmsVotingMenuItemSelector = "body > div.navbar.navbar-inverse > div > div.navbar-collapse.collapse > ul:nth-child(1) > li:nth-child(4) > a";
        const string cmsVotingOnModerationSelector = "body > div.container.body-content > p:nth-child(5) > a";
        const string cmsVotingOnModerationAcceptFirstItemButtonSelector = "body > div.container.body-content > table > tbody > tr:nth-child(2) > td:nth-child(4) > a:nth-child(1)";

        //const string accountMenuSelector = "#react-root > div > header > div.header__container > ul.header__items > li:nth-child(4) > a";
        /*const string realtyMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(4) > li";
        const string statusButtonSelector = "#react-root > div > header > div.header__container > ul.header__items > li:nth-child(3)";
        const string statusHeaderSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.status-menu > div:nth-child(2) > div.status-menu-header > span";
        const string statusSubheaderSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.status-menu > div:nth-child(2) > div.status-menu-content > h3";
        const string statusTextSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.status-menu > div:nth-child(2) > div.status-menu-content > p";
        const string profileHeaderSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.status-menu > div:nth-child(3) > div.status-menu-header > span";
        const string profileSubheaderSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.status-menu > div:nth-child(3) > div.status-menu-content > h3";
        const string profileTextSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.status-menu > div:nth-child(3) > div.status-menu-content > p";
        const string leaseExpireDateSelector = "#date-input";
        const string leaseExpireDateNextMonthButtonSelector = "#datepicker-container > div > div > div.DayPicker-NavBar > span.DayPicker-NavButton.DayPicker-NavButton--next";
        const string leaseExpireDateSomeDaySelectionSelector = "#datepicker-container > div > div > div.DayPicker-Months > div > div.DayPicker-Body > div:nth-child(4) > div:nth-child(4)";
        const string leasecohabitant = "#modal-add_user > div > div.modal__content > div:nth-child(5) > input:nth-child(2)";
        const string ownerPhoneSelector = "#phone";
        const string ownershipTypeCohabitantSelector = "#modal-add_user > div > div.modal__content > div:nth-child(3) > div > div.select__list > div > li:nth-child(2)";
        const string exitOptionAccountMenuSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.header__profile-menu > ul > a:nth-child(4) > li";
        const string confirmTenantButtonSelector = "#main-content > div:nth-child(2) > div > div:nth-child(1) > div > div > div.settings-item__center > div > button.button-red.button-small";
        const string declineTenantButtonSelector = "#main-content > div:nth-child(2) > div > div:nth-child(1) > div > div > div.settings-item__center > div > button.button-white.button-small";
        const string tenantMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(5) > li";
        const string cmsAccountButtonSelector = "body > div.navbar.navbar-inverse.navbar-fixed-top > div > div.navbar-collapse.collapse > ul:nth-child(1) > li:nth-child(5) > a";
        const string cmsAccountSearchFieldSelector = "#Phone";
        const string cmsAccountFindAccountButtonSelector = "body > div.container.body-content > form > div > div:nth-child(3) > div > input";
        const string editPersonalDataOptionAccountMenuSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.header__profile-menu > ul > a:nth-child(2) > li";
        const string personValidationRequestButtonSelector = "#main-content > div:nth-child(2) > div > div:nth-child(3) > div > button";
        const string dynamicallyLoadedBlockSelector = "#main-content > div.content > div:nth-child(2) > div.basic-block__content";*/

        static public void Authorization(RemoteWebDriver driver, string login)
        {
            //driver.FindElementByCssSelector(authorizationButtonSelector).Click();
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(login);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
        }

        /*static public void Deauthorization(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(profileMenuButtonSelector).Click();
            driver.FindElementByCssSelector(profileLogoutButtonSelector).Click();
            driver.FindElementByCssSelector(logoSelector).Click();
        }*/

        static public void CreateNews(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(newsSideMenuItemSelector).Click();
            driver.FindElementByCssSelector(homeSelector).Click();
            driver.FindElementByCssSelector(createContentButtonSelector).Click();
            newsText = "Autotest"/*RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 200)*/;
            driver.FindElementByCssSelector(createNewsTextareaSelector).SendKeys(newsText);
            driver.FindElementByCssSelector(createNewsOKButtonSelector).Click();
        }

        static public void CreateNewsWithAttachment(RemoteWebDriver driver)
        {
            //driver.FindElementByCssSelector(addAttachmentButtonSelector);
            //driver.findElement(By.xpath(locator)).sendKeys(file.getAbsolutePath());
            driver.FindElementByCssSelector(newsSideMenuItemSelector).Click();
            driver.FindElementByCssSelector(homeSelector).Click();
            driver.FindElementByCssSelector(createContentButtonSelector).Click();
            newsText = "Autotest with attachment"/*RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 200)*/;
            driver.FindElementByCssSelector(createNewsTextareaSelector).SendKeys(newsText);
            String script = "document.getElementById('fileName').value='" + "C:\\\\Users\\\\kirpichev\\\\Downloads\\\\TestMaterials\\\\Piczes\\\\Random\\\\1.jpg" + "';";
            ((IJavaScriptExecutor)driver).ExecuteScript(script);
            driver.FindElementByCssSelector(createNewsOKButtonSelector).Click();
        }

        /*static public void CreatePoll(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(pollSideMenuItemSelector).Click();
            driver.FindElementByCssSelector(createPollButtonSelector).Click();
            pollText = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 250);
            pollFirstOption = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 100);
            pollSecondOption = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 127);     //не верьте сообщению валидации - максимум 127, а не 128!
            driver.FindElementByCssSelector(createPollTextareaSelector).SendKeys(pollText);
            driver.FindElementByCssSelector(firstPollOptionTextareaSelector).SendKeys(pollFirstOption);
            driver.FindElementByCssSelector(secondPollOptionTextareaSelector).SendKeys(pollSecondOption);
            driver.FindElementByCssSelector(createPollOKButtonSelector).Click();
        }

        static public void CreateVoting(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(votingSideMenuItemSelector).Click();
            driver.FindElementByCssSelector(createVotingButtonSelector).Click();
            votingText = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 150);
            votingFirstHeader = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 150);
            votingFirstText = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 250);
            votingSecondHeader = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 150);
            votingSecondText = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 250);
            driver.FindElementByCssSelector(createVotingTextareaSelector).SendKeys(votingText);
            AddVotingItem(driver, votingFirstHeader, votingFirstText);
            AddVotingItem(driver, votingSecondHeader, votingSecondText);
            driver.FindElementByCssSelector(addVotingOKButtonSelector).Click();
        }

        static public void AddVotingItem(RemoteWebDriver driver, string header, string text)
        {
            driver.FindElementByCssSelector(addVotingItemButtonSelector).Click();
            driver.FindElementByCssSelector(addVotingItemPopupHeaderSelector).SendKeys(header);
            driver.FindElementByCssSelector(addVotingItemPopupTextSelector).SendKeys(text);
            driver.FindElementByCssSelector(addVotingItemPopupOKButtonSelector).Click();
        }

        static public void AcceptVoting(RemoteWebDriver driver)
        {
            driver.Navigate().GoToUrl(cmsUrl);
            driver.FindElementByCssSelector(cmsLoginFieldSelector).SendKeys(devLogin);
            driver.FindElementByCssSelector(cmsPasswordFieldSelector).SendKeys(devPassword);
            driver.FindElementByCssSelector(cmsLoginButtonSelector).Click();
            driver.FindElementByCssSelector(cmsVotingMenuItemSelector).Click();
            driver.FindElementByCssSelector(cmsVotingOnModerationSelector).Click();
            driver.FindElementByCssSelector(cmsVotingOnModerationAcceptFirstItemButtonSelector).Click();
            driver.Navigate().GoToUrl(baseUrl);
        }

        static public void ViewNewsDetails(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(newsSideMenuItemSelector).Click();
            driver.FindElementByCssSelector(newsDetailsButtonSelector).Click();
        }

        static public void ViewPollDetails(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(pollSideMenuItemSelector).Click();
            driver.FindElementByCssSelector(pollDetailsButtonSelector).Click();
        }

        static public void ViewVotingDetails(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(votingSideMenuItemSelector).Click();
            driver.FindElementByCssSelector(votingDetailsButtonSelector).Click();
        }

        static public void AddOwnedProperty(RemoteWebDriver driver)
        {                                                               //при добавлении ещё одной квартиры по тому же адресу попап перехода к недвижимости не отображается (вероятно, необходимо быть собственником, добавляться собственником, иметь активной добавляемую собственность)
            driver.FindElementByCssSelector(propertySideMenuItemSelector).Click();
            driver.FindElementByCssSelector(addPropertyButtonSelector).Click();
            List<string> addressesList = AutotestSupport.AutotestSupport.ParseCsv("C:/Users/kirpichev/Downloads/curl-7.61.1_8-win64-mingw/curl-7.61.1-win64-mingw/bin/Locations/result.csv");
            randomAddress = RandoDotNet.RandoFramework.PickRandomListElement(addressesList);
            randomFlatNumber = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.numericCharSet, 2);
            driver.FindElementByCssSelector(propertyAddressInputSelector).SendKeys(randomAddress);
            driver.FindElementByCssSelector(propertyAddressFirstOptionSelector).Click();
            driver.FindElementByCssSelector(propertyFlatNumberSelector).SendKeys(randomFlatNumber);
            driver.FindElementByCssSelector(propertyAddingNextButtonSelector).Click();
            driver.FindElementByCssSelector(ownershipTypeChooseSelector).Click();
            driver.FindElementByCssSelector(ownershipTypeOwnerSelector).Click();
            driver.FindElementByCssSelector(propertyAddingAddButtonSelector).Click();
            driver.FindElementByCssSelector(goToNewAddressButtonSelector).Click();
            driver.FindElementByCssSelector(ownedPropertyTabSelector).Click();
            propertySelectorAddress = driver.FindElementByCssSelector(firstPropertyAddressFirstRow).Text + driver.FindElementByCssSelector(Steps.firstPropertyAddressSecondRow).Text;
            randomAddressArray = (randomAddress + ' ' + randomFlatNumber).Split(' ');
        }

        static public void AddLeasedProperty(RemoteWebDriver driver, string ownerName)
        {
            driver.FindElementByCssSelector(homeSelector).Click();
            driver.FindElementByCssSelector(propertySideMenuItemSelector).Click();
            driver.FindElementByCssSelector(addPropertyButtonSelector).Click();
            driver.FindElementByCssSelector(propertyAddressInputSelector).SendKeys(selectedPropertyAddress);
            driver.FindElementByCssSelector(propertyAddressFirstOptionSelector).Click();
            driver.FindElementByCssSelector(propertyFlatNumberSelector).SendKeys(flatNumber);
            driver.FindElementByCssSelector(propertyAddingNextButtonSelector).Click();
            driver.FindElementByCssSelector(leaseDatePickerInputSelector).Click();
            driver.FindElementByCssSelector(leaseDatePickerNextMonthSelector).Click();
            driver.FindElementByCssSelector(leaseDatePickerNextMonthSelector).Click();
            driver.FindElementByCssSelector(leaseDatePickerFirstMondaySelector).Click();
            driver.FindElementByCssSelector(ownerNameFieldSelector).SendKeys(ownerName);
            driver.FindElementByCssSelector(ownerPhoneFieldSelector).SendKeys(ownerLogin);
            driver.FindElementByCssSelector(propertyAddingAddButtonSelector).Click();
            driver.FindElementByCssSelector(goToNewAddressButtonSelector).Click();
        }

        static public void CreateChat(RemoteWebDriver driver)   //может понадобиться переход к недвижимости на Жуковского 58, где всегда дохрена соседей
        {
            driver.FindElementByCssSelector(chatSideMenuItemSelector).Click();
            if (AutotestSupport.AutotestSupport.IsElementPresentByCssSelector(driver, createChatSelector))
            {driver.FindElementByCssSelector(createChatSelector).Click();}
            else
            {driver.FindElementByCssSelector(createFirstChatSelector).Click();}
            chatName = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 25);
            driver.FindElementByCssSelector(chatNameInputSelector).SendKeys(chatName);
            Thread.Sleep(shortRest);
            driver.FindElementByCssSelector(chatSelectAllSelector).Click();
            driver.FindElementByCssSelector(chatOKButtonSelector).Click();
        }

        static public void AddMessageToChat(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(chatSideMenuItemSelector).Click();
            newChatMessage = RandoDotNet.RandoFramework.CreateRandomString(RandoDotNet.RandoFramework.totalCharSet, 200);
            Thread.Sleep(extendedRest);
            driver.FindElementByCssSelector(chatNewMessageInputSelector).SendKeys(newChatMessage);
            driver.FindElementByCssSelector(chatNewMessageButtonSelector).Click();
        }

        static public void ChangeSelectedProperty(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(propertySideMenuItemSelector).Click();
            driver.FindElementByCssSelector(propertySelectorSelector).Click();
            List<IWebElement> properties = driver.FindElements(By.ClassName("select-list__item")).ToList<IWebElement>();
            IWebElement element = RandoDotNet.RandoFramework.PickRandomListElement(properties);
            propertyToSwitchTo = element.Text;
            element.Click();
            Thread.Sleep(shortRest);
            selectedPropertyAddress = driver.FindElementByCssSelector(propertySelectorSelector).Text;
            selectedPropertyAddressesArray = selectedPropertyAddress.Split(' ');
        }

        static public void SwitchToRandomOwnedProperty(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(propertySideMenuItemSelector).Click();
            driver.FindElementByCssSelector(propertySelectorSelector).Click();
            List<IWebElement> properties = driver.FindElements(By.ClassName("select-list__item")).ToList<IWebElement>();
            int elementNumber = r.Next(properties.Count);
            IWebElement element = properties[elementNumber];
            while (!element.Text.Contains("СОБСТВЕННИК"))
            {
                elementNumber = r.Next(properties.Count);
                element = properties[elementNumber];
            }
            string rawSelectedPropertyAddress = element.Text;
            selectedPropertyAddress = rawSelectedPropertyAddress.Substring(0, rawSelectedPropertyAddress.Length - 12);
            element.Click();
            Thread.Sleep(shortRest);
            flatString = Regex.Match(selectedPropertyAddress, @"кв. \d+").Value;
            flatNumber = Regex.Match(flatString, @"\d+").Value;
        }

        static public string[] ReadUserName(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(homeSelector).Click();
            driver.FindElementByCssSelector(activitySideMenuItemSelector).Click();
            string rawUserName = driver.FindElementByCssSelector(activityPageUserNameSelector).Text;
            return rawUserName.Split(' ');
        }

        static public string[] ReadNeighbourName(RemoteWebDriver driver)
        {
            driver.FindElementByCssSelector(homeSelector).Click();
            driver.FindElementByCssSelector(neighboursSideMenuItemSelector).Click();
            string rawNeighbourName = driver.FindElementByCssSelector(firstNeighbourSelector).Text;
            return rawNeighbourName.Split(' ');
        }*/
    }
}
