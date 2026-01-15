namespace Recenzex.Dtos
{
    public class FilmDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }

        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
