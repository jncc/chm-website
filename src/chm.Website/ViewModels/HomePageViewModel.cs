using cloudscribe.SimpleContent.ContentTemplates.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chm.Website.ViewModels
{
    public class HomePageViewModel : SectionsWithImageViewModel
    {
        public HomePageViewModel()
        {

        }

        public string TwitterHandle { get; set; }
    }
}
