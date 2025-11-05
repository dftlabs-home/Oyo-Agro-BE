using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Request
{
    public class MailRequest
    {
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }

    public class MailParameter
    {
        public string? RealName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UserCompany { get; set; }
        public string? ProcessName { get; set; }
        public string? MessageType { get; set; }
        public string? TicketNumber { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonEmail { get; set; }      
        public string? UserToken { get; set; }
        public string? Remark { get; set; }
        public string? RequestNumber { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public string? EticketApprover { get; set; }
        public string? ApproverEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? DateSubmitted { get; set; }        
        public string? EmployeeName { get; set; }
        public string? ResetLink { get; set; }
    }
}
