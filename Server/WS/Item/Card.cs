namespace Server.WS.Item
{
    /// <summary>
    /// Informations sur une carte
    /// </summary>
    public class Card
    {
        public decimal PointValue;
        public int SortOrder;
        public string Name;
        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Card Get(string name)
        {
            Card c = new Card
            {
                Name = name
            };
            if (name.Contains("valet"))
            {
                c.PointValue = 1.5m;
            }
            else if (name.Contains("cavalier"))
            {
                c.PointValue = 2.5m;
            }
            else if (name.Contains("dame"))
            {
                c.PointValue = .35m;
            }
            else if (name.Contains("roi"))
            {
                c.PointValue = 4.5m;
            }
            else if (name.Contains("atout") && (!name.Contains("1") || !name.Contains("21") || !name.Contains("excuse")))
            {
                c.PointValue = new decimal(0.5);
            }
            else if (name.Contains("1") || name.Contains("21") || name.Contains("excuse"))
            {
                c.PointValue = 4.5m;
            }
            else
            {
                c.PointValue = new decimal(0.5);
            }
            return null;
        }
    }
}