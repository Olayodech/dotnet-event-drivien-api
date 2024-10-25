using System.ComponentModel.DataAnnotations;
using CommandService.Models;

namespace CommandService.DTO {
    public class CommandCreateDto {
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string CommandLine { get; set; }
    }
}