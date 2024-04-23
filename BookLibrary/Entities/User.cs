using System.ComponentModel.DataAnnotations;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = null!;

}
