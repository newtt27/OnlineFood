using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentList.Models
{
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
    }

    public class PromotionUsed
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled
    }

    public class OrderListViewModel
    {
        public List<Order> Orders { get; set; }
        public string SearchOrderId { get; set; }
        public string SearchCustomerName { get; set; }
        public DateTime? SearchDate { get; set; }
        public DateTime? SearchStartDate { get; set; }
        public DateTime? SearchEndDate { get; set; }
        public OrderStatus? FilterStatus { get; set; }
    }
}