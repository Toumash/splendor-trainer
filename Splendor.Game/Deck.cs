using static Splendor.Game.Tier;
using static Splendor.Game.Color;

namespace Splendor.Game
{
    class Deck
    {
        private static Card Card(Tier tier, Color color, int score, int white, int blue, int green, int red, int brown)
        {
            var set = new CrystalSet();
            set.Set(Color.White, white);
            set.Set(Color.Blue, blue);
            set.Set(Color.Green, green);
            set.Set(Color.Red, red);
            set.Set(Color.Brown, brown);
            return new Card(color, score, set, tier);
        }

        private static Artist Noble(int score, int white, int blue, int green, int red, int brown)
        {
            return new Artist(3, CrystalSet.New()
                    .Set(Blue, blue)
                    .Set(Green, green)
                    .Set(White, white)
                    .Set(Red, red)
                    .Set(Brown, brown)
                );
        }
        public static List<Artist> Artists = new List<Artist>(){
            Noble(3, 0, 0, 0, 4, 4),
            Noble(3, 0, 0, 3, 3, 3),
            Noble(3, 0, 4, 4, 0, 0),
            Noble(3, 4, 4, 0, 0, 0),
            Noble(3, 3, 3, 0, 0, 3),
            Noble(3, 0, 0, 4, 4, 0),
            Noble(3, 0, 3, 3, 3, 0),
            Noble(3, 4, 0, 0, 0, 4),
            Noble(3, 3, 3, 3, 0, 0),
            Noble(3, 3, 0, 0, 3, 3),
        };

