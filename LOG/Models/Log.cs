using System;

namespace LOG.Models;

public class Log
{
    public int Id { get; set; }
    public int PeopleId { get; set; }
    public People People { get; set; }
    public DateTime CreateTime { get; set; }=DateTime.Now;
    public string Content { get; set; }
    public bool Visible { get; set; } = true;
}