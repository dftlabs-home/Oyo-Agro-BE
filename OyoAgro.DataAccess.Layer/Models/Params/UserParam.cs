using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class UserParam
    {
        public string? Phonenumber { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? DecryptedPassword { get; set; }
        public string? Middlename { get; set; }
        public string? Gender { get; set; }
        public string? EmailAddress { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? Salt { get; set; }
        public string? Password { get; set; }
        public int? Lgaid { get; set; }


    }
}
