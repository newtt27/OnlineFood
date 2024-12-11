using PaymentList.Models;
using System;
using System.Collections.Generic;

namespace Dashboard.Models
{
    public class IncomeData
    {
        public DateTime Date { get; set; }
        public double Revenue { get; set; }
        public double Expenses { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int OrderCount { get; set; }
        public decimal Price { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal OrderCount { get; set; }
    }

    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public List<PromotionUsed> Promotions { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
    }


    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    public class PromotionUsed
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
    }


    public enum OrderStatus
    {
        Paid = 1,
        Pending = 2,
        Cancelled = 3
    }
    public class PaymentListViewModel
    {
        public List<Order> Orders { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchOrderId { get; set; }
        public string SearchCustomerName { get; set; }
        public DateTime? SearchDate { get; set; }
        public DateTime? SearchStartDate { get; set; }
        public DateTime? SearchEndDate { get; set; }
        public OrderStatus? FilterStatus { get; set; }
    }
    public class DashboardViewModel
    {
        public List<IncomeData> IncomeData { get; set; }
        public List<Product> TopProducts { get; set; }
        public List<Customer> TopCustomers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string DateMode { get; set; }
        public DateTime? SingleDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public PaymentListViewModel PaymentList { get; set; }

        public DateTime CurrentDate { get; set; }
    }

    public class DailyIncome
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
    }
}