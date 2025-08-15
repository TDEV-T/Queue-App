using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QueueService.Models;

[Table("queue_states")]
public partial class QueueState
{
    [Key]
    [Column("id")]
    public short Id { get; set; }

    [Column("current_prefix")]
    [MaxLength(1)]
    public char CurrentPrefix { get; set; }

    [Column("current_number")]
    public int CurrentNumber { get; set; }

    [Column("version")]
    public int Version { get; set; }

    [Column("last_updated_at")]
    public DateTime LastUpdatedAt { get; set; }
}
