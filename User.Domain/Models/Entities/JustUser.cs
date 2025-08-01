﻿using System.ComponentModel.DataAnnotations;

namespace User.Domain.Models.Entities;

public class JustUser
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public ICollection<Role> Roles { get; set; } = new List<Role>();

    public DateTime CreatedAt = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }

    public bool IsActive { get; set; } = true;

}