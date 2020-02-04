using AGL_Common.Enums;
using System.Collections.Generic;
using System.Linq;

namespace AGL_Common
{
    public class Response<T>
    {
        public ResponseStatus ResponseStatus => Errors.Any() ? ResponseStatus.Failure : ResponseStatus.Success;
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
    }
}
