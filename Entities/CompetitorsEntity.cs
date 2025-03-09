using Survivor.Entities;

public class CompetitorsEntity : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CategoryId { get; set; } // Kategori ID'si (zorunlu)

    // Relational Property
    public CategoryEntity? Category { get; set; } // Nullable 
}