﻿using System;

namespace ExceptionHandlingBlock.Biz
{
    [Serializable]
    public class InvalidDataException : Exception
    {
        public InvalidDataException()
        {
        }

        public InvalidDataException(string message)
            : base(message)
        {
        }

        public InvalidDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
