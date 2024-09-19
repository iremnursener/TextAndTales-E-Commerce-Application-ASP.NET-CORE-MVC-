using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{

    [Area("customer")]
    [Authorize]
    public class CardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCardVM ShoppingCardVM { get; set; }
        public CardController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }









        public IActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCardVM = new()
            {
                ShoppingCards = _unitOfWork.ShoppingCard.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Product"

                )


            };

            foreach (var card in ShoppingCardVM.ShoppingCards)
            {
                card.Price = GetPriceBasedOnQuantity(card);
                ShoppingCardVM.TotalOrder += (card.Price * card.Count);
            }
            return View(ShoppingCardVM);
        }

        public IActionResult Summary()
        {
            return View();
        }





        public IActionResult Plus(int cardId)
        {

            var cardFromDb = _unitOfWork.ShoppingCard.Get(u => u.Id == cardId);
            cardFromDb.Count += 1;
            _unitOfWork.ShoppingCard.Update(cardFromDb);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));

        }


        public IActionResult Minus(int cardId)
        {

            var cardFromDb = _unitOfWork.ShoppingCard.Get(u => u.Id == cardId);
            if (cardFromDb.Count <= 1)
            {
                //remove that from card
                _unitOfWork.ShoppingCard.Remove(cardFromDb);

            }
            else
            {

                cardFromDb.Count -= 1;
                _unitOfWork.ShoppingCard.Update(cardFromDb);
            }


            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Remove(int cardId)
        {

            var cardFromDb = _unitOfWork.ShoppingCard.Get(u => u.Id == cardId);

            _unitOfWork.ShoppingCard.Remove(cardFromDb);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));

        }
        private double GetPriceBasedOnQuantity(ShoppingCard shoppingCard)
        {
            if (shoppingCard.Count <= 50)
            {
                return shoppingCard.Product.Price;

            }
            else
            {
                if (shoppingCard.Count <= 100)
                {
                    return shoppingCard.Product.Price50;
                }
                else
                {
                    return shoppingCard.Product.Price100;
                }

            }



        }

    }
}
