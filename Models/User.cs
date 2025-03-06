namespace BooksApiYayin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; } //buradaki soru isareti bu ozelligin null olabilecegini ifade ediyor
        //public List<RentedBook>? RentedBooks { get; set; }
    }
}
