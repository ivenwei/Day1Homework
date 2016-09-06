using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork1Day1.Filters
{
    public class DateRangeAttribute : ValidationAttribute, IClientValidatable
    {
        private int minDay;
        private int maxDay;
        public DateRangeAttribute(int minDay,int maxDay)
        {
            this.minDay = minDay;
            this.maxDay = maxDay;

            if (minDay > maxDay)
            {
                throw new Exception("請輸入正確驗證範圍!!");
            }
        }

        public override bool IsValid(object value)
        {
            if (value == null){
                return true;
            }

            DateTime? compareDate = value as DateTime?;
            if (compareDate.HasValue)
            {
                DateTime startDate = DateTime.Now.AddDays(minDay);
                DateTime endDate = DateTime.Now.AddDays(maxDay);

                if (startDate.Date > compareDate.Value || compareDate.Value > endDate.Date)
                {
                    return false;
                }
            }
            return true;
        }

       



        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                //ValidationType 的值一定要是小寫！
                ValidationType = "daterange",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };

            //ValidationParameters 一定要是小寫！
            //Js        C#
            rule.ValidationParameters["min"] = minDay;
            rule.ValidationParameters["max"] = maxDay;

            //可能不只一個前端驗證 所以使用yield return
            yield return rule;
        }
    }
}