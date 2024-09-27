namespace Invitro.Models;

public class Procedure : BaseDbItem
{
    public string name { get; set; }
    public string? description { get; set; }
    public decimal? price { get; set; }
    public bool archived { get; set; } = false;
}