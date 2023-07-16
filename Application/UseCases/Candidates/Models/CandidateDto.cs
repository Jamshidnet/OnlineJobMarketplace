using Microsoft.AspNetCore.Http;
using OnlineJobMarketplace.Domein.Enums;

namespace OnlineJobMarketplace.Application.UseCases.Students.Models
{
    public  record CandidateDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public decimal ExperienceDuration { get; set; }

        public string? ImageName { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string Education { get; set; }
        public Gender Gender { get; set; }
    }
}
