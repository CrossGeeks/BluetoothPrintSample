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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
