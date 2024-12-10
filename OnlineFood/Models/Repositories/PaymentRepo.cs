using OnlineFood.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OnlineFood.Models.Repositories
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly OnlineFoodContext _context;

        public PaymentRepo(OnlineFoodContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }
        //thêm một hoặc nhiều Bill vào một Payment
        public async Task AddBillsToPaymentAsync(int paymentId, IEnumerable<Bill> bills)
        {
            var payment = await _context.Payments
                .Include(p => p.Bills)
                .FirstOrDefaultAsync(p => p.Id == paymentId);
            if (payment != null)
            {
                foreach (var bill in bills)
                {
                    payment.Bills.Add(bill);
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment?> GetPaymentByPaymentIdAsync(int paymentId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
        }
        public async Task<Payment?> GetPaymentWithBillsByPaymentIdAsync(int paymentId)
        {
            return await _context.Payments
                .Include(p => p.Bills) // Bao gồm danh sách Bills
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == paymentId);
        }
        public async Task<int> GetBillCountByPaymentIdAsync(int paymentId)
        {
            var payment = await _context.Payments.Include(p => p.Bills).FirstOrDefaultAsync(p => p.Id == paymentId);
            return payment?.Bills.Count ?? 0;
        }
        public async Task<IEnumerable<Bill>> GetBillsByPaymentIdAsync(int paymentId)
        {
            var payment = await _context.Payments.Include(p => p.Bills).FirstOrDefaultAsync(p => p.Id == paymentId);
            return payment?.Bills ?? Enumerable.Empty<Bill>();
        }
        public async Task<int> GetMaxPaymentIdAsync()
        {
            return await _context.Payments.AnyAsync()
                ? await _context.Payments.MaxAsync(p => p.Id)
                : -1; // Hoặc giá trị mặc định khác nếu không có Payment nào
        }


        public async Task RemovePaymentByIdAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }
        }
        public async Task RemoveBillsFromPaymentAsync(int paymentId, IEnumerable<int> billIds)
        {
            var payment = await _context.Payments.Include(p => p.Bills).FirstOrDefaultAsync(p => p.Id == paymentId);
            if (payment != null)
            {
                payment.Bills = payment.Bills.Where(b => !billIds.Contains(b.Id)).ToList();
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentByIdAsync(int paymentId, Payment updatedPayment, IEnumerable<Bill>? updatedBills = null)
        {
            var existingPayment = await _context.Payments.Include(p => p.Bills).FirstOrDefaultAsync(p => p.Id == paymentId);
            if (existingPayment != null)
            {
                // Cập nhật thông tin Payment
                _context.Entry(existingPayment).CurrentValues.SetValues(updatedPayment);

                // Nếu có danh sách Bills được cập nhật
                if (updatedBills != null)
                {
                    existingPayment.Bills = updatedBills.ToList();
                }

                await _context.SaveChangesAsync();
            }
        }

    }
}
