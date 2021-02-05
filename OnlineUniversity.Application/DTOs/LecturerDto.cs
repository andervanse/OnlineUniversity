
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineUniversity.Application.DTOs
{
    public class LecturerDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
