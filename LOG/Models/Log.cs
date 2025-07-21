namespace LOG.Models;


public class Log
{
    public int Id { get; set; }
    
    public string CreateTime { get; set; }
    
    public string Content { get; set; }
    
    public bool Visible { get; set; }
}