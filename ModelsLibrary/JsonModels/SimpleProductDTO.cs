using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.JsonModels
{
    public class SimpleProductDTO
    {
        public string? Name { get; set; }
        public string? Area { get; set; }
        public string? Tags { get; set; }
        public string? ProductImageSource { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
    }
}
