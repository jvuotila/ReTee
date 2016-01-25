using System.Web.Mvc;
using ReTeeUmbraco.Models;
using SendGrid;
using System.Web.Configuration;
using System;
using System.Net;
using System.Text;

namespace ReTeeUmbraco.Controllers
{
    public class MemberRegistrationFormSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        private string MemberPersonInChargeEmailAddress = "jasenvastaava@retee.golf";
        private string MemberPersonInCharge = "Miikka Siivonen";
        private string AccountNumber = "FI90 5078 1120 0586 35";
        private string Bic = "OKOYFIHH";
        private string ReferenceNumber = "20161";
        private int MemberFeeAdult = 20;
        private int MemberFeeJunior = 10;
        private int MemberFeeSupportMember = 10;
        private int MemberFeeSupporter = 100;
        private int PDGAFeeAdult = 50;
        private int PDGAFeeJunior = 25;
        private int PDGADiscFee = 15;
        private int PDGADiscGolferMagazineFee = 35;

        private enum Totuusarvo
        {
            kyllä,
            ei
        }

        private enum MemberTypes
        {
            täysjäsen,
            juniorjäsen,
            kannatusjäsen_10,
            kannatusjäsen_100
        }

        private string BuildEmailToSportsClub(MemberRegistrationFormViewModel memberRegistrationMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Etunimi: " + memberRegistrationMessage.FirstName).Append("<br/>");
            sb.Append("Sukunimi: " + memberRegistrationMessage.LastName).Append("<br/>");
            sb.Append("Sähköpostiosoite: " + memberRegistrationMessage.EmailAddress).Append("<br/>");
            sb.Append("Puhelinnumero: " + memberRegistrationMessage.PhoneNumber).Append("<br/>");
            sb.Append("Syntymäpäivä: " + memberRegistrationMessage.DateOfBirth).Append("<br/>");
            sb.Append("Katuosoite: " + memberRegistrationMessage.StreetAddress).Append("<br/>");
            sb.Append("Postinumero: " + memberRegistrationMessage.PostalCode).Append("<br/>");
            sb.Append("Postitoimipaikka: " + memberRegistrationMessage.City).Append("<br/>");
            sb.Append("Jäsentyyppi: ");

            switch (memberRegistrationMessage.MemberType)
            {
                case "member":
                    sb.Append(MemberTypes.täysjäsen).Append("<br/>");
                    break;
                case "junior":
                    sb.Append(MemberTypes.juniorjäsen).Append("<br/>");
                    break;
                case "supporter":
                    sb.Append(MemberTypes.kannatusjäsen_100).Append("<br/>");
                    break;
                case "supportmember":
                    sb.Append(MemberTypes.kannatusjäsen_10).Append("<br/>");
                    break;
            }

            if (memberRegistrationMessage.SupportMemberClub != null)
            {
                sb.Append("Ensisijainen jäsenseura: " + memberRegistrationMessage.SupportMemberClub).Append("<br/>");
            }

            if (memberRegistrationMessage.PDGALicense)
            {
                sb.Append("PDGA-lisenssi: " + (memberRegistrationMessage.PDGALicense ? Totuusarvo.kyllä : Totuusarvo.ei)).Append("<br/>");
            }

            if (memberRegistrationMessage.PDGALicenseNumber != "")
            {
                sb.Append("PDGA-numero: " + memberRegistrationMessage.PDGALicenseNumber).Append("<br/>");
            }

            if (memberRegistrationMessage.PDGAMemberDisc)
            {
                sb.Append("Tilaan PDGA-jäsenkiekon: " + (memberRegistrationMessage.PDGAMemberDisc ? Totuusarvo.kyllä : Totuusarvo.ei)).Append("<br/>");
            }

            if (memberRegistrationMessage.DiscGolferMagazine)
            {
                sb.Append("Tilaan PDGA DiscGolfer Magazinen vuosikerran: " + (memberRegistrationMessage.DiscGolferMagazine ? Totuusarvo.kyllä : Totuusarvo.ei));
            }

            return sb.ToString();
        }

