namespace OnlineFood.Models.Services
{
    public interface IPaymentService
    {
        Task AddPaymentAsync(Payment payment);
        Task<Payment?> GetPaymentByPaymentIdAsync(int paymentId);
        Task<int> GetMaxPaymentIdAsync();
        Task RemovePaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task UpdatePaymentByIdAsync(int paymentId, Payment updatedPayment); // Thêm phương thức này
        Task SendEmail(string email, string content);
    }
}