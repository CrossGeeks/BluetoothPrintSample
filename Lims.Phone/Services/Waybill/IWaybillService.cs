using Lims.Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lims.Phone.Services.Waybill
{
    public interface IWaybillService
    {
        string GetWaybillNumber(string company, string name);

        string SaveWaybill(ShippingViewModel shippingViewModel);
    }
}
