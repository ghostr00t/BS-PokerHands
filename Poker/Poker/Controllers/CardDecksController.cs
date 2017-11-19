using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Poker.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;

namespace Poker.Controllers
{
	public class CardDecksController : Controller
	{
		private TravieIOEntities db = new TravieIOEntities();

		public JsonResult GetCards()
		{			
			IList<CardDeck> cards = db.CardDecks.ToList();
			Shuffle(cards);
			IList<CardDeck> hand = cards.Take(5).ToList();
			hand = hand.OrderBy(h => h.Rank).ToList();
			string TheHand = FindHands(hand);
			var obj = MakeTuple(hand, TheHand);
			return Json(obj,  JsonRequestBehavior.AllowGet);
		}

		static Tuple<IList<CardDeck>, string> MakeTuple(IList<CardDeck> hand, string TheHand)
		{
			return new Tuple<IList<CardDeck>, string>(hand, TheHand);
		}

		public static void Shuffle<T>(IList<T> cards)
		{
			var random = new Random();
			for (int i = cards.Count - 1; i >= 1; i--)
			{
				int other = random.Next(0, i + 1);
				T temp = cards[i];
				cards[i] = cards[other];
				cards[other] = temp;
			}
		}
		
		public string FindHands(IList<CardDeck> hand)
		{
			string HandName = "";

			//Royal Flush
			if (hand[0].Value == "10" && hand[1].Value == "J" && hand[2].Value == "Q" && hand[3].Value == "K" && hand[4].Value == "A")
			{
				 HandName = "You got a royal flush.";
			}

			//Straight Flush
			else if (hand.GroupBy(c => c.Suit).Count() == 1 && hand[0].Rank + 1 == hand[1].Rank && hand[1].Rank + 1 == hand[2].Rank && hand[2].Rank + 1 == hand[3].Rank && hand[3].Rank + 1 == hand[4].Rank)
			{
				HandName = "You got A straight flush.";
			}
			
			//4 of a Kind
			else if (hand.GroupBy(c => c.Suit).Count() == 4 && hand.GroupBy(c => c.Value).Count() == 2)
			{
				HandName = "You got four of a kind.";
			}

			//Full House
			else if (hand.GroupBy(c => c.Value).Count() == 2 && hand.GroupBy(c => c.Value).Any(g => g.Count() == 3))
			{
				HandName = "You got a full house.";
			}

			//Flush
			else if (hand.GroupBy(c => c.Suit).Count() == 1)
			{
				HandName = "You got a flush.";
			}

			//Straight
			else if(hand[0].Rank + 1 == hand[1].Rank && hand[1].Rank + 1 == hand[2].Rank && hand[2].Rank + 1 == hand[3].Rank && hand[3].Rank + 1 == hand[4].Rank)
			{
				HandName = "You got a straight.";
			}

			//Three of a Kind
			else if (hand.GroupBy(c => c.Value).Count() == 3 && hand.GroupBy(c => c.Value).Any(g => g.Count() == 3))
			{
				HandName = "You got three of a kind.";
			}

			//Two Pair
			else if (hand.GroupBy(c => c.Value).Count() == 3)
			{
				HandName = "You got two pairs.";
			}


			//Pair
			else if (hand.GroupBy(c => c.Value).Count() == 4)
			{
				HandName = "You got a pair.";
			}

			//High Card
			else
			{
				HandName = "Your high card is the " + hand[4].Value + " of " + hand[4].Suit + ".";
			}

			return HandName; 
		}
	}
}
