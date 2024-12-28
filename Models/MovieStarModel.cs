namespace Movie_Review_WebAPI.Models
{
    public class MovieStarModel
    {

        public int MovieID { get; set; }

        public int StarID { get; set; }

        public string? StarName { get; set; }

        public string? MovieName { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? DirectorID { get; set; }

        public string? Poster { get; set; }

        public decimal? Rating { get; set; }

        public string? Writer { get; set; }
        public string? Duration { get; set; }

    }
}
