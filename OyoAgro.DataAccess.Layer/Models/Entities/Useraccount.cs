using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Useraccount : BaseEntity
    {

        public Useraccount()
        {
            Notifications = new HashSet<Notification>();
            Notificationtargets = new HashSet<Notificationtarget>();
            Profileadditionalactivities = new HashSet<Profileadditionalactivity>();
            Userprofiles = new HashSet<Userprofile>();
            Userregions = new HashSet<Userregion>();
            PasswordResetTokens = new HashSet<PasswordResetToken>();
        }

        public int Userid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Username { get; set; } = null!;
        public string Passwordhash { get; set; } = null!;
        public string? Password { get; set; }       
        public long? Version { get; set; }
        public string? Salt { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
        public string? Apitoken { get; set; }
        public int? Logincount { get; set; }
        public DateOnly? Lastlogindate { get; set; }
        public DateOnly? Deactivateddate { get; set; }
        public int? Failedloginattempt { get; set; }
        public string? Securityquestion { get; set; }
        public string? Securityanswer { get; set; }
        public bool? Isactive { get; set; }
        public bool? Islocked { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }
        public DateTime? LastPasswordReset { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Notificationtarget> Notificationtargets { get; set; }
        public virtual ICollection<Profileadditionalactivity> Profileadditionalactivities { get; set; }
        public virtual ICollection<Userprofile> Userprofiles { get; set; }
        public virtual ICollection<Userregion> Userregions { get; set; }
        public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; }

        [NotMapped]
        public string Phonenumber { get; set; } 
        [NotMapped]
        public string Firstname { get; set; } 
        [NotMapped]
        public string Lastname { get; set; } 
        [NotMapped]
        public string DecryptedPassword { get; set; }
        [NotMapped]
        public string Middlename { get; set; }

        [NotMapped]
        public string Gender { get; set; }
        [NotMapped]
        public int Residentialaddressid { get; set; }
        [NotMapped]
        public string Address { get; set; }

        [NotMapped]
        public int? Lgaid { get; set; }

        [NotMapped]
        public int? Roleid { get; set; }
         [NotMapped]
        public int? RegionId { get; set; }

    }
}
