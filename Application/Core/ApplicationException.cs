﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class ApplicationException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public ApplicationException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
