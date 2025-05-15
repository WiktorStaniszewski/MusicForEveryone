namespace EShop.Application.Service;

public interface ICreditCardService
{
    public Boolean ValidateCardNumber(string cardNumber);

    public string GetCardType(string cardNumber);
}