        private string BuildMemberEmail(MemberRegistrationFormViewModel memberRegistrationMessage)
        {
            int MembershipFee = 0;

            switch (memberRegistrationMessage.MemberType)
            {
                case "member":
                    MembershipFee += MemberFeeAdult;
                    if (memberRegistrationMessage.PDGALicense)
                    {
                        MembershipFee += PDGAFeeAdult;
                    }
                    break;
                case "junior":
                    MembershipFee += MemberFeeJunior;
                    if (memberRegistrationMessage.PDGALicense)
                    {
                        MembershipFee += PDGAFeeJunior;
                    }
                    break;
                case "supporter":
                    MembershipFee += MemberFeeSupporter;
                    break;
                case "supportmember":
                    MembershipFee += MemberFeeSupportMember;
                    break;
            }

            if (memberRegistrationMessage.PDGAMemberDisc)
            {
                MembershipFee += PDGADiscFee;
            }

            if (memberRegistrationMessage.DiscGolferMagazine)
            {
                MembershipFee += PDGADiscGolferMagazineFee;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("Hei").Append("<br/><br/>");
            sb.Append("Ohessa ReTee ry:n jäsenmaksu vuodelle " + DateTime.Now.Year).Append("<br/><br/>");
            sb.Append("Tilinumero: " + AccountNumber).Append("<br/>");
            sb.Append("BIC: " + Bic).Append("<br/>");
            sb.Append("Saajan nimi: ReTee ry").Append("<br/>");
            sb.Append("Viitenumero: " + ReferenceNumber).Append("<br/>");
            sb.Append("Eräpäivä: " + DateTime.Now.AddDays(14).ToString("dd.MM.yyyy")).Append("<br/>");
            sb.Append("Summa: " + MembershipFee + " euroa").Append("<br/><br/>");
            sb.Append("--").Append("<br/>");
            sb.Append(MemberPersonInCharge).Append("<br/>");
            sb.Append("Jäsenvastaava, ReTee ry").Append("<br/>");
            sb.Append("http://www.retee.golf/");

            return sb.ToString();
        }

        [HttpPost]
        public ActionResult CreateMemberRegistrationMessage(MemberRegistrationFormViewModel memberRegistrationMessage)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            ViewBag.MemberRegistration = memberRegistrationMessage;

            var appName = WebConfigurationManager.AppSettings["sendgridAppName"];
            var contactEmailAddress = WebConfigurationManager.AppSettings["contactEmailAddress"];
            var sendgridUsername = WebConfigurationManager.AppSettings["sendgridUsername"];
            var sendgridPassword = WebConfigurationManager.AppSettings["sendgridPassword"];
            if (appName != null &&
                contactEmailAddress != null &&
                sendgridUsername != null &&
                sendgridPassword != null)
            {
                var SportsClubEmail = new SendGridMessage();
                SportsClubEmail.From = new System.Net.Mail.MailAddress(memberRegistrationMessage.EmailAddress);
                SportsClubEmail.AddTo(contactEmailAddress);
                SportsClubEmail.Subject = "Jäsenhakemus: " + memberRegistrationMessage.FirstName + " " + memberRegistrationMessage.LastName;

                SportsClubEmail.Html = BuildEmailToSportsClub(memberRegistrationMessage);


                var MemberEmail = new SendGridMessage();
                MemberEmail.From = new System.Net.Mail.MailAddress(MemberPersonInChargeEmailAddress);
                MemberEmail.AddTo(memberRegistrationMessage.EmailAddress);
                MemberEmail.Subject = "ReTee ry jäsenmaksu vuodelle " + DateTime.Now.Year;

                MemberEmail.Html = BuildMemberEmail(memberRegistrationMessage);

                var credentials = new System.Net.NetworkCredential(sendgridUsername, sendgridPassword);
                var transportWeb = new Web(credentials);
                transportWeb.DeliverAsync(SportsClubEmail);
                transportWeb.DeliverAsync(MemberEmail);

                // Lisätään jäsen rekisteriin
                try { 
                    var memberService = Services.MemberService;
                    var member = memberService.CreateMemberWithIdentity(memberRegistrationMessage.EmailAddress, memberRegistrationMessage.EmailAddress, (memberRegistrationMessage.LastName + " " + memberRegistrationMessage.FirstName), "ReTeeMember");
                    member.SetValue("firstName", memberRegistrationMessage.FirstName);
                    member.SetValue("lastName", memberRegistrationMessage.LastName);
                    member.SetValue("phoneNumber", memberRegistrationMessage.PhoneNumber);
                    member.SetValue("birthDate", DateTime.Parse(memberRegistrationMessage.DateOfBirth));
                    member.SetValue("streetAddress", memberRegistrationMessage.StreetAddress);
                    member.SetValue("postalCode", memberRegistrationMessage.PostalCode);
                    member.SetValue("postOffice", memberRegistrationMessage.City);
                    member.SetValue("memberType", "member");
                    member.SetValue("mainClub", memberRegistrationMessage.SupportMemberClub);
                    member.SetValue("pdgaLicenseNumber", memberRegistrationMessage.PDGALicenseNumber);

                    var dts = Services.DataTypeService;

                    // Get an instance of the status editor
                    var ReTeeMemberTypesDataType = dts.GetDataTypeDefinitionByName("ReTee Member Type");
                    var ReTeeMemberTypes = dts.GetPreValuesCollectionByDataTypeId(ReTeeMemberTypesDataType.Id).PreValuesAsDictionary;

                    switch (memberRegistrationMessage.MemberType)
                    {
                        case "member":
                            member.SetValue("memberType", ReTeeMemberTypes["0"].Id);
                            break;
                        case "junior":
                            member.SetValue("memberType", ReTeeMemberTypes["0"].Id);
                            break;
                        case "supporter":
                            member.SetValue("memberType", ReTeeMemberTypes["2"].Id);
                            break;
                        case "supportmember":
                            member.SetValue("memberType", ReTeeMemberTypes["1"].Id);
                            break;
                    }

                    memberService.Save(member);
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }

                //memberService.SavePassword(member, model.Password);
            }

            return CurrentUmbracoPage();
        }
    }
}