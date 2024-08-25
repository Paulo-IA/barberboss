using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses;

public class ResponseShortBillingJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount {  get; set; }
}
