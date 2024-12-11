using OnlineFood.Models.Repositories;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace OnlineFood.Models.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _paymentRepo;

        public PaymentService(IPaymentRepo paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _paymentRepo.AddPaymentAsync(payment);
        }

        public async Task<Payment?> GetPaymentByPaymentIdAsync(int paymentId)
        {
            return await _paymentRepo.GetPaymentByPaymentIdAsync(paymentId);
        }
        public async Task<int> GetMaxPaymentIdAsync()
        {
            return await _paymentRepo.GetMaxPaymentIdAsync();
        }
        public async Task RemovePaymentByIdAsync(int paymentId)
        {
            await _paymentRepo.RemovePaymentByIdAsync(paymentId);
        }
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepo.GetAllPaymentsAsync();
        }
        public async Task UpdatePaymentByIdAsync(int paymentId, Payment updatedPayment)
        {
            var payment = await _paymentRepo.GetPaymentByPaymentIdAsync(paymentId);
            if (payment != null)
            {
                payment.PhuongThucThanhToan = updatedPayment.PhuongThucThanhToan;
                payment.Mota = updatedPayment.Mota;
                payment.TrangThai = updatedPayment.TrangThai;

                await _paymentRepo.SaveChangesAsync();
            }
        }
        public async Task SendEmail(string email, string content)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;  // Cổng 587 sử dụng TLS
                smtpClient.Credentials = new NetworkCredential("lebuutri89@gmail.com", "trqd zzor qqdq nbhn"); // Mật khẩu ứng dụng
                smtpClient.EnableSsl = true;  // Đảm bảo kết nối được mã hóa

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("lebuutri89@gmail.com"),
                    Subject = "Đặt hàng thành công",
                    Body = content,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);
                //mailMessage.To.Add("lebuutri89@gmail.com"); // Gửi đến chính bạn

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent to yourself successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }


    }
}