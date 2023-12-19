namespace VideoTheque.ViewModels
{
    public class EmpruntableFilmViewModel
    {

        public EmpruntableFilmViewModel(int id, string title)
        {
            this.id = id;
            this.title = title;
        }
        
        public String title { get; set; }
        
        public int id { get; set; }
        
    }
}