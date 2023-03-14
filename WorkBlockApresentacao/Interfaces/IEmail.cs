using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkBlockApresentacao.Interfaces
{
    public interface IEmail
    {
        void SendEmail(List<string> emailsTo, string subject, string body, List<string> attachments);
    }
}