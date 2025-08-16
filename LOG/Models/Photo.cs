using System;

namespace LOG.Models;

public class Photo
{
    public int Id { get; set; }
    
    public int PeopleId { get; set; }
    
    public People People { get; set; }
    
    public DateTime CreateTime { get; set; } = DateTime.Now;
    
    public byte[] Image { get; set; }
    
    public string Content { get; set; }
    
    public bool Visible { get; set; } = true;
}