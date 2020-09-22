using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace iLive_BDT
{
    [Binding]
    public class SpecFlowFeatureStatusSteps
    {
        private RemoteWebDriver driver = (RemoteWebDriver)ScenarioContext.Current["driver"];

        TimeSpan waitForNextTurn = new TimeSpan(1000);
        TimeSpan shortRest = new TimeSpan(5000);
        TimeSpan extendedRest = new TimeSpan(25000);

        String currentUser = String.Empty;
        String lastUser = String.Empty;

        static int ownerPropertyIndex = 3;
        static int leaserPropertyIndex = 3;
        static int cohabitantPropertyIndex = 3;

        const string siteUrl = "http://ilive-web.cmsplanet.ru/";
        const string cmsUrl = "http://ilive.cmsplanet.ru:5005/";

        const string devLogin = "Developer";
        const string devPassword = "TrEdramEFegaSWEw4MuC";
        const string ownerLogin = "9653944057";
        const string leaserLogin = "9062866102";
        const string cohabitantLogin = "9632289456";
        const string password = "!nW0rkPassw0rd";

        const string ownerLastName = "Офал";

        const string leaseExpirationDate = "12122020";

        const string firstPropertyAddress = "обл Тульская г Тула ул Бондаренко д 5";
        const string firstPropertyFlatNumber = "18";
        const string secondPropertyAddress = "обл Тульская г Тула ул Кирова д 25";
        const string secondPropertyFlatNumber = "25";
        const string thirdPropertyAddress = "обл Тульская г Тула ул Металлургов д 86";
        const string thirdPropertyFlatNumber = "51";
        const string nonexistantPropertyAddress = "обл Тульская г Тула ул Калинина д 77";
        const string nonexistantPropertyFlatNumber = "666";

        const string propertyStatusHeader = "Ваше отношение к недвижимости";
        const string profileStatusHeader = "Статус вашего профиля в ЖИВУ.РФ";
        const string errorStatusHeader = "Нет данных";
        const string statusForOwnedButModeratedPropertySubheader = "Заявка на модерации";
        const string statusForOwnedButModeratedPropertyText = "Мы отправили запрос в Росреестр. Вы можете просматривать и создавать новости и опросы. Также вы можете участвовать в чатах дома и создавать чаты. Доступ к данной недвижимости действует в течение 30 дней! Как только мы подтвердим факт владения вами данным недвижимым имуществом, вы получите постоянный доступ к недвижимости. Чтобы начать голосовать, вам необходим проверенный статус пользовательского профиля. Спасибо, что вы с нами!";
        const string statusForLeasedButYetHasnTBeenApprovedPropertySubheader = "Заявка отправлена собственнику";
        const string statusForLeasedButYetHasnTBeenApprovedPropertyText = "Факт аренды ожидает подтверждения собственником. Вы можете просматривать и создавать новости, опросы и чаты. В течение 30 дней собственник должен ответить на заявку. Как только собственник подтвердит факт аренды вами данного объекта недвижимого имущества, вы получите постоянный доступ к недвижимости. Спасибо за понимание!";
        const string statusForCohabitationThatYetHasnTBeenApprovedSubheader = "Заявка отправлена собственнику";
        const string statusForCohabitationThatYetHasnTBeenApprovedText = "Факт вашего проживания в данной недвижимости ожидает подтверждения собственником. Вы можете просматривать и создавать новости, опросы и чаты. В течение 30 дней собственник должен ответить на заявку. Как только собственник подтвердит факт проживания вами в данном объекте недвижимого имущества, вы получите постоянный доступ к недвижимости. Спасибо за понимание!";
        const string statusForAcceptedLeaseOfModeratedPropertySubheader = "Вы арендуете данную недвижимость";
        const string statusForAcceptedLeaseOfModeratedPropertyText = "Факт аренды подтвержден собственником. Вы можете просматривать и создавать новости и опросы. Также вы можете участвовать в чатах дома и создавать чаты. ВНИМАНИЕ: Права собственника на недвижимость ожидают подтверждения. В случае неподтверждения факта владения объектом недвижимости собственником, ваш доступ к данной недвижимости будет приостановлен. Спасибо, что вы с нами!";
        const string statusForAcceptedCohabitanceOfModeratedPropertySubheader = "Вы проживаете в данной недвижимости как сожитель";
        const string statusForAcceptedCohabitanceOfModeratedPropertyText = "Ваше отношение к недвижимости подтверждено собственником. Вы можете просматривать и создавать новости и опросы. Также вы можете участвовать в чатах дома и создавать чаты. ВНИМАНИЕ: Права собственника на недвижимость ожидают подтверждения. В случае неподтверждения факта владения объектом недвижимости собственником, ваш доступ к данной недвижимости будет приостановлен. Спасибо, что вы с нами!";
        const string statusForOwnedAndAcceptedPropertySubheader = "Вы владеете данной недвижимостью";
        const string statusForOwnedAndAcceptedPropertyText = "Ваша недвижимость подтверждена Росреестром. Вы можете просматривать и создавать новости и опросы. Также вы можете участвовать в чатах дома и создавать чаты. Чтобы начать голосовать, вам необходим подтвержденный статус профиля пользователя. Спасибо, что вы с нами!";
        const string statusForAcceptedLeaseSubheader = "Вы арендуете данную недвижимость";
        const string statusForAcceptedLeaseText = "Факт аренды подтвержден собственником. Вы можете просматривать и создавать новости и опросы. Также вы можете участвовать в чатах дома и создавать чаты. Спасибо, что вы с нами!";
        const string statusForAcceptedCohabitanceSubheader = "Вы проживаете в данной недвижимости как сожитель";
        const string statusForAcceptedCohabitanceText = "Ваше отношение к недвижимости подтверждено собственником. Вы можете просматривать и создавать новости и опросы. Также вы можете участвовать в чатах дома и создавать чаты. Спасибо, что вы с нами!";
        const string statusForOwnedAndDeclinedPropertySubheader = "Заявка отклонена";
        const string statusForOwnedAndDeclinedPropertyText = "Заявка отклонена /nНе удалось получить подтверждение по вашему объекту.Обратитесь в службу поддержки системы ЖИВУ.РФ.";
        const string statusForForAcceptedLeaseOfDeclinedPropertySubheader = "Заявка отклонена";
        const string statusForForAcceptedLeaseOfDeclinedPropertyText = "Поскольку право собственника не подтвердилось, ваша заявка не может быть одобрена.";
        const string statusForAcceptedCohabitanceOfDeclinedPropertySubheader = "Заявка отклонена";
        const string statusForAcceptedCohabitanceOfDeclinedPropertyText = "Поскольку право собственника не подтвердилось, ваша заявка не может быть одобрена.";
        const string statusForDeclinedLeaseSubheader = "Заявка отклонена";
        const string statusForDeclinedLeaseText = "К сожалению, собственник не подтвердил вас в качестве арендатора.";
        const string statusForDeclinedCohabitanceSubheader = "Заявка отклонена";
        const string statusForDeclinedCohabitanceText = "К сожалению, собственник не подтвердил вас в качестве сожителя.";
        const string statusForUnconfirmedProfileSubheader = "Ваш профиль не подтвержден!";
        const string statusForUnconfirmedProfileText = "Вы не можете создавать и участвовать в голосованиях.";
        const string statusForModeratedProfileSubtitle = "Ваш профиль на модерации";
        const string statusForModeratedProfileText = "Вы не можете создавать и участвовать в голосованиях";
        const string statusForAcceptedProfileSubheader = "Ваш профиль подтвержден!";
        const string statusForAcceptedProfileText = "Вы можете создавать и участвовать в голосованиях при условии, что вы являетесь собственником и ваш дом подключен к ЖИВУ.РФ. Спасибо, что вы с нами";
        const string statusForDeclinedProfileSubheader = "Ваш профиль не прошел модерацию!";
        const string statusForDeclinedProfileText = "Вы не можете создавать и участвовать в голосованиях.";
        const string statusForNon_ExistantPropertySubheader = "Не удалось получить выписку ЕГРН";
        const string statusForNon_ExistantPropertyText = "По вашему запросу не удалось найти кадастровый номер объекта недвижимости, расположенного по адресу: " + nonexistantPropertyAddress;

        const string loginPhoneNumberSelector = "#react-root > div > div.container__landing > section > div > form > input[type=\"text\"]";
        const string loginPasswordSelector = "#react-root > div > div.container__landing > section > div > form > div > input[type=\"password\"]";
        const string loginButtonSelector = "#react-root > div > div.container__landing > section > div > form > button";
        const string realtyMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(4) > li";
        const string addRealtyButtonSelector = "#main-content > div.content > div:nth-child(1) > header > a";
        const string realtyAddressInputSelector = "#modal-add_house > div > div > div.modal__select-container > div > div > input[type=\"text\"]";
        const string realtyAddressFirstOptionSelector = "#modal-add_house > div > div > div.modal__select-container > div > div.select__list > li:nth-child(1)";
        const string realtyFlatNumberSelector = "#modal-add_house > div > div > div > div > div > div.input-container.flat-input > div.input-container.flat-input > input[type=\"text\"]";
        const string realtyAddingNextButtonSelector = "#modal-add_house > div > div > div > button";
        const string ownershipTypeChooseSelector = "#modal-add_user > div > div.modal__content > div:nth-child(3) > div > div.select__button";
        const string ownershipTypeOwnerSelector = "#modal-add_user > div > div.modal__content > div:nth-child(3) > div > div.select__list > div > li:nth-child(3)";
        const string realtyAddingAddButtonSelector = "#modal-add_user > div > div.modal__footer > button.button-red.button-small";
        const string goToNewAddressButtonSelector = "button.button-red:nth-child(3)" /*"#main-content > div.content > div.modal.modal--centered > div > div > button.button-red"*/;
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
        const string accountMenuSelector = "#react-root > div > header > div.header__container > ul.header__items > li:nth-child(4) > a";
        const string exitOptionAccountMenuSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.header__profile-menu > ul > a:nth-child(4) > li";
        const string confirmTenantButtonSelector = "#main-content > div:nth-child(2) > div > div:nth-child(1) > div > div > div.settings-item__center > div > button.button-red.button-small";
        const string declineTenantButtonSelector = "#main-content > div:nth-child(2) > div > div:nth-child(1) > div > div > div.settings-item__center > div > button.button-white.button-small";
        const string tenantMenuItemSelector = "#main-content > div.left-menu > ul > a:nth-child(5) > li";
        const string cmsLoginFieldSelector = "#Name";
        const string cmsPasswordFieldSelector = "#Password";
        const string cmsAccountButtonSelector = "body > div.navbar.navbar-inverse.navbar-fixed-top > div > div.navbar-collapse.collapse > ul:nth-child(1) > li:nth-child(5) > a";
        const string cmsAccountSearchFieldSelector = "#Phone";
        const string cmsAccountFindAccountButtonSelector = "body > div.container.body-content > form > div > div:nth-child(3) > div > input";
        const string editPersonalDataOptionAccountMenuSelector = "#react-root > div > header > div.header__container > ul.header__items > li.active > div.header__profile-menu > ul > a:nth-child(2) > li";
        const string personValidationRequestButtonSelector = "#main-content > div:nth-child(2) > div > div:nth-child(3) > div > button";
        const string dynamicallyLoadedBlockSelector = "#main-content > div.content > div:nth-child(2) > div.basic-block__content";
        const string cmsLoginButtonSelector = ".btn";

        [Given(@"Authorized as owner")]
        public void GivenAuthorizedAsOwner()
        {
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(ownerLogin);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
            currentUser = ownerLogin;
        }

        [Given(@"Authorized as leaser")]
        public void GivenAuthorizedAsLeaser()
        {
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(leaserLogin);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
            currentUser = leaserLogin;
        }

        [Given(@"Authorized as cohabitant")]
        public void GivenAuthorizedAsCohabitant()
        {
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(cohabitantLogin);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
            currentUser = cohabitantLogin;
        }

        [Given(@"Property added to it's owner")]
        public void GivenPropertyAddedToItSOwner()
        {
            driver.FindElementByCssSelector(realtyMenuItemSelector).Click();
            Thread.Sleep(shortRest);
            driver.FindElementByCssSelector(addRealtyButtonSelector).Click();
            driver.FindElementByCssSelector(realtyAddressInputSelector).SendKeys(firstPropertyAddress);
            driver.FindElementByCssSelector(realtyAddressFirstOptionSelector).Click();
            driver.FindElementByCssSelector(realtyFlatNumberSelector).SendKeys(firstPropertyFlatNumber);
            driver.FindElementByCssSelector(realtyAddingNextButtonSelector).Click();
            driver.FindElementByCssSelector(ownershipTypeChooseSelector).Click();
            driver.FindElementByCssSelector(ownershipTypeOwnerSelector).Click();
            driver.FindElementByCssSelector(realtyAddingAddButtonSelector).Click();
            driver.FindElementByCssSelector(goToNewAddressButtonSelector).Click();
            ownerPropertyIndex = ownerPropertyIndex + 1;
        }

        [Given(@"Property added to it's leaser")]
        public void GivenPropertyAddedToItSLeaser()
        {
            driver.FindElementByCssSelector(realtyMenuItemSelector).Click();
            Thread.Sleep(shortRest);
            driver.FindElementByCssSelector(addRealtyButtonSelector).Click();
            driver.FindElementByCssSelector(realtyAddressInputSelector).SendKeys(firstPropertyAddress);
            driver.FindElementByCssSelector(realtyAddressFirstOptionSelector).Click();
            driver.FindElementByCssSelector(realtyFlatNumberSelector).SendKeys(firstPropertyFlatNumber);
            driver.FindElementByCssSelector(realtyAddingNextButtonSelector).Click();
            driver.FindElementByCssSelector(leaseExpireDateSelector).Click();
            driver.FindElementByCssSelector(leaseExpireDateNextMonthButtonSelector).Click();
            driver.FindElementByCssSelector(leaseExpireDateSomeDaySelectionSelector).Click();
            driver.FindElementByCssSelector(leasecohabitant).SendKeys(ownerLastName);
            driver.FindElementByCssSelector(ownerPhoneSelector).SendKeys(ownerLogin);
            driver.FindElementByCssSelector(realtyAddingAddButtonSelector).Click();
            driver.FindElementByCssSelector(goToNewAddressButtonSelector).Click();
            Thread.Sleep(shortRest);
            leaserPropertyIndex = leaserPropertyIndex + 1;
        }

        [Given(@"Property added to it's cohabitant")]
        public void GivenPropertyAddedToItSCohabitant()
        {
            driver.FindElementByCssSelector(realtyMenuItemSelector).Click();
            Thread.Sleep(shortRest);
            driver.FindElementByCssSelector(addRealtyButtonSelector).Click();
            driver.FindElementByCssSelector(realtyAddressInputSelector).SendKeys(firstPropertyAddress);
            driver.FindElementByCssSelector(realtyAddressFirstOptionSelector).Click();
            driver.FindElementByCssSelector(realtyFlatNumberSelector).SendKeys(firstPropertyFlatNumber);
            driver.FindElementByCssSelector(realtyAddingNextButtonSelector).Click();
            driver.FindElementByCssSelector(ownershipTypeChooseSelector).Click();
            driver.FindElementByCssSelector(ownershipTypeCohabitantSelector).Click();
            driver.FindElementByCssSelector(leasecohabitant).SendKeys(ownerLastName);
            driver.FindElementByCssSelector(ownerPhoneSelector).SendKeys(ownerLogin);
            driver.FindElementByCssSelector(realtyAddingAddButtonSelector).Click();
            driver.FindElementByCssSelector(goToNewAddressButtonSelector).Click();
            cohabitantPropertyIndex = cohabitantPropertyIndex + 1;
        }

        [When(@"Status button is pressed")]
        public void WhenStatusButtonIsPressed()
        {
            Thread.Sleep(extendedRest);
            driver.Navigate().Refresh();    //статус висит, пока заявка создаётся; что это означает - не оч понятно, но, по ходу, нужно
            driver.FindElementByCssSelector(statusButtonSelector).Click();
            while (driver.FindElementByCssSelector(statusHeaderSelector).Text == "Заявка формируется")
            {
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                Thread.Sleep(shortRest);
            }
        }

        [Given(@"Lease/cohabitance is accepted")]
        public void GivenLeaseCohabitanceIsAccepted()
        {
            driver.FindElementByCssSelector(accountMenuSelector).Click();
            driver.FindElementByCssSelector(exitOptionAccountMenuSelector).Click();
            lastUser = currentUser;
            currentUser = ownerLogin;
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(ownerLogin);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
            driver.FindElementByCssSelector(tenantMenuItemSelector).Click();
            driver.FindElementByCssSelector(confirmTenantButtonSelector).Click();
            driver.FindElementByCssSelector(accountMenuSelector).Click();
            driver.FindElementByCssSelector(exitOptionAccountMenuSelector).Click();
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(lastUser);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
            driver.FindElementByCssSelector(realtyMenuItemSelector).Click();
            currentUser = lastUser;
            lastUser = ownerLogin;
        }

        [Given(@"Owned property is accepted")]
        public void GivenOwnedPropertyIsAccepted()
        {
            driver.Navigate().GoToUrl(cmsUrl);
            driver.FindElementByCssSelector(cmsLoginFieldSelector).SendKeys(devLogin);
            driver.FindElementByCssSelector(cmsPasswordFieldSelector).SendKeys(devPassword);
            driver.FindElementByCssSelector(cmsLoginButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountSearchFieldSelector).SendKeys("7" + currentUser);
            driver.FindElementByCssSelector(cmsAccountFindAccountButtonSelector).Click();
            string cmsPropertyAcceptButtonSelector = "body > div.container.body-content > form:nth-child(" + ownerPropertyIndex + ") > div > div > div:nth-child(1) > input";
            driver.FindElementByCssSelector(cmsPropertyAcceptButtonSelector).Click();
            driver.Navigate().GoToUrl(siteUrl);
        }

        [Given(@"Owned property is declined")]
        public void GivenOwnedPropertyIsDeclined()
        {
            driver.Navigate().GoToUrl(cmsUrl);
            driver.FindElementByCssSelector(cmsLoginFieldSelector).SendKeys(devLogin);
            driver.FindElementByCssSelector(cmsPasswordFieldSelector).SendKeys(devPassword);
            driver.FindElementByCssSelector(cmsLoginButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountSearchFieldSelector).SendKeys("7" + currentUser);
            driver.FindElementByCssSelector(cmsAccountFindAccountButtonSelector).Click();
            string cmsPropertyAcceptButtonSelector = "body > div.container.body-content > form:nth-child(" + ownerPropertyIndex + ") > div > div > div:nth-child(3) > input";
            driver.FindElementByCssSelector(cmsPropertyAcceptButtonSelector).Click();
            driver.Navigate().GoToUrl(siteUrl);
        }

        [Given(@"Lease/cohabitance is declined")]
        public void GivenLeaseCohabitanceIsDeclined()
        {
            driver.FindElementByCssSelector(accountMenuSelector).Click();
            driver.FindElementByCssSelector(exitOptionAccountMenuSelector).Click();
            lastUser = currentUser;
            currentUser = ownerLogin;
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(ownerLogin);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
            driver.FindElementByCssSelector(tenantMenuItemSelector).Click();
            driver.FindElementByCssSelector(declineTenantButtonSelector).Click();
            driver.FindElementByCssSelector(accountMenuSelector).Click();
            driver.FindElementByCssSelector(exitOptionAccountMenuSelector).Click();
            driver.FindElementByCssSelector(loginPhoneNumberSelector).SendKeys(lastUser);
            driver.FindElementByCssSelector(loginPasswordSelector).SendKeys(password);
            driver.FindElementByCssSelector(loginButtonSelector).Click();
            currentUser = lastUser;
            lastUser = ownerLogin;
        }

        [Given(@"Confirmation form is filled")]
        public void GivenConfirmationFormIsFilled()
        {
            driver.FindElementByCssSelector(accountMenuSelector).Click();
            driver.FindElementByCssSelector(editPersonalDataOptionAccountMenuSelector).Click();
            driver.FindElementByCssSelector(personValidationRequestButtonSelector).Click();
            //нафигачить логику заполнения паспортных данных, когда её выльют на прод
        }

        [Given(@"Profile is accepted")]
        public void GivenProfileIsAccepted()
        {
            driver.Navigate().GoToUrl(cmsUrl);
            driver.FindElementByCssSelector(cmsLoginFieldSelector).SendKeys(devLogin);
            driver.FindElementByCssSelector(cmsPasswordFieldSelector).SendKeys(devPassword);
            driver.FindElementByCssSelector(cmsLoginButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountSearchFieldSelector).SendKeys("7" + currentUser);
            driver.FindElementByCssSelector(cmsAccountFindAccountButtonSelector).Click();
            //нафигачить логику подтверждения паспортных данных, когда её выльют на прод
            driver.Navigate().GoToUrl(siteUrl);
        }

        [Given(@"Profile is declined")]
        public void GivenProfileIsDeclined()
        {
            driver.Navigate().GoToUrl(cmsUrl);
            driver.FindElementByCssSelector(cmsLoginFieldSelector).SendKeys(devLogin);
            driver.FindElementByCssSelector(cmsPasswordFieldSelector).SendKeys(devPassword);
            driver.FindElementByCssSelector(cmsLoginButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountButtonSelector).Click();
            driver.FindElementByCssSelector(cmsAccountSearchFieldSelector).SendKeys("7" + currentUser);
            driver.FindElementByCssSelector(cmsAccountFindAccountButtonSelector).Click();
            //нафигачить логику отклонения паспортных данных, когда её выльют на прод
            driver.Navigate().GoToUrl(siteUrl);
        }

        [Then(@"Status for owned, but moderated property can be found")]
        public void ThenStatusForOwnedButModeratedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForOwnedButModeratedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForOwnedButModeratedPropertyText);
        }

        [Then(@"Status for leased, but yet hasn't been approved property can be found")]
        public void ThenStatusForLeasedButYetHasnTBeenApprovedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForLeasedButYetHasnTBeenApprovedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForLeasedButYetHasnTBeenApprovedPropertyText);
        }

        [Then(@"Status for cohabitation that yet hasn't been approved can be found")]
        public void ThenStatusForCohabitationThatYetHasnTBeenApprovedCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForCohabitationThatYetHasnTBeenApprovedSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForCohabitationThatYetHasnTBeenApprovedText);
        }

        [Then(@"Status for accepted lease of moderated property can be found")]
        public void ThenStatusForAcceptedLeaseOfModeratedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForAcceptedLeaseOfModeratedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForAcceptedLeaseOfModeratedPropertyText);
        }

        [Then(@"Status for accepted cohabitance of moderated property can be found")]
        public void ThenStatusForAcceptedCohabitanceOfModeratedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForAcceptedCohabitanceOfModeratedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForAcceptedCohabitanceOfModeratedPropertyText);
        }

        [Then(@"Status for owned and accepted property can be found")]
        public void ThenStatusForOwnedAndAcceptedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForOwnedAndAcceptedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForOwnedAndAcceptedPropertyText);
        }

        [Then(@"Status for accepted lease can be found")]
        public void ThenStatusForAcceptedLeaseCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForAcceptedLeaseSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForAcceptedLeaseText);
        }

        [Then(@"Status for accepted cohabitance can be found")]
        public void ThenStatusForAcceptedCohabitanceCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForAcceptedCohabitanceSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForAcceptedCohabitanceText);
        }

        [Then(@"Status for owned and declined property can be found")]
        public void ThenStatusForOwnedAndDeclinedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForOwnedAndDeclinedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForOwnedAndDeclinedPropertyText);
        }

        [Then(@"Status for for accepted lease of declined property can be found")]
        public void ThenStatusForForAcceptedLeaseOfDeclinedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForForAcceptedLeaseOfDeclinedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForForAcceptedLeaseOfDeclinedPropertyText);
        }

        [Then(@"Status for accepted cohabitance of declined property can be found")]
        public void ThenStatusForAcceptedCohabitanceOfDeclinedPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForAcceptedCohabitanceOfDeclinedPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForAcceptedCohabitanceOfDeclinedPropertyText);
        }

        [Then(@"Status for declined lease can be found")]
        public void ThenStatusForDeclinedLeaseCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForDeclinedLeaseSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForDeclinedLeaseText);
        }

        [Then(@"Status for declined cohabitance can be found")]
        public void ThenStatusForDeclinedCohabitanceCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(statusHeaderSelector).Text.ToString(), propertyStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusSubheaderSelector).Text.ToString(), statusForDeclinedCohabitanceSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(statusTextSelector).Text.ToString(), statusForDeclinedCohabitanceText);
        }

        [Then(@"Status for unconfirmed profile can be found")]
        public void ThenStatusForUnconfirmedProfileCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(profileHeaderSelector).Text.ToString(), profileStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileSubheaderSelector).Text.ToString(), statusForUnconfirmedProfileSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileTextSelector).Text.ToString(), statusForUnconfirmedProfileText);
        }

        [Then(@"Status for moderated profile can be found")]
        public void ThenStatusForModeratedProfileCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(profileHeaderSelector).Text.ToString(), profileStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileSubheaderSelector).Text.ToString(), statusForModeratedProfileSubtitle);
            Assert.AreEqual(driver.FindElementByCssSelector(profileTextSelector).Text.ToString(), statusForModeratedProfileText);
        }

        [Then(@"Status for accepted profile can be found")]
        public void ThenStatusForAcceptedProfileCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(profileHeaderSelector).Text.ToString(), profileStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileSubheaderSelector).Text.ToString(), statusForAcceptedProfileSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileTextSelector).Text.ToString(), statusForAcceptedProfileText);
        }

        [Then(@"Status for declined profile can be found")]
        public void ThenStatusForDeclinedProfileCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(profileHeaderSelector).Text.ToString(), profileStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileSubheaderSelector).Text.ToString(), statusForDeclinedProfileSubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileTextSelector).Text.ToString(), statusForDeclinedProfileText);
        }

        [Then(@"Status for non-existant property can be found")]
        public void ThenStatusForNon_ExistantPropertyCanBeFound()
        {
            Assert.AreEqual(driver.FindElementByCssSelector(profileHeaderSelector).Text.ToString(), profileStatusHeader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileSubheaderSelector).Text.ToString(), statusForNon_ExistantPropertySubheader);
            Assert.AreEqual(driver.FindElementByCssSelector(profileTextSelector).Text.ToString(), statusForNon_ExistantPropertyText);
        }
    }
}
