namespace ENSEKApi.Models;

public class Account
{
    public int AccountId { get; set; }
    public required string AccountName { get; set; }
    public required string AccountType { get; set; }
}
