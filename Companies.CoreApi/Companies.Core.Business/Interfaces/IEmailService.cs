using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Core.Business.Interfaces
{
    public interface IEmailService
    {
        Task SendCompanyCreatedEmail(string companyName);

        Task SendCompanyUpdatedEmail(string companyName);

    }
}
