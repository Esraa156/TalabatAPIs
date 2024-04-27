using Talabat.Core.Entities;

namespace TalabatProject.APIs.DTOs
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
       
    }
}