        public static List<Card> Cards = new List<Card>()
            {
                Card(Tier.T1,Brown,0,1,1,1,1,0),
                Card(Tier.T1,Brown,0,1,2,1,1,0),
                Card(Tier.T1,Brown,0,2,2,0,1,0),
                Card(Tier.T1,Brown,0,0,0,1,3,1),
                Card(Tier.T1,Brown,0,0,0,2,1,0),
                Card(Tier.T1,Brown,0,2,0,2,0,0),
                Card(Tier.T1,Brown,0,0,0,3,0,0),
                Card(Tier.T1,Brown,1,0,4,0,0,0),
                Card(Tier.T1,Blue,0,1,0,1,1,1),
                Card(Tier.T1,Blue,0,1,0,1,2,1),
                Card(Tier.T1,Blue,0,1,0,2,2,0),
                Card(Tier.T1,Blue,0,0,1,3,1,0),
                Card(Tier.T1,Blue,0,1,0,0,0,2),
                Card(Tier.T1,Blue,0,0,0,2,0,2),
                Card(Tier.T1,Blue,0,0,0,0,0,3),
                Card(Tier.T1,Blue,1,0,0,0,4,0),
                Card(Tier.T1,White,0,0,1,1,1,1),
                Card(Tier.T1,White,0,0,1,2,1,1),
                Card(Tier.T1,White,0,0,2,2,0,1),
                Card(Tier.T1,White,0,3,1,0,0,1),
                Card(Tier.T1,White,0,0,0,0,2,1),
                Card(Tier.T1,White,0,0,2,0,0,2),
                Card(Tier.T1,White,0,0,3,0,0,0),
                Card(Tier.T1,White,1,0,0,4,0,0),
                Card(Tier.T1,Green,0,1,1,0,1,1),
                Card(Tier.T1,Green,0,1,1,0,1,2),
                Card(Tier.T1,Green,0,0,1,0,2,2),
                Card(Tier.T1,Green,0,1,3,1,0,0),
                Card(Tier.T1,Green,0,2,1,0,0,0),
                Card(Tier.T1,Green,0,0,2,0,2,0),
                Card(Tier.T1,Green,0,0,0,0,3,0),
                Card(Tier.T1,Green,1,0,0,0,0,4),
                Card(Tier.T1,Red,0,1,1,1,0,1),
                Card(Tier.T1,Red,0,2,1,1,0,1),
                Card(Tier.T1,Red,0,2,0,1,0,2),
                Card(Tier.T1,Red,0,1,0,0,1,3),
                Card(Tier.T1,Red,0,0,2,1,0,0),
                Card(Tier.T1,Red,0,2,0,0,2,0),
                Card(Tier.T1,Red,0,3,0,0,0,0),
                Card(Tier.T1,Red,1,4,0,0,0,0),
                Card(Tier.T2,Brown,1,3,2,2,0,0),
                Card(Tier.T2,Brown,1,3,0,3,0,2),
                Card(Tier.T2,Brown,2,0,1,4,2,0),
                Card(Tier.T2,Brown,2,0,0,5,3,0),
                Card(Tier.T2,Brown,2,5,0,0,0,0),
                Card(Tier.T2,Brown,3,0,0,0,0,6),
                Card(Tier.T2,Blue,1,0,2,2,3,0),
                Card(Tier.T2,Blue,1,0,2,3,0,3),
                Card(Tier.T2,Blue,2,5,3,0,0,0),
                Card(Tier.T2,Blue,2,2,0,0,1,4),
                Card(Tier.T2,Blue,2,0,5,0,0,0),
                Card(Tier.T2,Blue,3,0,6,0,0,0),
                Card(Tier.T2,White,1,0,0,3,2,2),
                Card(Tier.T2,White,1,2,3,0,3,0),
                Card(Tier.T2,White,2,0,0,1,4,2),
                Card(Tier.T2,White,2,0,0,0,5,3),
                Card(Tier.T2,White,2,0,0,0,5,0),
                Card(Tier.T2,White,3,6,0,0,0,0),
                Card(Tier.T2,Green,1,3,0,2,3,0),
                Card(Tier.T2,Green,1,2,3,0,0,2),
                Card(Tier.T2,Green,2,4,2,0,0,1),
                Card(Tier.T2,Green,2,0,5,3,0,0),
                Card(Tier.T2,Green,2,0,0,5,0,0),
                Card(Tier.T2,Green,3,0,0,6,0,0),
                Card(Tier.T2,Red,1,2,0,0,2,3),
                Card(Tier.T2,Red,1,0,3,0,2,3),
                Card(Tier.T2,Red,2,1,4,2,0,0),
                Card(Tier.T2,Red,2,3,0,0,0,5),
                Card(Tier.T2,Red,2,0,0,0,0,5),
                Card(Tier.T2,Red,3,0,0,0,6,0),
                Card(Tier.T3,Brown,3,3,3,5,3,0),
                Card(Tier.T3,Brown,4,0,0,0,7,0),
                Card(Tier.T3,Brown,4,0,0,3,6,3),
                Card(Tier.T3,Brown,5,0,0,0,7,3),
                Card(Tier.T3,Blue,3,3,0,3,3,5),
                Card(Tier.T3,Blue,4,7,0,0,0,0),
                Card(Tier.T3,Blue,4,6,3,0,0,3),
                Card(Tier.T3,Blue,5,7,3,0,0,0),
                Card(Tier.T3,White,3,0,3,3,5,3),
                Card(Tier.T3,White,4,0,0,0,0,7),
                Card(Tier.T3,White,4,3,0,0,3,6),
                Card(Tier.T3,White,5,3,0,0,0,7),
                Card(Tier.T3,Green,3,5,3,0,3,3),
                Card(Tier.T3,Green,4,0,7,0,0,0),
                Card(Tier.T3,Green,4,3,6,3,0,0),
                Card(Tier.T3,Green,5,0,7,3,0,0),
                Card(Tier.T3,Red,3,3,5,3,0,3),
                Card(Tier.T3,Red,4,0,0,7,0,0),
                Card(Tier.T3,Red,4,0,3,6,3,0),
                Card(Tier.T3,Red,5,0,0,7,3,0),
            };

    }
}