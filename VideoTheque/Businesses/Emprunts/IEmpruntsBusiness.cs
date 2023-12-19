﻿using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Emprunts
{
    public interface IEmpruntsBusiness
    {
        //Empruntes un film à un hôte
        void EmpruntFilm(int idHost, int idFilm);
        
        //Passes un film en emprunté et retourne la copie du film
        EmpruntViewModel EmpruntFilm(int idFilm);

        //Supprime un emprunt
        void DeleteEmprunt(string filmName);
        
        //Récupère nos films empruntables
        EmpruntableFilmViewModel GetEmpruntableFilms();
        
        //Recupère les films empruntables de l'hôte
        EmpruntableFilmViewModel GetEmpruntableFilms(int idHost);
    }
}