using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TUNIWEB.Models
{
    public class GooglereCaptchaService
    {
        private ReCAPTCHASSettings _settings;

        public GooglereCaptchaService(IOptions<ReCAPTCHASSettings> settings)
        {
            _settings = settings.Value;
        }
        public virtual async Task<GoogleRespo> recVer(string _tooken)
        {
            GooglereCaptchaData _myData = new GooglereCaptchaData()
            {
                response = _tooken,
                secret = _settings.ReCaPTCHA_Secret_Key
            };
            HttpClient _client = new HttpClient();
            var responese = await _client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_myData.secret}&response={_myData.response}");
            var capresp = JsonConvert.DeserializeObject<GoogleRespo>(responese);
            return capresp;
        }
    }
    public class GooglereCaptchaData
    {
        public string response { get; set; }//Token
        public string secret { get; set; }//Secret Key
    }
    public class GoogleRespo
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime Callenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
