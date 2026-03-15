using ContactsX.Domain.Common.Entities;
using ContactsX.Domain.ValueOpjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsX.Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [Column("user_name")]
    public string UserName { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [Column("role")]
    public UserRole Role { get; set; }

    [Column("status")]
    public UserStatus Status { get; set; }

}
