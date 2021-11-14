using static Splendor.Game.Tier;
using static Splendor.Game.Color;

namespace Splendor.Game
{

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var deck = new List<Card>()
        {
        new Card(White, 0,new Price().Set(Blue, 1),T1)
        };
            var artistDeck = new List<Artist>(){
            new Artist(3, new Price().Set(Blue,3).Set(Green,3).Set(White,3))
        };
            var g = new GameBoard(deck, artistDeck);
            System.Console.WriteLine(g);
        }
    }

    public class GameBoard
    {
        public List<Card> Deck { get; init; }
        public List<Card> VisibleCards { get; private set; }
        public List<Artist> ArtistDeck { get; init; }

        public GameBoard(List<Card> deck, List<Artist> artistDeck)
        {
            Deck = deck;
            ArtistDeck = artistDeck;
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
        T2
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
             $"{Tier}s:{Score} c:{Color} p:{Price}";
    }
    public class Price
    {
        public Dictionary<Color, int> _price = new Dictionary<Color, int>();

        public override string ToString() =>
             "$:" + string.Join(" ", _price.Select(p => $"{p.Key}{p.Value}"));

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
}