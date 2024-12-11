using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Models;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;

namespace Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        private const int CustomersPerPage = 5;
        private readonly List<Order> _orders;
        private const int PageSize = 10;

        private readonly OnlineFoodContext _context;

        public DashboardController(OnlineFoodContext context)
        {
            // Sample data
            _context = context;
            _orders = new List<Order>
            {
               new Order
            {
                OrderId = "ORD-001",
                CustomerId = "CUST-001",
                Customer = new Customer
                {
                    Id = "CUST-001",
                    Name = "John Doe",
                    Contact = "+1234567890",
                    Email = "john@example.com"
                },
                OrderDate = DateTime.Now.AddDays(-1),
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        ProductId = "PROD-001",
                        Product = new Product
                        {
                            Id = "PROD-001",
                            Name = "iPhone 14 Pro",
                            ImageUrl = "/images/products/iphone.jpg",
                            Price = 999
                        },
                        Quantity = 1,
                        UnitPrice = 999
                    },
                    new OrderItem
                    {
                        Id = 2,
                        ProductId = "PROD-002",
                        Product = new Product
                        {
                            Id = "PROD-002",
                            Name = "AirPods Pro",
                            ImageUrl = "/images/products/airpods.jpg",
                            Price = 249
                        },
                        Quantity = 2,
                        UnitPrice = 249
                    }
                },
                Promotions = new List<PromotionUsed>
                {
                    new PromotionUsed
                    {
                        Id = 1,
                        Code = "SUMMER2023",
                        DiscountAmount = 100
                    },
                    new PromotionUsed
                    {
                        Id = 2,
                        Code = "WELCOME10",
                        DiscountAmount = 50
                    }
                },
                SubTotal = 1497, // 999 + (2 * 249)
                DiscountPercent = 10,
                TotalPrice = 1347.30m, // After 10% discount
                PaymentMethod = "Credit Card",
                Status = OrderStatus.Paid
            },
            new Order
            {
                OrderId = "ORD-002",
                CustomerId = "CUST-002",
                Customer = new Customer
                {
                    Id = "CUST-002",
                    Name = "Jane Smith",
                    Contact = "+1234567891",
                    Email = "jane@example.com"
                },
                OrderDate = DateTime.Now.AddDays(-2),
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 3,
                        ProductId = "PROD-003",
                        Product = new Product
                        {
                            Id = "PROD-003",
                            Name = "MacBook Pro",
                            ImageUrl = "/images/products/macbook.jpg",
                            Price = 1999
                        },
                        Quantity = 1,
                        UnitPrice = 1999
                    }
                },
                Promotions = new List<PromotionUsed>
                {
                    new PromotionUsed
                    {
                        Id = 3,
                        Code = "STUDENT20",
                        DiscountAmount = 399.80m
                    }
                },
                SubTotal = 1999,
                DiscountPercent = 20,
                TotalPrice = 1599.20m,
                PaymentMethod = "PayPal",
                Status = OrderStatus.Pending
            }
            };
        }
        private async Task<List<IncomeData>> GetIncomeData(DateTime startDate, DateTime? endDate = null)
        {
            var query = from b in _context.Bills
                        join bs in _context.BillSupplies on b.NgayCheckOut.Date equals bs.Date.Date into billSuppliesGroup
                        from bs in billSuppliesGroup.DefaultIfEmpty()
                        where b.NgayCheckOut >= startDate && (endDate == null || b.NgayCheckOut <= endDate)
                        group new { b, bs } by b.NgayCheckOut.Date into g
                        select new IncomeData
                        {
                            Date = g.Key,
                            Revenue = g.Sum(x => x.b.TongTienSau),
                            Expenses = g.Sum(x => (double?)x.bs.TongTien) ?? 0
                        };

            return await query.ToListAsync();
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalTopCustomers = await _context.Bills
                .GroupBy(b => b.IdKhachHang)
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalTopCustomers / (double)CustomersPerPage);

            var viewModel = new DashboardViewModel
            {
                DateMode = "single",
                SingleDate = DateTime.Today,
                IncomeData = await GetIncomeData(DateTime.Today),
                TopProducts = await GetTopProducts(),
                TopCustomers = await GetTopCustomers(page),
                CurrentPage = page,
                TotalPages = totalPages,
                PaymentList = await GetPaymentListViewModel(page)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoadData(string dateMode, DateTime? singleDate, DateTime? startDate, DateTime? endDate, int page = 1)
        {
            var totalTopCustomers = await _context.Bills
                .GroupBy(b => b.IdKhachHang)
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalTopCustomers / (double)CustomersPerPage);

            var viewModel = new DashboardViewModel
            {
                DateMode = dateMode,
                SingleDate = singleDate,
                StartDate = startDate,
                EndDate = endDate,
                IncomeData = await GetIncomeData(singleDate ?? startDate ?? DateTime.Today, endDate),
                TopProducts = await GetTopProducts(),
                TopCustomers = await GetTopCustomers(page),
                CurrentPage = page,
                TotalPages = totalPages,
                PaymentList = await GetPaymentListViewModel(page),
                CurrentDate = DateTime.Now
            };

            return Json(viewModel);
        }

        private async Task<List<Product>> GetTopProducts()
        {
            return await _context.Bills
                .Include(b => b.IdOrderInfoNavigation)
                .ThenInclude(oi => oi.IdMonAnNavigation)
                .GroupBy(b => b.IdOrderInfoNavigation.IdMonAn)
                .Select(g => new Product
                {
                    Id = g.Key.ToString(),
                    Name = g.First().IdOrderInfoNavigation.IdMonAnNavigation.TenMonAn,
                    ImageUrl = g.First().IdOrderInfoNavigation.IdMonAnNavigation.Hinhanh ?? "/images/placeholder.svg",
                    OrderCount = g.Sum(b => b.IdOrderInfoNavigation.SoLuongHangMon),
                    Price = g.First().IdOrderInfoNavigation.IdMonAnNavigation.GiaTien
                })
                .OrderByDescending(p => p.OrderCount)
                .Take(6)
                .ToListAsync();
        }

        private async Task<List<Customer>> GetTopCustomers(int page = 1)
        {
            var skip = (page - 1) * CustomersPerPage;

            return await _context.Bills
                .Include(b => b.IdKhachHangNavigation)
                .GroupBy(b => b.IdKhachHang)
                .Select(g => new Customer
                {
                    Id = g.Key.ToString(),
                    Name = g.First().IdKhachHangNavigation.TenKhachHang,
                    Contact = g.First().IdKhachHangNavigation.SoDienThoai,
                    Email = g.First().IdKhachHangNavigation.Email,
                    TotalSpent = g.Sum(b => (decimal)b.TongTienSau),
                    OrderCount = g.Count()
                })
                .OrderByDescending(c => c.TotalSpent)
                .Skip(skip)
                .Take(CustomersPerPage)
                .ToListAsync();
        }


        private async Task<PaymentListViewModel> GetPaymentListViewModel(int page = 1, string orderId = null, string customerName = null, OrderStatus? status = null)
        {
            var query = _context.Orders
                .Include(o => o.IdKhachHangNavigation)
                .Include(o => o.IdFoodNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(orderId))
            {
                query = query.Where(o => o.Id.ToString().Contains(orderId));
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(o => o.IdKhachHangNavigation.TenKhachHang.Contains(customerName));
            }

            if (status.HasValue)
            {
                string statusString = status.ToString();
                query = query.Where(o => o.TrangThai == statusString);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var orders = await query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(o => new
                {
                    o.Id,
                    o.IdKhachHang,
                    CustomerName = o.IdKhachHangNavigation.TenKhachHang,
                    CustomerContact = o.IdKhachHangNavigation.SoDienThoai,
                    CustomerEmail = o.IdKhachHangNavigation.Email,
                    o.Date,
                    o.IdFood,
                    FoodName = o.IdFoodNavigation.TenMonAn,
                    FoodImage = o.IdFoodNavigation.Hinhanh,
                    o.IdFoodNavigation.GiaTien,
                    o.TongSoLuong,
                    o.TrangThai
                })
                .ToListAsync();

            var mappedOrders = orders.Select(o => new Order
            {
                OrderId = o.Id.ToString(),
                CustomerId = o.IdKhachHang.ToString(),
                Customer = new Customer
                {
                    Id = o.IdKhachHang.ToString(),
                    Name = o.CustomerName,
                    Contact = o.CustomerContact,
                    Email = o.CustomerEmail
                },
                OrderDate = o.Date,
                Items = new List<OrderItem>
        {
            new OrderItem
            {
                Id = o.IdFood,
                ProductId = o.IdFood.ToString(),
                Product = new Product
                {
                    Id = o.IdFood.ToString(),
                    Name = o.FoodName,
                    ImageUrl = o.FoodImage ?? "/images/placeholder.svg",
                    Price = o.GiaTien
                },
                Quantity = o.TongSoLuong,
                UnitPrice = o.GiaTien
            }
        },
                SubTotal = o.TongSoLuong * o.GiaTien,
                TotalPrice = o.TongSoLuong * o.GiaTien,
                Status = Enum.TryParse<OrderStatus>(o.TrangThai, out var parsedStatus) ? parsedStatus : OrderStatus.Pending
            }).ToList();

            return new PaymentListViewModel
            {
                Orders = mappedOrders,
                CurrentPage = page,
                TotalPages = totalPages,
                SearchOrderId = orderId,
                SearchCustomerName = customerName,
                FilterStatus = status
            };
        }



        [HttpGet]
        public async Task<IActionResult> GetPaymentList(int page = 1, string orderId = null, string customerName = null, OrderStatus? status = null)
        {
            var viewModel = await GetPaymentListViewModel(page, orderId, customerName, status);
            return Json(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentDetails(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.IdKhachHangNavigation)
                .Include(o => o.IdFoodNavigation)
                .Include(o => o.IdCartNavigation)
                    .ThenInclude(c => c.IdKmNavigation)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }

            var orderDetails = new
            {
                OrderId = order.Id.ToString(),
                Customer = new
                {
                    Id = order.IdKhachHang.ToString(),
                    Name = order.IdKhachHangNavigation.TenKhachHang,
                    Contact = order.IdKhachHangNavigation.SoDienThoai,
                    Email = order.IdKhachHangNavigation.Email
                },
                OrderDate = order.Date,
                Items = new List<object>
                {
                    new
                    {
                        Id = order.IdFood,
                        ProductId = order.IdFood.ToString(),
                        ProductName = order.IdFoodNavigation.TenMonAn,
                        ImageUrl = order.IdFoodNavigation.Hinhanh ?? "/images/placeholder.svg",
                        Quantity = order.TongSoLuong,
                        UnitPrice = order.IdFoodNavigation.GiaTien,
                        TotalPrice = order.TongSoLuong * order.IdFoodNavigation.GiaTien
                    }
                },
                Promotions = order.IdCartNavigation.IdKmNavigation != null
                    ? new List<object>
                    {
                        new
                        {
                            Id = order.IdCartNavigation.IdKmNavigation.Id,
                            Code = order.IdCartNavigation.IdKmNavigation.Code,
                            DiscountAmount = order.IdCartNavigation.IdKmNavigation.PhanTram
                        }
                    }
                    : new List<object>(),
                SubTotal = order.TongSoLuong * order.IdFoodNavigation.GiaTien,
                DiscountPercent = order.IdCartNavigation.IdKmNavigation?.PhanTram ?? 0,
                TotalPrice = order.TongSoLuong * order.IdFoodNavigation.GiaTien * (100 - (order.IdCartNavigation.IdKmNavigation?.PhanTram ?? 0)) / 100,
                Status = order.TrangThai
            };

            return Json(orderDetails);
        }



    }
}
