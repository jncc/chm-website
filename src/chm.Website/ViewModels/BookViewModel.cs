using cloudscribe.SimpleContent.ContentTemplates.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace chm.Website.ViewModels
{
    public class BookViewModel
    {
        //Title
        [Display(Name = "Book title")]
        [Required(ErrorMessage = "Title is required!"), MinLength(2, ErrorMessage = "A book title cannot be less than 2 characters!")]
        public string BookTitle { get; set; }

        //Author
        [Display(Name = "Author")]
        [Required(ErrorMessage = "Author is required!"), MinLength(2, ErrorMessage = "An Author name cannot be less than 5 characters!")]
        public string BookAuthor { get; set; }

        //ISBN
        [Display(Name = "ISBN-10 or ISBN-13")]
        [Required(ErrorMessage = "ISBN is required!"), RegularExpression("^.{1,13}$", ErrorMessage = "Not a valid ISBN!")]
        public string ISBN { get; set; }

        //Genre
        [Display(Name = "Genre")]
        public string BookGenre { get; set; }

        //Genre pick-list values
        public IEnumerable<SelectListItem> Genres
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "Fiction: romance", Text="Fiction: romance" },
                new SelectListItem { Value = "Fiction: horror", Text="Fiction: horror" },
                new SelectListItem { Value = "Fiction: tragedy", Text="Fiction: tragedy" },
                new SelectListItem { Value = "Non-fiction: hobbies and crafts", Text="Non-fiction: hobbies and crafts" },
                new SelectListItem { Value = "Non-fiction: history", Text="Non-fiction: history" },
            };
            }
        }

        //Precis
        [Display(Name = "Precis")]
        [Required(ErrorMessage = "A precis is required!"), MinLength(2, ErrorMessage = "Please tell us a bit more about the book!")]
        public string Precis { get; set; }
    }
}


