using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses;

public class ResponseGetAllBillingsJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount {  get; set; }
    public PaymentType PaymentType { get; set; }
}
