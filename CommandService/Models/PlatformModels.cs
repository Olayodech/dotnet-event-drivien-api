using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CommandService.Models
{
    public class PlatformModels {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ExternalId { get; set; }
        public ICollection<CommandModel> Commands { get; set; } = new List<CommandModel>();
    }
}