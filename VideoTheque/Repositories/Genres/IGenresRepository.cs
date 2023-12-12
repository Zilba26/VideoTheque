﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Genres
{
    public interface IGenresRepository
    {
        Task<List<GenreDto>> GetGenres();

        ValueTask<GenreDto?> GetGenre(int id);

        Task<GenreDto?> GetGenre(string name);

        Task InsertGenre(GenreDto genre);

        Task UpdateGenre(int id, GenreDto genre);

        Task DeleteGenre(int id);
    }
}
