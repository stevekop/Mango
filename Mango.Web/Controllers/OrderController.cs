﻿using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Service.IService;
using Mango.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }


        public IActionResult OrderIndex()
        {
            return View();
        }

        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            var response = await _orderService.GetOrder(orderId);
            if (response !=null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }

            if (!User.IsInRole(SD.RoleAdmin) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }
            return View(orderHeaderDto);
        }


        [HttpGet]
        public IActionResult GetAll() 
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            if (!User.IsInRole(SD.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }
            ResponseDto response = _orderService.GetAllOrders(userId).GetAwaiter().GetResult();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new {data = list});
        }

    }
}