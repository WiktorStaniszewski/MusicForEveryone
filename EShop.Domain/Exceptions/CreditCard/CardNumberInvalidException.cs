﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.Exceptions.CreditCard;

public class CardNumberInvalidException : Exception
{
    public CardNumberInvalidException() { }

    public CardNumberInvalidException(string message) : base(message) { }

    public CardNumberInvalidException(string message, Exception innerException) : base(message, innerException) { }

}
