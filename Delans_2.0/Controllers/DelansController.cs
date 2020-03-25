using Delans.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace Delans.Controllers
{
    public class DelansController : Controller
    {
        public async Task<ActionResult> DetailsInfo(string number)
        {
            try
            {
                return base.PartialView("/Views/Delans/Details.cshtml", await ApiMethods.GetStatusOfInvoice(HttpContext.Session.GetString("BasePath"), HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("ICN"), number));
            }
            catch
            {
                return BadRequest();
            }
        }

        public ActionResult GetCalculatorBody()
        {
            return PartialView("/Views/Delans/BodyCalculator.cshtml");
        }

        public async Task<ActionResult> GetTableBody_Body(DateTime? Start = null)
        {
            try
            {
                return base.PartialView("/Views/Delans/BodyTable_Body.cshtml", 
                    await ApiMethods.GetExt(HttpContext.Session.GetString("BasePath"), HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("ICN")));
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<ActionResult> GetTableBody_Header()
        {
            try
            {
                return base.PartialView("/Views/Delans/BodyTable_Header.cshtml");
            }
            catch
            {
                return BadRequest();
            }
        }

        public ActionResult Main()
        {
            try
            {       
                return View("/Views/Delans/Main.cshtml");
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<ActionResult> Logout()
        {
            try
            {
                bool success = await ApiMethods.Logout(HttpContext.Session.GetString("BasePath"),HttpContext.Session.GetString("SessionID"));

                CleanSessionParams();

                return RedirectToAction("Login", new { token = HttpContext.Session.GetString("Token")});
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<string> TryLogin(string login = "", string password = "")
        {
            LoginResponseBase baseauth = await ApiMethods.GetLoginBase(HttpContext.Session.GetString("Token"));

            if (!String.IsNullOrEmpty(baseauth.link))
            {

                HttpContext.Session.SetString("BasePath", baseauth.link);

                LoginResponse auth = await ApiMethods.Login(HttpContext.Session.GetString("BasePath"), login, password);
                if (!String.IsNullOrEmpty(auth.SessionId))
                {
                    HttpContext.Session.SetString("SessionID", auth.SessionId);
                    HttpContext.Session.SetString("ICN", auth.ICN);

                    return "Success";
                }
                else
                {
                    CleanSessionParams();
                    return "BadLogin";
                }
            }
            else
            {
                //return false;
                return "BadToken";
            }
        }

        public ActionResult Login(string token)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("SessionID")) && !String.IsNullOrEmpty(HttpContext.Session.GetString("ICN"))) 
            {
                return RedirectToAction("Main");
            }
            else
            {
                if (token != null)
                {
                    HttpContext.Session.SetString("Token", token);
                    CleanSessionParams();
                    return View();
                }
                else
                {
                    return BadRequest();
                }
            }

        }

        public async Task<string> PrintStickers(string[] ExtNumbers)
        {
            GetStickerResponse GetStickerResponse = await ApiMethods.GetStickers(HttpContext.Session.GetString("BasePath"),
                                                                                 HttpContext.Session.GetString("SessionID"),
                                                                                 HttpContext.Session.GetString("ICN"),
                                                                                 ExtNumbers);


            return GetStickerResponse.Stickers;
        }

        public async Task<string> PrintInvoices(string[] ExtNumbers)
        {
            GetInvoiceResponse GetInvoiceResponse = await ApiMethods.GetInvoice(HttpContext.Session.GetString("BasePath"),
                                                                                 HttpContext.Session.GetString("SessionID"),
                                                                                 HttpContext.Session.GetString("ICN"),
                                                                                 ExtNumbers);


            return GetInvoiceResponse.Invoice;
        }

        private void CleanSessionParams()
        {
            HttpContext.Session.SetString("SessionID", "");
            HttpContext.Session.SetString("ICN", "");
            HttpContext.Session.SetString("BasePath", "");
        }
    }
}