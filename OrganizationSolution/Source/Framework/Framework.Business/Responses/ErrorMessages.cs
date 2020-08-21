namespace Framework.Business
{
    using System;
    using System.Collections.Generic;

    public class ErrorMessages<TErrorCode> : WrapperObject<ErrorMessage<TErrorCode>>
        where TErrorCode : struct, Enum
    {
        public ErrorMessages()
        {
        }

        public ErrorMessages(IEnumerable<ErrorMessage<TErrorCode>> models)
            : base(models)
        {
        }
    }
}
