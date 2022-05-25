﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Entities
{
    [Index(nameof(PlaceName), IsUnique = true)]
    public class Place : Record
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string PlaceName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(4000)]
        public string Description { get; set; }
        [Required]
        public int Rank { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public int PricePerNight { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }

        public string? AccountId { get; set; }
        [NotMapped]
        public IdentityUser? Account { get; set; }
    }
}
