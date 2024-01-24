namespace Domain.Repositories.DTOs;

public class EventTicketsAmountDto
{
    public int Total { get; set; } = 0;
    public int Purchased { get; set; } = 0;
    public int Available { get; set; } = 0;
}