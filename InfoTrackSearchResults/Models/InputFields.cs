using System.ComponentModel.DataAnnotations;

namespace InfoTrackSearchResults.Models
{
    public class InputFields
    {
        [Url]
        [Required(ErrorMessage="Please provide a URL")]
        public string Url { get; set; }
        [Required(ErrorMessage="Please provide key words")]
        public string KeyWords { get; set; }
    }
}