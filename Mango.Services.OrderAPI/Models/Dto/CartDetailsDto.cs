﻿// Ignore Spelling: Dto

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.OrderAPI.Models.Dto
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderID { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
