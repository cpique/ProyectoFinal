using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class SendGridMailing
    {
        public void Execute()
        {
            HelloEmail().Wait();
        }

        private static async Task HelloEmail()
        {
            String apiKey = "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";
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            Email from = new Email("cristian.pique@hotmail.com");
            String subject = "Hello World from the SendGrid CSharp Library";
            Email to = new Email("cristianpique33@gmail.com");
            Content content = new Content("text/plain", "Textual content");
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
