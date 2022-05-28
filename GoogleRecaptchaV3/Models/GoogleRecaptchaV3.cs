namespace GoogleRecaptchaV3.Models
{
    public class GoogleRecaptchaV3Model
    {
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
        public string VerifyURL { get; set; }
    }

    public class GoogleRecaptchaV3Response
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
        public float score { get; set; }
        public string action { get; set; }
    }

}
