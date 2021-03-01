using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IErrorHandler
    {
        public T SysError<T>(T resp, string errMsg);

        public void SysError(string errMsg);
    }
}
