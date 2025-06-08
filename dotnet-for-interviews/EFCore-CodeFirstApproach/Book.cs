public class Book
{
    public int Id { get; set; } // Primary Key by convention
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    // Navigation property for relationships
    public List<Review> Reviews { get; set; }
}
