using System.Collections;
using System.Collections.Generic;

namespace LOG.Models;

public class People
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public int ArchiveId { get; set; }

    public Archive Archive { get; set; }
    public ICollection<Log> Logs { get; } = new List<Log>();
    public ICollection<Photo> Photos { get; } = new List<Photo>();

}