using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YoShop.WeChat
{
    public class BaseRequest
    {
        public uint WxappId { get; set; }
        public string Token { get; set; }
    }

    public class LoginRequest : BaseRequest
    {
        public string Code { get; set; }

        [JsonProperty("user_info")]
        public string UserInfo { get; set; }

        [JsonProperty("encrypted_data")]
        public string EncryptedData { get; set; }

        public string Iv { get; set; }

        public string Signature { get; set; }
    }

    public class RequestUser
    {
        public string NickName { get; set; }
        public byte Gender { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string AvatarUrl { get; set; }
    }
}
