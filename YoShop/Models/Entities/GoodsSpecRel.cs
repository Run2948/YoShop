using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "goods_spec_rel")]
    public partial class GoodsSpecRel : WxappEntity
    {
        [JsonProperty, Column(Name = "id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint Id { get; set; }

        [JsonProperty, Column(Name = "goods_id", DbType = "int(11) unsigned")]
        public uint GoodsId { get; set; }

        [JsonProperty, Column(Name = "spec_id", DbType = "int(11) unsigned")]
        public uint SpecId { get; set; }

        [Navigate(nameof(SpecId))]
        public virtual Spec Spec { get; set; }

        [JsonProperty, Column(Name = "spec_value_id", DbType = "int(11) unsigned")]
        public uint SpecValueId { get; set; }

        [Navigate(nameof(SpecValueId))]
        public virtual SpecValue SpecValue { get; set; }
    }
}
