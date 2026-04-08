using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories;

public interface IEmailService
{
    Task SendEmail(string email, string subject, string message);
}
