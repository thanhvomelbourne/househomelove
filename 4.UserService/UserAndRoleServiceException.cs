using System;
using System.Runtime.Serialization;
using CoreLibrary.Service;

namespace UserService
{
    class UserAndRoleServiceException : ServiceException
    {
        public UserAndRoleServiceException() : base() { }
        public UserAndRoleServiceException(string message) : base(message) { }
        public UserAndRoleServiceException(string message, Exception innerException) : base(message, innerException) { }
        protected UserAndRoleServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
