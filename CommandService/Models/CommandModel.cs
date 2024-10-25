using System.ComponentModel.DataAnnotations;
using CommandService.Models;

namespace CommandService.Models
{
    public class CommandModel {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string CommandLine { get; set; }
        [Required]
        public int PlatformId { get; set; }
        public PlatformModels Platform { get; set; }

    }
}