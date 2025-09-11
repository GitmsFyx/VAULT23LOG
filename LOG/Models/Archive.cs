using System.Collections.Generic;

namespace LOG.Models;

public class Archive
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public ICollection<People> Peoples { get; } = new List<People>();
    
}