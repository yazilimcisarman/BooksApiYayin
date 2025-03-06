namespace BooksApiYayin.Models
{
    public class RentedBook
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } //kiralanan kitap ilk asamada birakilmayacgi icin end date null olabilir ? ile bunu sagladik
    }
}
