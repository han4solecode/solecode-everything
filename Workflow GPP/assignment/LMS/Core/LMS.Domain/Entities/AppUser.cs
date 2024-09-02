using Microsoft.AspNetCore.Identity;

namespace LMS.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        public LibraryCard? LibraryCard { get; set; }

        [PersonalData]
        public ICollection<Lending> Lendings { get; set; } = new List<Lending>();

        [PersonalData]
        public decimal? Penalty { get; set; }

        // refresh token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}