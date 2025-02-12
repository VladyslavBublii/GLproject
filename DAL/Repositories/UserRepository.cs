using Core.Models;
using DAL.Data;
using DAL.Interfaces;

namespace DAL.Repositories;

public class UserRepository(StoreContext context) : Repository<User>(context), IUserRepository
{
    private StoreContext StoreContext => Context as StoreContext;
}