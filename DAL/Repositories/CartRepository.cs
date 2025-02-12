using Core.Models;
using DAL.Data;
using DAL.Interfaces;

namespace DAL.Repositories;

public class CartRepository(StoreContext context) : Repository<Cart>(context), ICartRepository
{
    private StoreContext StoreContext => Context as StoreContext;
}