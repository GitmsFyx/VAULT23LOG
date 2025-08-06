using System;

namespace LOG.Models;

public class Log
{
    public int Id { get; set; }
    public int PeopleId { get; set; }
    public People People { get; set; }
    public DateTime CreateTime { get; set; }
    public string Content { get; set; }
    public bool Visible { get; set; }
}