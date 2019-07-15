using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string recipient, string subject, string emailBody);
        void ReceiveEmail(string subject, string emailBody, string emailSender = null);
    }
}
