namespace VideoTheque.ViewModels
{
    public class EmpruntableFilmViewModel
    {

        public EmpruntableFilmViewModel(int id, string titre)
        {
            this.id = id;
            this.titre = titre;
        }
        
        public String titre { get; set; }
        
        public int id { get; set; }
        
    }
}