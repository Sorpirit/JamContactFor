using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Cors;

namespace ContactFormBackend
{
    //[Produces("application/json")]
    [EnableCors(Startup.MyAllowSpecificOrigins)]
    [ApiController]
    [Route("send")]
    public class ContactForm : ControllerBase
    {
        private readonly LoggedInUsers _users;
        private readonly EmailServerConfiguration _serverConfiguration;

        public ContactForm(EmailServerConfiguration serverConfiguration, LoggedInUsers users)
        {
            _users = users;
            _serverConfiguration = serverConfiguration;
        }

        [HttpPost]
        public IActionResult SendContactInformation([FromBody] User user)
        {
            if (!IsUserValid(user))
            {
                return BadRequest("Invalid user");
            }
            
            MailAddress to = new MailAddress(user.Email);
            MailAddress sender = new(_serverConfiguration.SmtpUsername);
            NetworkCredential credentials = new(_serverConfiguration.SmtpUsername,_serverConfiguration.SmtpPassword);

            using MailMessage message = new MailMessage(sender, to);
            message.Subject = $"Good morning, {user.Name}";
            message.Body = user.Message;
            using SmtpClient client = new SmtpClient(_serverConfiguration.SmtpServer, _serverConfiguration.SmtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = credentials,
                EnableSsl = true,
            };
            
            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                return BadRequest(ex.StatusCode + " " + ex.Message);
            }

            _users.RegisteredUsers.Add(user);
            
            return Ok();
        }

        private bool IsUserValid(User user)
        {
            return !string.IsNullOrWhiteSpace(user.Email) 
                   && new EmailAddressAttribute().IsValid(user.Email) 
                   //&& !_users.RegisteredUsers.Contains(user) 
                   && !string.IsNullOrWhiteSpace(user.Name) 
                   && !string.IsNullOrWhiteSpace(user.Name);
        }
    }
}