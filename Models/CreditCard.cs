using eCommerceMvc.Models.Shared.Exceptions;

namespace eCommerce.Models;

public class CreditCard
{
    public CreditCard(string number)
    {
        this.Number = number;
    }

    public decimal? Balance { get; private set; }
    public decimal? Credit { get; private set; }
    public string Number { get; set; }



    public void AssignCreditLimit(decimal credit)
    {
        if (this.Credit != null)
            throw new CreditCardLimitIsAlreadyAssigned();
        this.Credit = credit;
        this.AddBalance(credit);
    }

    private void AddBalance(decimal balance)
    {
        this.Balance = this.Balance + balance ?? balance;
    }

    public void Withdraw(decimal balanceToWithdraw)
    {
        if(this.Balance < balanceToWithdraw)
            throw new CreditCardNotEnoughBalance();
        this.Balance -= balanceToWithdraw;
    }
}