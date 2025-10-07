using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MotorcycleFlow.Application.Common.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public Error Error { get; }

        private Result(bool isSuccess, T value, Error error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new(true, value, Error.None);
        public static Result<T> Failure(Error error) => new(false, default, error);
    }
}
