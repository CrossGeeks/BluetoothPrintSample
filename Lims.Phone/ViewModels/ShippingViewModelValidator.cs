using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lims.Phone.ViewModels
{
    public class ShippingViewModelValidator : AbstractValidator<ShippingViewModel>
    {
        public ShippingViewModelValidator()
        {
            //收货人电话不能为空或不符合格式
            RuleFor(item => item.ConsigneePhone)
                .NotNull().WithMessage("收货人电话不能为空，请检查！！！")
                .NotEmpty().WithMessage("收货人电话不能为空，请检查！！！")
                .Matches("^1[345789]\\d{9}$|^0[0-9]{2,3}-[0-9]{7,8}").WithMessage("电话号码格式错误，请检查！！！");

            //收货人姓名不能为空
            RuleFor(item => item.ConsigneeName)
                .NotNull().WithMessage("收货人姓名不能为空，请检查！！！")
                .NotEmpty().WithMessage("收货人姓名不能为空，请检查！！！");

            //收货人地址为空检查
            RuleFor(item => item.ConsigneeAddress)
                .NotNull().WithMessage("收货人地址不能为空，请检查！！！")
                .NotEmpty().WithMessage("收货人地址不能为空，请检查！！！");

            //发货人电话为空和格式检查
            RuleFor(item => item.ShipperPhone)
                .NotNull().WithMessage("发货人电话不能为空，请检查！！！")
                .NotEmpty().WithMessage("发货人电话不能为空，请检查！！！")
                .Matches("^1[345789]\\d{9}$|^0[0-9]{2,3}-[0-9]{7,8}").WithMessage("发货人电话号码格式不符，请检查！！！");

            //发货人姓名为空检查
            RuleFor(item => item.ShipperName)
                .NotNull().WithMessage("发货人姓名不能为空，请检查！！！")
                .NotEmpty().WithMessage("发货人姓名不能为空，请检查！！！");

            //提货网点不能为空，请检查！！！
            RuleFor(item => item.PickupLocations)
                .NotNull().WithMessage("提货网点不能为空，请检查！！！")
                .NotEmpty().WithMessage("提货网点不能为空，请检查！！！");

            //目的地为空检查
            RuleFor(item => item.Destination)
                .NotNull().WithMessage("目的地不能为空，请检查！！！")
                .NotEmpty().WithMessage("目的地不能为空，请检查！！！");

            //货物名称为空检查
            RuleFor(item => item.CargoName)
                .NotNull().WithMessage("货物名称不能为空，请检查！！！")
                .NotEmpty().WithMessage("货物名称不能为空，请检查！！！");

            //货物件数必须大于零
            RuleFor(item => item.NumberOfPieces)
                .NotNull().WithMessage("货物件数不能为零，请检查！！！")
                .NotEmpty().WithMessage("货物件数不能为零，请检查！！！");

            //运费字段检查
            RuleFor(item => item.FreightRates)
                .NotNull().WithMessage("运费不能为零，请检查！！！")
                .NotEmpty().WithMessage("运费不能为零，请检查！！！");

            //付款方式检查
            RuleFor(item => item.PaymentMethods)
                .NotNull().WithMessage("付款方式不能为空，请检查")
                .NotEmpty().WithMessage("付款方式不能为空，请检查");

            RuleFor(item => item.TransitMethod)
                .NotNull().WithMessage("中转方式不能为空，请检查！！！")
                .NotEmpty().WithMessage("中转方式不能为空，请检查！！！");
        }
    }
}
