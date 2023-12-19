namespace VideoTheque.ViewModels
{
    public class EmpruntViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Duration { get; set; }
        public AgeRatingViewModel AgeRating { get; set; }
        public GenreViewModel Genre { get; set; }
        public PersonneViewModel Director { get; set; }
        public PersonneViewModel Scenarist { get; set; }
        public PersonneViewModel FirstActor { get; set; }
        public string Support { get; set; }
    }
}