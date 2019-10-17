using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Ayehu.Sdk.ActivityCreation.Interfaces;
using Ayehu.Sdk.ActivityCreation.Extension;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Graph.Auth;

namespace Ayehu.Sdk.ActivityCreation
{
    public class OfficeSendMail : IActivity
    {
        /// <summary>
        /// APPLICATION (CLIENT) ID
        /// </summary>
        public string appId;

        /// <summary>
        /// Directory (tenant) ID
        /// </summary>
        public string tenantId;

        /// <summary>
        /// Client secret
        /// </summary>
        /// <remarks>
        /// A secret string that the application uses to prove its identity when requesting a token. 
        /// Also can be referred to as application password.
        /// </remarks>
        public string secret;

        /// <summary>
        /// Email subject
        /// </summary>
        public string subject;

        /// <summary>
        /// The text of the message to be sent 
        /// </summary>
        public string messageBody;

        /// <summary>
        /// The sender's email
        /// </summary>
        public string fromEmail;

        /// <summary>
        /// The recipient of the email.
        /// </summary>
        public string toEmail;

        public ICustomActivityResult Execute()
        {
            GraphServiceClient client = new GraphServiceClient("https://graph.microsoft.com/v1.0", GetProvider());
            string userId = GetUserId(client);

            Message msg = new Message();
            msg.Subject = subject;
            msg.Body = new ItemBody
            {
                ContentType = BodyType.Text,
                Content = messageBody
            };
            msg.ToRecipients = new List<Recipient>() 
            { 
                new Recipient 
                { 
                    EmailAddress = new EmailAddress { Address = toEmail } 
                } 
            };

            client.Users[userId].SendMail(msg, true).Request().WithMaxRetry(3).PostAsync().Wait();

            return this.GenerateActivityResult(GetActivityResult);
        }

        private ClientCredentialProvider GetProvider()
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(appId)
                .WithTenantId(tenantId)
                .WithClientSecret(secret)
                .Build();

            return new ClientCredentialProvider(confidentialClientApplication);
        }

        private string GetUserId(GraphServiceClient client)
        {
            var users = client.Users.Request().GetAsync().Result.ToList();

            foreach (var user in users)
            {
                if (user.Mail != null && user.Mail.ToLower() == fromEmail.ToLower())
                {
                    return user.Id;
                }
            }

            return string.Empty;
        }

        private DataTable GetActivityResult
        {
            get
            {
                DataTable dt = new DataTable("resultSet");
                dt.Columns.Add("Result");
                dt.Rows.Add("Success");

                return dt;
            }
        }
    }
}
