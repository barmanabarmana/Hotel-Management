using Entities.Hotels;

namespace Entities.Files
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
