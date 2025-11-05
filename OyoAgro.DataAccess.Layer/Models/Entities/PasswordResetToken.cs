using System;
using System.ComponentModel.DataAnnotations;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public class PasswordResetToken : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public string Token { get; set; } = null!;
        
        public DateTime ExpiresAt { get; set; }
        
        public bool IsUsed { get; set; } = false;
        
        public DateTime? UsedAt { get; set; }
        
        public string? IpAddress { get; set; }
        
        public string? UserAgent { get; set; }
        
        // Navigation property
        public virtual Useraccount User { get; set; } = null!;
    }
}
