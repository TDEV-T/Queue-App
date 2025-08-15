using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueueService.Models;

[Table("tickets")]
public partial class Ticket
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("ticket_number")]
    [StringLength(3)]
    public string TicketNumber { get; set; } = null!;

    [Column("issued_at")]
    public DateTime IssuedAt { get; set; }

    [Column("status")]
    public string Status { get; set; } = "Waiting";
}
