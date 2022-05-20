using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Journey.Entities
{
    public enum Role
    {
        Tenant,
        LandLord
    }
    [Index(nameof(AccountName), IsUnique = true)]
    public class Account : Record
    {
        [Required, MinLength(3), MaxLength(50)]
        public string AccountName { get; set; }
        [Required, DataType(DataType.Password),
            MinLength(3), MaxLength(50)]
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<Place>? Places { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
