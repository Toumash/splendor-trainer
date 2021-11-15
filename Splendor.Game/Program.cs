using static Splendor.Game.Tier;
using static Splendor.Game.Color;
using System.Linq;

namespace Splendor.Game
{
    public class Program
    {
        public static void Main()
        {
            var g = new GameBoard()
            {
                Deck = Deck.Cards,
                ArtistDeck = Deck.Artists,
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
        public CrystalSet Tokens { get; set; } = new CrystalSet();

        public override string ToString()
        {

            string Tier(Tier tier)
            {
                return string.Join("\n", VisibleCards.Where(c => c.Tier == tier).Select(vc => vc.ToString()));
            }
            return $"=== T1 ===\n{Tier(T1)}\n=== T2 ===\n{Tier(T2)}\n=== T3 ===\n{Tier(T3)}";
        }
    }
    public class Player
    {
        public Player(CrystalSet tokens, List<Card> cards, List<Card> reservedCards)
        {
            Tokens = tokens;
            Cards = cards;
            ReservedCards = reservedCards;
        }

        public CrystalSet Tokens { get; }
        public List<Card> Cards { get; }
        public List<Card> ReservedCards { get; }
    }
    public class Artist
    {
        public Artist(int score, CrystalSet price)
        {
            Price = price;
            Score = score;
        }

        public CrystalSet Price { get; init; }
        public int Score { get; init; }

        public override string ToString() =>
            $"Artist: {Score} {Price}";
    }
    public enum Tier { T1, T2, T3 }
    public class Card
    {
        public Card(
            Color color,
            int score,
            CrystalSet price,
            Tier tier)
        {
            Color = color;
            Score = score;
            Price = price;
            Tier = tier;
        }

        public Color Color { get; init; }
        public int Score { get; init; }
        public CrystalSet Price { get; init; }
        public Tier Tier { get; init; }

        public override string ToString() =>
             $"{Score}{Color.ToEmoji()}💲{Price}";
    }
    public class CrystalSet
    {
        public Dictionary<Color, int> _tokens = new Dictionary<Color, int>();

        public override string ToString() =>
             string.Join(" ", _tokens.Select(p => string.Concat(Enumerable.Repeat(p.Key.ToEmoji(), p.Value))).Where(s => !string.IsNullOrWhiteSpace(s)));

        public bool ContainsRequired(CrystalSet required) =>
            required.GetCostLines().All(r =>
                _tokens.ContainsKey(r.Key)
                && _tokens[r.Key] >= r.Value
            );

        public static CrystalSet New()
        {
            return new CrystalSet();
        }
        public CrystalSet Set(Color index, int value)
        {
            _tokens[index] = value;
            return this;
        }
        public int this[Color index]
        {
            get
            {
                return _tokens[index];
            }
        }

        public Dictionary<Color, int> GetCostLines()
        {
            return _tokens;
        }

        public static CrystalSet operator +(CrystalSet a, Crystal b)
        {
            var obj = new CrystalSet();
            foreach (var x in a.GetCostLines())
            {

                if (b.Color == x.Key)
                {

                    obj.Set(x.Key, a[x.Key] + 1);
                }
                else
                {
                    obj.Set(x.Key, a[x.Key]);
                }
            }
            return obj;
        }

        public static CrystalSet operator +(CrystalSet a, CrystalSet b)
        {
            var obj = new CrystalSet();
            foreach (var x in a.GetCostLines())
            {
                obj.Set(x.Key, a[x.Key] + b[x.Key]);
            }
            foreach (var x in b.GetCostLines().Where(line => !a.GetCostLines().ContainsKey(line.Key)))
            {
                obj.Set(x.Key, x.Value);
            }
            return obj;
        }
    }
    public class Crystal
    {
        public Crystal(Color color)
        {
            Color = color;
        }

        public Color Color { get; }
        public static CrystalSet operator +(Crystal a, Crystal b)
        {
            var obj = new CrystalSet();
            if (a.Color == b.Color)
            {
                obj.Set(a.Color, 2);
            }
            else
            {
                obj.Set(a.Color, 1);
                obj.Set(b.Color, 1);
            }
            return obj;

        }
        public static Crystal Green = new Crystal(Color.Green);
        public static Crystal White = new Crystal(Color.White);
        public static Crystal Red = new Crystal(Color.Red);
        public static Crystal Yellow = new Crystal(Color.Yellow);
        public static Crystal Brown = new Crystal(Color.Brown);
        public static Crystal Blue = new Crystal(Color.Blue);
    }
    public enum Color { Green, Red, Brown, Blue, White, Yellow }
    #region Extensions
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
    #endregion
    public abstract class GameAction
    {
        public GameAction(Player player, GameBoard gameBoard)
        {
            Player = player;
            GameBoard = gameBoard;
        }

        public Player Player { get; }
        public GameBoard GameBoard { get; }

        public abstract void Execute();
    }
    public class TakeTokensAction : GameAction
    {

        public TakeTokensAction(Player player, GameBoard gameBoard, params Color[] tokens) : base(player, gameBoard)
        {
            Tokens = tokens;
        }

        public Color[] Tokens { get; }

        public override void Execute()
        {
        }
    }
    public class ReservedCardAction : GameAction
    {
        public ReservedCardAction(Player player, GameBoard gameBoard) : base(player, gameBoard)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
    public class BuyCardAction : GameAction
    {
        public BuyCardAction(Player player, GameBoard gameBoard) : base(player, gameBoard)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
    public class BuyReservedCardAction : GameAction
    {
        public BuyReservedCardAction(Player player, GameBoard gameBoard) : base(player, gameBoard)
        {
        }

        public override void Execute()
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