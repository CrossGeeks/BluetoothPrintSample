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
                .NotNull()
                .NotEmpty()
                .WithMessage("收货人电话不能为空，请检查！！！")
                .Matches("^(\\d{3,4}-)?\\d{7,8})$|(13[0-9]{9}")
                .WithMessage("电话号码格式错误，请检查！！！");

            //收货人姓名不能为空
            RuleFor(item => item.ConsigneeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("收货人姓名不能为空，请检查！！！");

            //收货人地址为空检查
            RuleFor(item => item.ConsigneeAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("收货人地址不能为空，请检查！！！");

            //发货人电话为空和格式检查
            RuleFor(item => item.ShipperPhone)
                .NotNull()
                .NotEmpty()
                .WithMessage("发货人电话不能为空，请检查！！！")
                .Matches("^(\\d{3,4}-)?\\d{7,8})$|(13[0-9]{9}")
                .WithMessage("发货人电话号码格式不符，请检查！！！");
        }
    }
}
