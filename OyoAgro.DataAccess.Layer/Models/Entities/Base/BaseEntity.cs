using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure;

namespace OyoAgro.DataAccess.Layer.Models.Entities.Base
{
    public abstract class BaseEntity
    {
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }

        public async Task Create(string? userId = null)
        {
            Createdat = DateTimeOffset.UtcNow.UtcDateTime;
            await Task.CompletedTask;
        }

        public async Task Modify(string? userId = null)
        {
            Updatedat = DateTimeOffset.UtcNow.UtcDateTime;
            await Task.CompletedTask;
        }
    }

}
