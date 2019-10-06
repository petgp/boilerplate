using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Models
{
  public class Sample
  {
    [Key]
    public int Id { get; set; }
    public string Owner_id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Img_url { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public int Ownership_length { get; set; }
  }
}
