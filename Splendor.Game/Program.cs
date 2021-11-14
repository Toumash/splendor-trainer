using static Splendor.Game.Tier;
using static Splendor.Game.Color;

namespace Splendor.Game
{

    public class Program
    {
        public static void Main()
        {
            var deck = new List<Card>()
            {
                new Card(White, 0, new Price().Set(Blue, 1),T1),
                new Card(White, 0, new Price().Set(Green, 1),T2),
                new Card(White, 0, new Price().Set(Blue, 1),T3)
            };
            var artistDeck = new List<Artist>(){
                new Artist(3, new Price().Set(Blue,3).Set(Green,3).Set(White,3))
            };
            var g = new GameBoard()
            {
                Deck = deck,
                ArtistDeck = artistDeck,
            };
            new StartGameCommand(g).Execute();
            while (true)
            {
                Console.Clear();
                Console.Write(g);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    public class GameBoard
    {
        public List<Card> Deck { get; set; } = new List<Card>();
        public List<Card> VisibleCards { get; set; } = new List<Card>();
        public List<Artist> ArtistDeck { get; set; } = new List<Artist>();
        public List<Artist> VisibleArtistsDeck { get; set; } = new List<Artist>();

        public override string ToString()
        {

            string Tier(Tier tier)
            {
                return string.Join(",", VisibleCards.Where(c => c.Tier == tier).Select(vc => vc.ToString()));
            }
            return $"T1 🃏:[{Tier(T1)}]\nT2 🃏:[{Tier(T2)}]\nT3 🃏:[{Tier(T3)}]";
        }
    }


    public class Artist
    {
        public Artist(int score, Price price)
        {
            Price = price;
            Score = score;
        }

        public Price Price { get; init; }
        public int Score { get; init; }

        public override string ToString() =>
            $"Artist: {Score} {Price}";
    }
    public enum Tier
    {
        T1,
        T2,
        T3
    }
    public class Card
    {
        public Card(
            Color color,
            int score,
            Price price,
            Tier tier)
        {
            Color = color;
            Score = score;
            Price = price;
            Tier = tier;
        }

        public Color Color { get; init; }
        public int Score { get; init; }
        public Price Price { get; init; }
        public Tier Tier { get; init; }

        public override string ToString() =>
             $"{Color.ToEmoji()} {Tier.ToEmoji()}  | {Score}🎯 {Price}";
    }
    public class Price
    {
        public Dictionary<Color, int> _price = new Dictionary<Color, int>();

        public override string ToString() =>
             "$:" + string.Join(" ", _price.Select(p => $"{p.Value}{p.Key.ToEmoji()}"));

        public bool IsAffordable(Dictionary<Color, int> availableTokens)
        {
            foreach (var priceElement in _price)
            {
                if (!availableTokens.ContainsKey(priceElement.Key))
                {
                    return false;
                }
                if (availableTokens[priceElement.Key] < priceElement.Value)
                {
                    return false;
                }
            }
            return true;
        }
        public static Price New()
        {
            return new Price();
        }
        public Price Set(Color index, int value)
        {
            _price[index] = value;
            return this;
        }
    }
    public enum Color
    {
        Green,
        Red,
        Brown,
        Blue,
        White,
        Yellow
    }

    public static class StringExtensions
    {
        public static string ToEmoji(this Tier tier)
        {
            return tier switch
            {
                Tier.T1 => "1️⃣",
                Tier.T2 => "2️⃣",
                Tier.T3 => "3️⃣",
                _ => "???"
            };
        }
        public static string ToEmoji(this Color color)
        {
            return color switch
            {
                Color.Green => "🟩",
                Color.White => "⬜",
                Color.Blue => "🟦",
                Color.Red => "🟥",
                Color.Yellow => "🟨",
                Color.Brown => "🟫",
                _ => "???"
            };
        }
    }

    public interface GameAction
    {
        void Execute();
    }
    public class PassAction : GameAction
    {
        public PassAction() { }
        public void Execute() { }
    }
    public class TakeTokensAction : GameAction
    {

        public TakeTokensAction(params Color[] tokens)
        {
            Tokens = tokens;
        }

        public Color[] Tokens { get; }

        public void Execute()
        {

        }
    }
    public class ReservedCardAction : GameAction
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
    public class BuyCardAction : GameAction
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
    public class BuyReservedCardAction : GameAction
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class StartGameCommand
    {
        public StartGameCommand(GameBoard gameBoard)
        {
            GameBoard = gameBoard;
        }

        public GameBoard GameBoard { get; }

        public void Execute()
        {
            var tier1 = GameBoard.Deck.Where(c => c.Tier == T1).Take(4);
            var tier2 = GameBoard.Deck.Where(c => c.Tier == T2).Take(4);
            var tier3 = GameBoard.Deck.Where(c => c.Tier == T3).Take(4);
            GameBoard.VisibleCards = new[] { tier1, tier2, tier3 }.SelectMany(l => l).ToList();
        }
    }
}