namespace Lims.Phone.Models
{
    public class ResulitInfo
    {
        /// <summary>
        /// 执行正确标志
        /// </summary>
        public bool IsOK { get; set; }

        /// <summary>
        /// 执行结果信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ResulitInfo()
        {
        }

        /// <summary>
        /// 由返回信息创建构造函数
        /// </summary>
        /// <param name="resultinfostr">返回结果字符串</param>
        public ResulitInfo(string resultinfostr)
        {
            //结果字符串按照'#'分割为中间结果
            string[] resultmid = resultinfostr.Split('#');
            //根据字符组0检测结果
            string[] okstr = resultmid[0].ToString().Split('=');

            if (okstr[1] == "1")
            {
                //结果正确
                this.IsOK = true;
                this.Message = resultmid[1].ToString();
            }
            else
            {
                //结果错误
                this.IsOK = false;
                //填充错误消息
                string[] MessageMid = resultmid[1].Split('=');
                this.Message = MessageMid[1].ToString().Trim();
            }
        }
    }
    public class AccountInfo
    {
        /// <summary>
        /// 账号信息结果对象类
        /// </summary>
        public ResulitInfo ResulitInfo { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 日期信息
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AccountInfo()
        {
        }

        /// <summary>
        /// 用结果字符串构造函数
        /// </summary>
        /// <param name="resultstr">检索结果字符串</param>
        public AccountInfo(string resultstr)
        {
            //错误结果返回字符串格式 state=0#err=公司代码错误.
            //正确结果返回字符串格式 state=1#账户=shj&姓名=胜京&comp=胜京物流&dat=2020-11-18
            this.ResulitInfo = new ResulitInfo(resultstr);
            if (this.ResulitInfo.IsOK)
            {
                //填充账号信息
                string[] midstr = this.ResulitInfo.Message.Split('&');
                this.Account = midstr[0].ToString().Split('=')[1].ToString().Trim();
                this.Name = midstr[1].ToString().Split('=')[1].ToString().Trim();
                this.Company = midstr[2].ToString().Split('=')[1].ToString().Trim();
                this.Date = midstr[3].ToString().Split('=')[1].ToString().Trim();
                //账号正确的话，将message信息置空
                if (this.ResulitInfo.IsOK)
                    this.ResulitInfo.Message = "";
            }
        }
    }
}
