using System;
using System.Collections.Generic;
using System.Text;

namespace Lims.Phone.Services
{
    public static class Properties
    {
        /// <summary>
        /// 获取参数内容
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>参数值，字符串，需自己进行转换</returns>
        public static string Get(string name)
        {
            //默认返回值
            string result = string.Empty;

            //将名称统一大写，防止错误
            name = name.ToUpper().Trim();
            //如果相应的指存在，取值返回
            if (App.Current.Properties.ContainsKey(name))
                result = App.Current.Properties[name].ToString().Trim();
            //返回结果值
            return result;
        }

        /// <summary>
        /// 参数值设置，有则保存，无则添加
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        public static void Set(string name,object value)
        {
            //名称大写
            name = name.ToUpper().Trim();
            //有则保存，无则增加
            if (App.Current.Properties.ContainsKey(name))
                App.Current.Properties[name] = value.ToString().Trim();
            else
                App.Current.Properties.Add(name, value);
            //保存
            App.Current.SavePropertiesAsync();
        }
    }
}
