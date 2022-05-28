using GoogleRecaptchaV3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace GoogleRecaptchaV3.Controllers
{
    public class LoginController : Controller
    {
        private readonly GoogleRecaptchaV3Model _recaptchV3Config;

        public LoginController(IOptions<GoogleRecaptchaV3Model> recaptchV3Config)
        {
            _recaptchV3Config = recaptchV3Config.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginMe(LoginModel model)
        {
            bool bCaptchValid = IsRecaptchV3Valid(model.CaptchaToken);

            if (bCaptchValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        bool IsRecaptchV3Valid(string captchaResponseToken)
        {
            var reCaptchVerifyUri = $"{_recaptchV3Config.VerifyURL}?secret={_recaptchV3Config.SecretKey}&response={captchaResponseToken}";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = httpClient.GetAsync(reCaptchVerifyUri).Result;

            string responseInJSON = responseMessage.Content.ReadAsStringAsync().Result;

            GoogleRecaptchaV3Response grcV3Response = JsonConvert.DeserializeObject<GoogleRecaptchaV3Response>(responseInJSON);

            return grcV3Response!.success;
        }

    }
}
