using BookStore.API.Models;

namespace BookStore.API.DTOs
{
    public class CreatePublisherDto
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
