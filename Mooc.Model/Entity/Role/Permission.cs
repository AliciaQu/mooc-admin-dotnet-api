using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mooc.Model.Entity
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Code { get; set; }  

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
            = new List<RolePermission>();
    }
}

