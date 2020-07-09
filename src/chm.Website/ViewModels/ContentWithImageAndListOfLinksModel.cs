using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cloudscribe.SimpleContent.ContentTemplates.ViewModels
{
    public class ContentWithImageAndListOfLinksModel
    {
        public ContentWithImageAndListOfLinksModel()
        {
            Items = new List<CHMListItemModel>();
        }

        public string Content { get; set; }
        [Display(Name = "Image URL (relative or absolute)")]
        public string Image { get; set; }
        [Display(Name = "Image caption/copyright")]
        public string ImageCaption { get; set; }
        public List<CHMListItemModel> Items { get; set; }
    }

    public class CHMListItemModel
    {
        public string LinkText { get; set; }
        public string LinkUrl { get; set; }

        public int Sort { get; set; } = 3;

        public bool OpensInNewWindow { get; set; }
    }
}
