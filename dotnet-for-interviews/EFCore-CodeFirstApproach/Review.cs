public class Review
{
    public int Id { get; set; }
    public int BookId { get; set; } // Foreign Key by convention
    public Book Book { get; set; }
    public string ReviewerName { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}