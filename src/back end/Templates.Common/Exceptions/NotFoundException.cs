using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public override string Message { get; }

        public NotFoundException()
        {
        }

        public NotFoundException(string message)
        {
            Message = message;
        }
    }
}
