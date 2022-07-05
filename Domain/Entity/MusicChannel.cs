using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class MusicChannel
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public ulong GuildId { get; set; }
    public ulong MusicChannelId { get; set; }
}