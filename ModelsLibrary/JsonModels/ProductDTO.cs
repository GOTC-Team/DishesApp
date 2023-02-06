using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.JsonModels
{
    public class ProductDTO
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public string? Instructions { get; set; }
        public string? ProductImageSource { get; set; }
        public string[]? Tags { get; set; }
        public string? YoutubeVideoSource { get; set; }
        public List<(string, string)> IngredientsMeasures { get; set; } = new List<(string, string)>();
        public string? InstructionsSource { get; set; }
    }
}
