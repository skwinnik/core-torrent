using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace transmission.netcore.Models
{
    public class AddTorrentViewModel
    {
        [Required]
        [RegularExpression(@"magnet:\?xt=urn:btih:.*")]
        public string Magnet { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
