using Lims.Phone.Models;
using Lims.Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lims.Phone.Services.Identity
{
    public static class Identity
    {
        private static SkpServiceReference.ServiceSoapClient serviceSoapClient = new SkpServiceReference.ServiceSoapClient(SkpServiceReference.ServiceSoapClient.EndpointConfiguration.ServiceSoap);

        public static AccountInfo Authenticate(LoginViewModel loginViewModel)
        {
            AccountInfo accountInfo = new AccountInfo(
                serviceSoapClient.loadp(loginViewModel.UserName, loginViewModel.Password, loginViewModel.CompanyCode, null).ToString().Trim()
                );

            return accountInfo;
        }
    }
}
