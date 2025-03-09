namespace Survivor.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }

        // Relational Property
        public ICollection<CompetitorsEntity> Competitors { get; set; } = new List<CompetitorsEntity>(); // Varsayılan değer
    }
}