using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoShop.Extensions;
using YoShop.Models;

namespace YoShop.Controllers
{
    public class SpecController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        /// <summary>
        /// 商品规格管理
        /// </summary>
        /// <param name="fsql"></param>
        public SpecController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 商品规格管理

        [HttpPost, Route("/goods.spec/addSpec")]
        public async Task<IActionResult> AddSpec(string specName, string specValue)
        {
            // 判断规格组是否存在
            long specId = await _fsql.Select<Spec>().Where(l => l.SpecName == specName).ToOneAsync(l => l.SpecId);
            if (specId == 0)
            {
                var specModel = new Spec
                {
                    SpecName = specName,
                    WxappId = GetSellerSession().WxappId,
                    CreateTime = DateTime.Now.ConvertToTimeStamp(),
                    UpdateTime = DateTime.Now.ConvertToTimeStamp()
                };
                specId = await _fsql.Insert<Spec>().AppendData(specModel).ExecuteIdentityAsync();
            }
            // 判断规格值是否存在
            long specValueId = await _fsql.Select<SpecValue>().Where(l => l.SpecId == specId && l.SpecValueName == specValue).ToOneAsync(l => l.SpecValueId);
            if (specValueId == 0)
            {
                var specValueModel = new SpecValue
                {
                    SpecId = (uint)specId,
                    SpecValueName = specValue,
                    WxappId = GetSellerSession().WxappId,
                    CreateTime = DateTime.Now.ConvertToTimeStamp(),
                    UpdateTime = DateTime.Now.ConvertToTimeStamp()
                };
                specValueId = await _fsql.Insert<SpecValue>().AppendData(specValueModel).ExecuteIdentityAsync();
            }

            return YesResult(new { specId, specValueId });
        }

        [HttpPost, Route("/goods.spec/addSpecValue")]
        public async Task<IActionResult> AddSpec(uint specId, string specValue)
        {
            long specValueId = await _fsql.Select<SpecValue>().Where(l => l.SpecId == specId && l.SpecValueName == specValue).ToOneAsync(l => l.SpecValueId);
            if (specValueId == 0)
            {
                var specValueModel = new SpecValue
                {
                    SpecId = (uint)specId,
                    SpecValueName = specValue,
                    WxappId = GetSellerSession().WxappId,
                    CreateTime = DateTime.Now.ConvertToTimeStamp(),
                    UpdateTime = DateTime.Now.ConvertToTimeStamp()
                };
                specValueId = await _fsql.Insert<SpecValue>().AppendData(specValueModel).ExecuteIdentityAsync();
            }

            return YesResult(new { specValueId });
        }

        #endregion
    }
}