using OnlineFood.Models.Repositories;
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
        public async Task SendEmail(string email)
        {
            // Đây là một phần khác, bạn cần tích hợp dịch vụ gửi email tại đây
            // Ví dụ minh họa:
            await Task.Run(() =>
            {
                Console.WriteLine($"Email sent to {email}");
            });
        }
    }
}
