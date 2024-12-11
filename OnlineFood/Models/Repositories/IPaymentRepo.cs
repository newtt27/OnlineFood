namespace OnlineFood.Models.Repositories
{
    public interface IPaymentRepo
    {
        Task<Payment?> GetPaymentByPaymentIdAsync(int paymentId);
        Task<int> GetMaxPaymentIdAsync();
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentByIdAsync(int paymentId, Payment updatedPayment, IEnumerable<Bill>? updatedBills = null);
        Task SaveChangesAsync();
        Task RemovePaymentByIdAsync(int PaymentId);
    }
}
