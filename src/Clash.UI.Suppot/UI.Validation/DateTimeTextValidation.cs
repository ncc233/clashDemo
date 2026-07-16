using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Clash.UI.Suppot.UI.Validation
{
    public class DateTimeTextValidation : ValidationRule
    {

        public string Type { get; set; }
        /// <summary>
        /// 最小年份（可选验证），默认 1（DateTime 支持的最小年份）。
        /// </summary>
        public int MinYear { get; set; } = 1;

        /// <summary>
        /// 最大年份（可选验证），默认 9999（DateTime 支持的最大年份）。
        /// </summary>
        public int MaxYear { get; set; } = 9999;

        /// <summary>
        /// 是否启用年份范围验证，默认 false。
        /// </summary>
        public bool ValidateYearRange { get; set; } = false;

        public ValidationParams ValidationParams { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // 1. 检查输入是否为空
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(false, "输入不能为空。");

            string dateString = value.ToString().Trim();

            // 2. 尝试按指定格式解析
            if (!DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed))
            {
                return new ValidationResult(false, "时间格式必须为 'yyyy-MM-dd HH:mm:ss'。");
            }

            // 3. 可选：验证年份范围
            if (ValidateYearRange)
            {
                if (parsed.Year < MinYear || parsed.Year > MaxYear)
                    return new ValidationResult(false, $"年份必须在 {MinYear} 到 {MaxYear} 之间。");
            }

            // 4. 验证通过，将解析结果放入 ErrorContent 返回
            return new ValidationResult(true, parsed);
        }
    }
}

