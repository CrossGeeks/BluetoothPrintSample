using FluentValidation.Results;
using Lims.Phone.Models;
using Lims.Phone.Services.Identity;
using Lims.Phone.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lims.Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            //数据校验定义调用
            LoginViewModelValidator validationRules = new LoginViewModelValidator();

            //校验页面数据，返回校验结果
            LoginViewModel loginViewModel = (LoginViewModel)BindingContext;
            ValidationResult validationResult = validationRules.Validate(loginViewModel);
            if (validationResult.IsValid)
            {
                AccountInfo accountInfo = Identity.Authenticate(loginViewModel);
                if (accountInfo.ResulitInfo.IsOK)
                {
                    //远程校验成功，将各种参数存入内存中
                    //登录成功标志
                    if (App.Current.Properties.ContainsKey("IsLogin"))
                    {
                        App.Current.Properties["IsLogin"] = accountInfo.ResulitInfo.IsOK;
                    }
                    else
                    {
                        App.Current.Properties.Add("IsLogin",accountInfo.ResulitInfo.IsOK);
                    }
                    //账号信息
                    if (App.Current.Properties.ContainsKey("Account"))
                    {
                        App.Current.Properties["Account"] = accountInfo.Account;
                    }
                    else
                    {
                        App.Current.Properties.Add("Account", accountInfo.Account);
                    }
                    //公司名称
                    if (App.Current.Properties.ContainsKey("Company"))
                    {
                        App.Current.Properties["Company"] = accountInfo.Company;
                    }
                    else
                    {
                        App.Current.Properties.Add("Company", accountInfo.Company);
                    }
                    //Name
                    if (App.Current.Properties.ContainsKey("Name"))
                    {
                        App.Current.Properties["Name"] = accountInfo.Name;
                    }
                    else
                    {
                        App.Current.Properties.Add("Name", accountInfo.Name);
                    }

                    //传递参数保存
                    App.Current.SavePropertiesAsync();
                    //返回到主页面
                    App.Current.MainPage.Navigation.PopAsync(true);
                }
                else
                {
                    //远程Webservice数据校验失败，显示错误信息
                    DisplayAlert("提示信息",accountInfo.ResulitInfo.Message,"确定");
                }
            }
            else
            {
                DisplayError(validationResult.Errors);
                //界面校验失败，显示错误信息
                //定义临时参数
                //string message = string.Empty;
                //string controllname = string.Empty;
                //int i = 0;
                //获取校验失败数据字段列表，取各个字段
                //foreach (var error in validationResult.Errors)
                //{
                    //获取错误信息
                    //message = error.ErrorMessage;
                    //获取字段属性，视图model和界面Entry同名
                    //controllname = error.PropertyName;
                    //取列表中第一个字段返回
                    //i++;
                    //if (i > 1)
                        //break;
                //}
                //显示错误信息
                //DisplayAlert("提示信息", message, "确定");
                //将输入光标置于错误字段上
                //Entry entry = (Entry)FindByName(controllname);
                //entry.Focus();
            }
        }

        private void DisplayError(IList<ValidationFailure> errors)
        {
            throw new NotImplementedException();
        }

        protected void DisplayError(List<ValidationFailure> validationFailures)
        {
            string message = string.Empty;
            string controllname = string.Empty;
            int i = 0;

            foreach (var error in validationFailures)
            {
                //获取错误信息
                message = error.ErrorMessage;
                //获取字段属性，视图model和界面Entry同名
                controllname = error.PropertyName;
                //取列表中第一个字段返回
                i++;
                if (i > 1)
                    break;
            }
            //显示错误信息
            DisplayAlert("提示信息", message, "确定");
            //将输入光标置于错误字段上
            Entry entry = (Entry)FindByName(controllname);
            entry.Focus();
        }
    }
}