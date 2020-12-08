using FluentValidation;

namespace Lims.Phone.ViewModels
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            //用户名字段校验，现只检测是否为空
            RuleFor(item => item.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("用户名称不能为空，请输入用户名称！！！");

            //用户密码字段校验，现只检测是否为空
            RuleFor(item => item.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("密码不能为空，请输入用户密码！！！");

            //公司代码字段校验，目前检查为空及代码长度为三位
            RuleFor(item => item.CompanyCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("公司代码不能为空，请输入公司代码！！！")
                .Length(3)
                .WithMessage("公司代码长度为3位，请检查");
        }
    }
}
