using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_alfred.models
{
    [Table("FetchLog")]
    public class FetchLog
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public ulong User { get; set; }
        
        [Required]
        public ulong LastMessage { get; set; }
    }
}