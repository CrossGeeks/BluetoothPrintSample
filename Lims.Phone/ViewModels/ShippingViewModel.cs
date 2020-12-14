using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Lims.Phone.Services;

namespace Lims.Phone.ViewModels
{
    public class ShippingViewModel : INotifyPropertyChanged
    {
        private string _consigneephone;
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ConsigneePhone 
        {
            get { return _consigneephone; }
            set 
            {
                _consigneephone = value;
                OnPropertyChanged();
            }
        }

        private string _consigneename;
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ConsigneeName
        {
            get { return _consigneename; }
            set 
            {
                _consigneename = value;
                OnPropertyChanged();
            }
        }

        private string _consigneeaddress;
        /// <summary>
        /// 收货人地址
        /// </summary>
        public string ConsigneeAddress
        {
            get { return _consigneeaddress; }
            set
            {
                _consigneeaddress = value;
                OnPropertyChanged();
            }
        }

        private string _shipperphone;
        /// <summary>
        /// 发货人电话
        /// </summary>
        public string ShipperPhone
        {
            get { return _shipperphone; }
            set
            {
                _shipperphone = value;
                OnPropertyChanged();
            }
        }

        private string _shippername;
        /// <summary>
        /// 发货人姓名
        /// </summary>
        public string ShipperName
        {
            get { return _shippername; }
            set 
            {
                _shippername = value;
                OnPropertyChanged();
             }
        }

        private string _pickuplocations;
        /// <summary>
        /// 提货网点
        /// </summary>
        public string PickupLocations
        {
            get { return _pickuplocations; }
            set
            {
                _pickuplocations = value;
                OnPropertyChanged();
            }
        }

        private string _destination;
        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination
        {
            get { return _destination; }
            set
            {
                _destination = value;
                OnPropertyChanged();
            }
        }

        private string _cargoname;
        /// <summary>
        /// 货物名称
        /// </summary>
        public string CargoName
        {
            get { return _cargoname; }
            set
            {
                _cargoname = value;
                OnPropertyChanged();
            }
        }

        private string _numberofpieces;
        /// <summary>
        /// 货物件数
        /// </summary>
        public string NumberOfPieces
        {
            get { return _numberofpieces; }
            set
            {
                _numberofpieces = value;
                OnPropertyChanged();
            }
        }

        private string _freightrates;
        /// <summary>
        /// 运费
        /// </summary>
        public string FreightRates
        {
            get { return _freightrates; }
            set
            {
                _freightrates = value;
                OnPropertyChanged();
            }
        }

        private string _guaranteedamount;
        /// <summary>
        /// 保价金额
        /// </summary>
        public string GuaranteedAmount
        {
            get { return _guaranteedamount; }
            set
            {
                _guaranteedamount = value;
                OnPropertyChanged();
            }
        }

        private string _collectionamount;
        /// <summary>
        /// 代收金额
        /// </summary>
        public string CollectionAmount
        {
            get { return _collectionamount; }
            set
            {
                _collectionamount = value;
                OnPropertyChanged();
            }
        }

        private string _cashback;
        /// <summary>
        /// 现返金额
        /// </summary>
        public string Cashback
        {
            get { return _cashback; }
            set
            {
                _cashback = value;
                OnPropertyChanged();
            }
        }

        private string _owedback;
        /// <summary>
        /// 欠返金额
        /// </summary>
        public string OwedBack
        {
            get { return _owedback; }
            set 
            {
                _owedback = value;
                OnPropertyChanged();
            }
        }

        private string _deliveryfees;
        public string DeliveryFees
        {
            get { return _deliveryfees; }
            set
            {
                _deliveryfees = value;
                OnPropertyChanged();
            }
        }

        private string _paymentmethods;
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentMethods
        {
            get { return _paymentmethods; }
            set
            {
                _paymentmethods = value;
                OnPropertyChanged();
            }
        }

        private string _transitmethod;
        /// <summary>
        /// 中转方式
        /// </summary>
        public string TransitMethod
        {
            get { return _transitmethod; }
            set 
            {
                _transitmethod = value;
                OnPropertyChanged();
            }
        }

        private string _waybillnumber;
        /// <summary>
        /// 单据号码
        /// </summary>
        public string WaybillNumber 
        {
            get { return _waybillnumber; }
            set 
            {
                _waybillnumber = value;
                OnPropertyChanged();
            }
        }

        private bool _issave;
        /// <summary>
        /// 单据保存状态标志位。true保存完毕，flase尚未保存
        /// </summary>
        public bool IsSave 
        {
            get { return _issave; }
            set 
            {
                _issave = value;
                OnPropertyChanged();
            }
        }

        private bool _isprintwaybill;
        /// <summary>
        /// 单据打印标志位，true打印完毕,flase尚未打印
        /// </summary>
        public bool IsPrintWaybill 
        {
            get { return _isprintwaybill; }
            set 
            {
                _isprintwaybill = value;
                OnPropertyChanged();
            }
        }

        private bool _isprintwaybilllabels;
        /// <summary>
        /// 运单标签是否打印标志位，true打印完毕，flase尚未打印
        /// </summary>
        public bool IsPrintWaybillLabels 
        {
            get { return _isprintwaybilllabels; }
            set 
            {
                _isprintwaybilllabels = value;
                OnPropertyChanged();
            }
        }

        private bool _islogin;
        /// <summary>
        /// 是否登录标记
        /// </summary>
        public bool IsLogin
        {
            get { return _islogin; }
            set
            {
                _islogin = value;
                OnPropertyChanged();
            }
        }

        private string _account;
        /// <summary>
        /// 账号，从login页面传递来的
        /// </summary>
        public string Account 
        {
            get { return _account; }
            set 
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        private string _company;
        /// <summary>
        /// 公司名称，从login页面传递来的
        /// </summary>
        public string Company 
        {
            get { return _company; }
            set 
            {
                _company = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        /// <summary>
        /// 简称
        /// </summary>
        public string Name 
        {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _date;
        /// <summary>
        /// 系统时间，从login页面传递过来的
        /// </summary>
        public string Date 
        {
            get { return _date; }
            set 
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private string _printname;
        /// <summary>
        /// 默认打印机名称
        /// </summary>
        public string PrintName
        {
            get { return _printname; }
            set 
            {
                _printname = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ShippingViewModel()
        {
            if (App.Current.Properties.ContainsKey("IsLogin"))
                IsLogin = Convert.ToBoolean(Properties.Get("IsLogin"));

            if (App.Current.Properties.ContainsKey("Account"))
                Account = Properties.Get("Account");

            if (App.Current.Properties.ContainsKey("Company"))
                Company = Properties.Get("Company");

            if (App.Current.Properties.ContainsKey("Name"))
                Name = Properties.Get("Name");

            if (App.Current.Properties.ContainsKey("Date"))
                Date = Properties.Get("Date");

            if (App.Current.Properties.ContainsKey("defaultPrinter"))
                PrintName = Properties.Get("defaultPrinter");
        }
    }
}
