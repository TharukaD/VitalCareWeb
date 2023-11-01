using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace VitalCareWeb.Services.EmailService;
public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;
    public EmailService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public string GetHTMLEmailContent(string header, string content)
    {
        return
            $"<!DOCTYPE html>\r\n" +
            $"<html>\r\n" +
            $"<head>\r\n" +
            $"<title></title>\r\n" +
            $"<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\" />\r\n" +
            $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\r\n" +
            $"<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n <style type=\"text/css\">\r\nbody,\r\ntable,\r\ntd,\r\na {{\r\n -webkit-text-size-adjust: 100%;\r\n -ms-text-size-adjust: 100%;\r\n }}\r\n table,\r\n img {{\r\n -ms-interpolation-mode: bicubic;\r\n}}\r\n /* RESET STYLES */\r\n img {{\r\n border: 0;\r\n height: auto;\r\n line-height: 100%;\r\n outline: none;\r\n text-decoration: none;\r\n }}\r\n table {{\r\n border-collapse: collapse !important;\r\n}}\r\nbody{{\r\nheight: 100% !important;\r\nmargin: 0 !important;\r\npadding: 0 !important;\r\n        width: 100% !important;\r\n      }}\r\n      /* iOS BLUE LINKS */\r\n      a[x-apple-data-detectors] {{\r\ncolor: inherit !important;\r\ntext-decoration: none !important;\r\nfont-size: inherit !important;\r\n        font-family: inherit !important;\r\n font-weight: inherit !important;\r\nline-height: inherit !important;\r\n}}\r\n/* MOBILE STYLES */\r\n@media screen and (max-width: 600px) {{\r\n        h1 {{\r\n          font-size: 32px !important;\r\n          line-height: 32px !important;\r\n        }}\r\n      }}\r\n      @-webkit-keyframes fadein {{\r\n        from {{\r\n          bottom: 0;\r\nopacity: 0;\r\n}}\r\n\r\nto {{\r\nbottom: 30px;\r\nopacity: 1;\r\n }}\r\n}}\r\n@keyframes fadein {{\r\nfrom {{\r\nbottom: 0;\r\n          opacity: 0;\r\n        }}\r\n        to {{\r\n          bottom: 30px;\r\n          opacity: 1;\r\n        }}\r\n      }}\r\n      @-webkit-keyframes fadeout {{\r\n        from {{\r\n          bottom: 30px;\r\n opacity: 1;\r\n }}\r\n to {{\r\n bottom: 0;\r\n opacity: 0;\r\n }}\r\n }}\r\n @keyframes fadeout {{\r\n from {{\r\n bottom: 30px;\r\n opacity: 1;\r\n }}\r\n to {{\r\n bottom: 0;\r\n opacity: 0;\r\n }}\r\n }}\r\n div[style*=\"margin: 16px 0;\"] {{\r\n margin: 0 !important;\r\n }}\r\n</style>\r\n  " +
            $"</head>\r\n\r\n  " +
            $"<body\r\n style=\"\r\n background-color: #f4f4f4;\r\n margin: 0 !important;\r\n padding: 0 !important;\r\n \"\r\n  >\r\n " +
            $"<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n <!-- LOGO -->\r\n  " +
            $"<tr>\r\n <td bgcolor=\"#CD9D96\" align=\"center\">\r\n " +
            $"<table\r\n border=\"0\"\r\n cellpadding=\"0\"\r\n cellspacing=\"0\"\r\n width=\"100%\"\r\n style=\"max-width: 600px\"\r\n >\r\n <tr>\r\n <td\r\n align=\"center\"\r\n valign=\"top\"\r\n style=\"padding: 40px 10px 40px 10px\"\r\n ></td>\r\n </tr>\r\n </table>\r\n </td>\r\n </tr>\r\n <tr>\r\n <td bgcolor=\"#CD9D96\" align=\"center\" style=\"padding: 0px 10px 0px 10px\">\r\n <table\r\n border=\"0\"\r\n cellpadding=\"0\"\r\n cellspacing=\"0\"\r\n width=\"100%\"\r\n style=\"max-width: 600px\"\r\n >\r\n <tr>\r\n <td\r\n bgcolor=\"#ffffff\"\r\n align=\"center\"\r\n valign=\"top\"\r\n style=\"\r\n padding: 40px 20px 20px 20px;\r\n border-radius: 8px 8px 0px 0px;\r\n color: #111111;\r\n font-family: 'Lato', Helvetica, Arial, sans-serif;\r\n font-size: 48px;\r\n font-weight: 400;\r\n line-height: 48px;\r\n \"\r\n >\r\n " +
            $" <img\r\n src=\"http://104.211.218.226:8043/assets/img/VitalcareLogo.png\"\r\n width=\"125\"\r\n  height=\"120\"\r\n style=\"display: block; border: 0px\"\r\n />\r\n  " +

            $"<h1 style=\"font-size: 30px; font-weight: 400; margin: 2\">\r\n {header}\r\n </h1>\r\n </td>\r\n </tr>\r\n </table>\r\n </td>\r\n </tr>\r\n <tr>\r\n        <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px\">\r\n <table\r\n border=\"0\"\r\n cellpadding=\"0\"\r\n cellspacing=\"0\"\r\n width=\"100%\"\r\n style=\"max-width: 600px\"\r\n >\r\n  <tr>\r\n <td\r\n bgcolor=\"#ffffff\"\r\n align=\"left\"\r\n style=\"\r\n padding: 20px 30px 40px 30px;\r\n color: #666666;\r\n font-family: 'Lato', Helvetica, Arial, sans-serif;\r\n font-size: 18px;\r\n font-weight: 400;\r\n line-height: 25px;\r\n\"\r\n>\r\n" +

            $"<p style=\"margin: 0\">\r\n {content}</p>\r\n " +

            $"\r\n</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td\r\nbgcolor=\"#ffffff\"\r\nalign=\"left\"\r\nstyle=\"\r\npadding: 0px 30px 40px 30px;\r\nborder-radius: 0px 0px 8px 8px;\r\ncolor: #666666;\r\nfont-family: 'Lato', Helvetica, Arial, sans-serif;\r\n font-size: 18px;\r\n font-weight: 400;\r\n line-height: 25px;\r\n \"\r\n >\r\n " +

            $"</td>\r\n </tr>\r\n </table>\r\n </td>\r\n </tr>\r\n <tr style=\"height: 80px\">\r\n <td\r\n bgcolor=\"#f4f4f4\"\r\n align=\"center\"\r\n style=\"padding: 30px 10px 0px 10px\"\r\n >\r\n <table\r\n border=\"0\"\r\n cellpadding=\"0\"\r\n cellspacing=\"0\"\r\n width=\"100%\"\r\n style=\"max-width: 600px\"\r\n></table>\r\n</td>\r\n</tr>\r\n</table>\r\n</body>\r\n</html>\r\n";
    }

    public bool SendEmail(EmailDto request)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailConfiguration.From));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailConfiguration.Host, _emailConfiguration.Port, true);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
