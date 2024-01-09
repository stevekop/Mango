using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto> CreateOrder(CartDto cartDto);
        Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto);
        Task<ResponseDto?> ValidateStripeSession(int orderHeaderId);
        Task<ResponseDto?> GetOrder(int orderId);
        Task<ResponseDto?> GetAllOrders(string? userId);
        Task<ResponseDto?> UpdateOrder(int orderId, string? newStatus);

    }
}
