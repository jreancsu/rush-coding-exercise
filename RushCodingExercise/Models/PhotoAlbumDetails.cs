using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushCodingExercise.Models
{
    public class PhotoAlbumDetails
    {
        public int AlbumId { get; set; }
        public IEnumerable<PhotoDetails> PhotoDetails { get; set; } = Enumerable.Empty<PhotoDetails>();
    }
}
