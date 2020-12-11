using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

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
        /// 
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
