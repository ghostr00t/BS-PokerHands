using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker.Models
{
	public class CardHand
	{
		public int ID { get; set; }
		public string Value { get; set; }
		public string Suit { get; set; }
		public int Rank { get; set; }
	}
}