﻿using Journey.DataAccess.Common;
using Journey.DataAccess.Identity;

namespace Journey.DataAccess.Entities
{
    public class Place : BaseEntity, IAuditedEntity
    {
        public string PlaceName { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
