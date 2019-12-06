
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "wxapp")]
    public partial class Wxapp : BaseEntity
    {
        [JsonProperty, Column(Name = "wxapp_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint WxappId { get; set; }

        [JsonProperty, Column(Name = "apikey")]
        public string ApiKey { get; set; }

        [JsonProperty, Column(Name = "app_id", DbType = "varchar(50)")]
        public string AppId { get; set; }

        [JsonProperty, Column(Name = "app_name", DbType = "varchar(50)")]
        public string AppName { get; set; }

        [JsonProperty, Column(Name = "app_secret", DbType = "varchar(50)")]
        public string AppSecret { get; set; }

        [JsonProperty, Column(Name = "is_phone")]
        public byte IsPhone { get; set; }

        [JsonProperty, Column(Name = "is_service")]
        public byte IsService { get; set; }

        [JsonProperty, Column(Name = "mchid", DbType = "varchar(50)")]
        public string MchId { get; set; }

        [JsonProperty, Column(Name = "phone_image_id", DbType = "int(11) unsigned")]
        public uint PhoneImageId { get; set; }

        [JsonProperty, Column(Name = "phone_no", DbType = "varchar(20)")]
        public string PhoneNo { get; set; }

        [JsonProperty, Column(Name = "service_image_id", DbType = "int(11) unsigned")]
        public uint ServiceImageId { get; set; }

        [Navigate(nameof(ServiceImageId))]
        public virtual UploadFile UploadFile { get; set; }
    }
}
