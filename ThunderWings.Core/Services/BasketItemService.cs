using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderWings.Core.Services.Interfaces;
using ThunderWings.Repo.DAL;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Services
{
    public class BasketItemService : GenericRepository<BasketItem>, IBasketItemService
    {
        public BasketItemService(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task DeleteBasketItem(int basketItemId)
        {
            var basketItem = await Get(basketItemId);
            if (basketItem == null)
            {
                throw new Exception("Basket item not found");
            }

            await Delete(basketItem);

            await SaveChanges();
        }
    }
}
