namespace CodeChallenge.Models.ViewModel
{
    public class AddMovieViewModel
    {
        public string Title { get; set; }

        public Guid DirectorUuid { get; set; }

        public DateTime ReleaseDate { get; set; }

        public short? Rating { get; set; }

    }
}
