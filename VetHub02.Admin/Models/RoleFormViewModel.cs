using System.ComponentModel.DataAnnotations;

namespace VetHub02.Admin.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(265)]
        public string Name { get; set; }
    }
}
