﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(string email);
    }
}
