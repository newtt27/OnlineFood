using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Models;
using System.Drawing.Printing;

namespace Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        private const int CustomersPerPage = 5;
        private readonly List<Order> _orders;
        private const int PageSize = 10;

        public DashboardController()
        {
            // Sample data
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
        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                DateMode = "single",
                SingleDate = DateTime.Today,
                IncomeData = GetIncomeData(DateTime.Today),
                TopProducts = GetTopProducts(),
                TopCustomers = GetTopCustomers(1),
                CurrentPage = 1,
                TotalPages = (int)Math.Ceiling(GetTopCustomers().Count / 5.0),
                PaymentList = GetPaymentListViewModel()

            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult LoadData(string dateMode, DateTime? singleDate, DateTime? startDate, DateTime? endDate, int page = 1)
        {
            var viewModel = new DashboardViewModel
            {
                DateMode = dateMode,
                SingleDate = singleDate,
                StartDate = startDate,
                EndDate = endDate,
                IncomeData = GetIncomeData(singleDate ?? startDate ?? DateTime.Today, endDate),
                TopProducts = GetTopProducts(),
                TopCustomers = GetTopCustomers(page),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(GetTopCustomers().Count / 5.0),
                PaymentList = GetPaymentListViewModel(page),
                CurrentDate = DateTime.Now

            };

            return Json(viewModel);
        }

        [HttpGet]
        public IActionResult GetPaymentList(int page = 1, string orderId = null, string customerName = null, OrderStatus? status = null)
        {
            var query = _orders.AsQueryable();

            if (!string.IsNullOrEmpty(orderId))
            {
                query = query.Where(o => o.OrderId.Contains(orderId));
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(o => o.Customer.Name.Contains(customerName));
            }

            if (status.HasValue)
            {
                OrderStatus orderStatus = (OrderStatus)status.Value;
                query = query.Where(o => o.Status == orderStatus);
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var orders = query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var viewModel = new PaymentListViewModel
            {
                Orders = orders,
                CurrentPage = page,
                TotalPages = totalPages,
                SearchOrderId = orderId,
                SearchCustomerName = customerName,
                FilterStatus = status
            };

            return Json(viewModel);
        }

        [HttpGet]
        public IActionResult GetPaymentDetails(string orderId)
        {
           
            var order = _orders.FirstOrDefault(o => o.OrderId.Equals(orderId, StringComparison.OrdinalIgnoreCase));

            if (order == null)
            {
               
                return NotFound(new { message = "Order not found" });
            }

            // Trả về thông tin đơn hàng dưới dạng JSON
            var orderDetails = new
            {
                OrderId = order.OrderId,
                Customer = new
                {
                    order.Customer.Id,
                    order.Customer.Name,
                    order.Customer.Contact,
                    order.Customer.Email
                },
                OrderDate = order.OrderDate,
                Items = order.Items.Select(item => new
                {
                    item.Id,
                    item.ProductId,
                    ProductName = item.Product.Name,
                    item.Product.ImageUrl,
                    item.Quantity,
                    item.UnitPrice,
                    TotalPrice = item.Quantity * item.UnitPrice
                }),
                Promotions = order.Promotions.Select(promo => new
                { 
                    promo.Id,
                    promo.Code,
                    promo.DiscountAmount
                }),
                SubTotal = order.SubTotal,
                DiscountPercent = order.DiscountPercent,
                TotalPrice = order.TotalPrice,
                PaymentMethod = order.PaymentMethod,
                Status = order.Status.ToString()
            };

            return Json(orderDetails);
        }


        private List<IncomeData> GetIncomeData(DateTime startDate, DateTime? endDate = null)
        {
            // Replace with actual data retrieval logic
            var random = new Random();
            var data = new List<IncomeData>();
            var currentDate = startDate;
            var endDateTime = endDate ?? startDate.AddDays(6);

            while (currentDate <= endDateTime)
            {
                data.Add(new IncomeData
                {
                    Date = currentDate,
                    Revenue = random.Next(5000, 10000),
                    Expenses = random.Next(3000, 7000)
                });
                currentDate = currentDate.AddDays(1);
            }

            return data;
        }

        private List<Product> GetTopProducts()
        {
            // Replace with actual data retrieval logic
            return new List<Product>
            {
                new Product { Name = "Product A", OrderCount = 150, ImageUrl = "/images/placeholder.svg" },
                new Product { Name = "Product B", OrderCount = 120, ImageUrl = "/images/placeholder.svg" },
                new Product { Name = "Product C", OrderCount = 100, ImageUrl = "/images/placeholder.svg" },
                new Product { Name = "Product D", OrderCount = 90, ImageUrl = "/images/placeholder.svg" },
                new Product { Name = "Product E", OrderCount = 80, ImageUrl = "/images/placeholder.svg" },
                new Product { Name = "Product F", OrderCount = 70, ImageUrl = "/images/placeholder.svg" },
                new Product { Name = "Product G", OrderCount = 60, ImageUrl = "/images/placeholder.svg" }
            };
        }


        private List<Customer> GetTopCustomers(int page = 1)
        {
            // Replace with actual data retrieval logic
            var allCustomers = new List<Customer>
            {
                new Customer { Id = "CUST-001", Name = "Neil Sims", Email = "neil.sims@example.com", AvatarUrl = "/images/placeholder.svg", TotalSpent = 3320 },
                new Customer { Id = "CUST-002", Name = "Bonnie Green", Email = "bonnie.green@example.com", AvatarUrl = "/images/placeholder.svg", TotalSpent = 2467 },
                new Customer { Id = "CUST-003", Name = "Michael Gough", Email = "michael.gough@example.com", AvatarUrl = "/images/placeholder.svg", TotalSpent = 2235 },
                new Customer { Id = "CUST-004", Name = "Thomas Lean", Email = "thomas.lean@example.com", AvatarUrl = "/images/placeholder.svg", TotalSpent = 1842 },
                new Customer { Id = "CUST-005", Name = "Lana Byrd", Email = "lana.byrd@example.com", AvatarUrl = "/images/placeholder.svg", TotalSpent = 1044 },
                new Customer { Id = "CUST-006", Name = "Karen Nelson", Email = "karen.nelson@example.com", AvatarUrl = "/images/placeholder.svg", TotalSpent = 980 },
                new Customer { Id = "CUST-007", Name = "Robert Smith", Email = "robert.smith@example.com", AvatarUrl = "/images/placeholder.svg", TotalSpent = 890 }
            };

            return allCustomers.Skip((page - 1) * CustomersPerPage).Take(CustomersPerPage).ToList();
        }

        private PaymentListViewModel GetPaymentListViewModel(int page = 1)
        {
            var totalItems = _orders.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            var orders = _orders
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return new PaymentListViewModel
            {
                Orders = orders,
                CurrentPage = page,
                TotalPages = totalPages
            };
        }



    }
}
