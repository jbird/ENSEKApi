using ENSEKApi.Models;

namespace ENSEKApi.Domain.Interfaces;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAccountsAsync();
}