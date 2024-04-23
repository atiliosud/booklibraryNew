using System.ComponentModel.DataAnnotations;

public partial class Book
{
    [Key]
    public int BookId { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; } = null!;

   
    public int TotalCopies { get; set; }
    public int CopiesInUse { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }

    [StringLength(100)]
    public string Author { get; set; }

    [StringLength(50)]
    public string? Category { get; set; }
}
