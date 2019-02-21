using cloudscribe.SimpleContent.ContentTemplates.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace chm.Website.ViewModels
{
    public class HomePageViewModel : SimpleGalleryViewModel
    {
        public HomePageViewModel()
        {

        }

        public string TwitterHandle { get; set; }

        [Required(ErrorMessage ="Carousel Interval is required.")]
        public int ImageRotationIntervalInMilliseconds { get; set; } = 5000;
    }
}
