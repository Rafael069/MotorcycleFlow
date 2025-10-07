using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Common.Results
{
    public record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NotFound = new("NotFound", "Resource not found");
        public static readonly Error Conflict = new("Conflict", "Resource already exists");
        public static readonly Error Validation = new("Validation", "Validation error");
        public static readonly Error InvalidFormat = new("InvalidFormat", "Invalid file format");

        public static Error Failure(string code, string message) => new(code, message);

       
    }
}
