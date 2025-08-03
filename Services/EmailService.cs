using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;
    private readonly string _senderPassword;

    public EmailService(string smtpServer, int smtpPort, string senderEmail, string senderPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _senderEmail = senderEmail;
        _senderPassword = senderPassword;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_senderEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(toEmail);

        using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
        {
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);

            // Thêm try-catch để bắt ngoại lệ cụ thể và ghi lại lỗi
            try
            {
                // Timeout mặc định là 100000 ms (100 giây), nhưng có thể bị ghi đè.
                // Để đảm bảo, bạn có thể đặt lại timeout.
                smtpClient.Timeout = 20000; // Đặt timeout là 20 giây

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                // Ghi lại lỗi chi tiết về SMTP, ví dụ: lỗi xác thực
                Console.WriteLine($"SMTP Error: {ex.StatusCode} - {ex.Message}");
                // throw; // Tùy chọn: bạn có thể ném lại ngoại lệ hoặc xử lý khác
            }
            catch (Exception ex)
            {
                // Ghi lại các lỗi chung khác
                Console.WriteLine($"General Email Sending Error: {ex.Message}");
                // throw;
            }
        }
    }
}