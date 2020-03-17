using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

using Vidly.ViewModels;

namespace Vidly.Controllers
{
    //Responsibilities of a controller:
    // 1. Feature handles for request from Views.
    // 2. Get data from Models to return to Views.
    // 3. Return data to the Views.
    public class MoviesController : Controller
    {
        //STEP 1: fetures a handle for request at the address movies/random
        [Route("movies/random")]
        public ActionResult Random()
        {
            //STEP 2: Get data from Models to return to Views
            var movie = new Movie() { Name = "Shrek" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1" }, 
                new Customer { Name = "Customer 2" }
            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            //STEP 3: Return data to the Views.
            return View(viewModel);
            /*
            NOTE: alternate returns...
            return Content("Hello World");                                                  -   returns blank text-content (not a view)
            return HttpNotFound();                                                          -   returns 404 error view
            return EmptyResult();                                                           -   returns no-result (void)
            return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });    -   redirects to alternate route/action and passes relevant parameters
            */
             
        }

        public ActionResult Edit(int id) 
        {
            return Content("Id = " + id);
        }
        [Route("movies/details/{id}")]
        public ActionResult Details(int id) 
        {
            var movie = GetMovies().SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            
            return View(movie);
        }
       

        /*[Route("Customer/Details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }*/
        [Route("movies/index/")]
        public ActionResult Index()
        {
            //get data (hardcoded)
            var movies = GetMovies();
            // return data to the view
            return View(movies);
        }

        private IEnumerable<Movie> GetMovies() 
        {
            return new List<Movie>
            {
                new Movie {Id = 1, Name = "The Matrix" },
                new Movie {Id = 2, Name = "The Matrix: Reloaded" },
                new Movie {Id = 3, Name = "The Matrix: Revolution" },
                new Movie {Id = 4, Name = "Wall-E" }
            };
        }





        //determines the path for the Action (Controller method - corresponding to a view)
        [Route("movies/released/{year:regex(\\d{4}:range(1900,3000))}/{month:regex(\\d{2}:range(1,12))}")]
        public ActionResult ByReleaseDate(int year, int month) 
        {
            return Content(year + "/" + month);
        }





        // How to pass nullable arguements from the input url to the controller/model
        /*public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }*/


    }
}