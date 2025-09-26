using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Dtos
{
    public class TData
    {
        // Operation result, Tag is 1 for success, 0 for failure, other verification returns results, which can be set as needed
        public int Tag { get; set; }

        // Prompt information or exception information
        public string? Message { get; set; }

        // Extend Message
        public string? Description { get; set; }
        public object? Data { get; set; }

    }

    // Data transfer object (DTO)
    public class TData<T> : TData
    {
        // The number of records in the list
        public int Total { get; set; }

        // data
        public T? Data { get; set; }
    }
}
