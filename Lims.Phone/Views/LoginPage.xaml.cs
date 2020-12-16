﻿using FluentValidation.Results;
using Lims.Phone.Models;
using Lims.Phone.Services;
using Lims.Phone.Services.Identity;
using Lims.Phone.ViewModels;
using System;
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
                WebServiceValidator(loginViewModel);
                //返回到主页面
                Application.Current.MainPage.Navigation.PopAsync(true);
            }
            else
                DisplayError(validationResult.Errors);
        }

        private void WebServiceValidator(LoginViewModel loginViewModel)
        {
            AccountInfo accountInfo = Identity.Authenticate(loginViewModel);
            if (accountInfo.ResulitInfo.IsOK)
            {
                //远程校验成功，将各种参数存入内存中
                SaveToPropertitys.SaveTo(accountInfo.ResulitInfo.IsOK, accountInfo.Account, accountInfo.Company, accountInfo.Name,accountInfo.Date);
            }
            else
                //远程Webservice数据校验失败，显示错误信息
                DisplayAlert("提示信息", accountInfo.ResulitInfo.Message, "确定");
        }

        private void DisplayError(IList<ValidationFailure> errors)
        {
            string message = string.Empty;
            string controllname = string.Empty;
            int i = 0;

            foreach (var error in errors)
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