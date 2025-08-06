using System.Collections;
using System.Collections.Generic;

namespace LOG.Models;

public class People
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public ICollection<Log> Logs { get; } = new List<Log>();

}