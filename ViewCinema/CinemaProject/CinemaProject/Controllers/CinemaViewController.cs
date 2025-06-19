using CinemaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaProject.Controllers
{
    public class CinemaViewController : Controller
    {
        public IActionResult Home()
        {
            ViewBag.Movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Dưới đáy hồ", PosterUrl = "/images/Phim Dưới đáy hồ.jpg", Duration = "1h18" },
                new Movie { Id = 2, Title = "Doraemon Movie 44", PosterUrl = "/images/Nobita và thế giới trong tranh.jpg", Duration = "2h00" }
            };

            ViewBag.Cinemas = new List<Cinema>
            {
                new Cinema { Id = 1, Name = "Beta Quang Trung", Area = "TP. Hồ Chí Minh" }
            };

            ViewBag.Showtimes = new List<Showtime>
            {
                new Showtime { Id = 1, MovieId = 1, CinemaId = 1, Time = DateTime.Parse("2025-06-07T13:00:00") },
                new Showtime { Id = 2, MovieId = 2, CinemaId = 1, Time = DateTime.Parse("2025-06-07T08:00:00") }
            };

            return View("Views/CinemaView/Home.cshtml");
        }
    }
}
