using System;
using System.Globalization;

namespace UFynd.Application.Exceptions
{
    public class UserFriendlyException : ApplicationException
    {
        public UserFriendlyException() : base() { }

        public UserFriendlyException(string message) : base(message) { }

        public UserFriendlyException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
