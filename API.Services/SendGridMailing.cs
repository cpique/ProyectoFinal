using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.Services
{
    public class SendGridMailing
    {
        public void Execute(string template)
        {
            HelloEmail(template).Wait();
        }

        private static async Task HelloEmail(string template)
        {
            String apiKey = Environment.GetEnvironmentVariable("ENVIRONMENT_VARIABLE_SENDGRID_KEY");
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            Email from = new Email("cristianpique33@gmail.com"); 
            String subject = "Bienvenido a AmosGym";
            Email to = new Email("cristian.pique@hotmail.com");
            Content content;
            if (string.IsNullOrEmpty(template))
            {
                content = new Content("text/plain", "Te damos la bienvenida a AMOS Gym. Visita nuestro sitio web http://amosgym.azurewebsites.net/ para enterarte de las últimas novedades y personalizar tu información");
            }
            else
            {
                var emailTemplate = File.ReadAllText(template);
                emailTemplate = emailTemplate.Replace("USER_REPLACE_TEXT", "cristian.pique@hotmail.com")
                                             .Replace("PASS_REPLACE_TEXT", "335588");
                content = new Content("text/html", emailTemplate);
            }
            Mail mail = new Mail(from, subject, to, content);
            
            //Email email = new Email("test2@example.com");
            //mail.Personalization[0].AddTo(email);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
            var statusCode = (response.StatusCode);
            var result = (response.Body.ReadAsStringAsync().Result);
            var headers = (response.Headers.ToString());
            var get = mail.Get();

        }
    }
}
