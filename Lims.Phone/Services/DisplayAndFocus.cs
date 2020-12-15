using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lims.Phone.Services
{
    public static class DisplayAndFocus
    {
        public static async System.Threading.Tasks.Task DisplayErrorAndFocusAsync(IList<ValidationFailure> errors)
        {
            string message = string.Empty;
            string controllername = string.Empty;
            int i = 0;

            controllername = errors[1].PropertyName;
            message = errors[1].ErrorMessage;

            await App.Current.MainPage.DisplayAlert("错误提示",message,"确定");
        }
    }
}
