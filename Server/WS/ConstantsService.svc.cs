using System;
using System.Collections.Generic;
using Server.WS.Item;

namespace Server.WS
{
    /// <summary>
    /// Permet pour le moment de:
    /// *récupérer des cartes dans un ordre random ou non
    /// *mélanger un paquet de cartes
    /// *générer un paquet de tarot
    /// </summary>
    public class ConstantsService : IConstantsService
    {
        public List<Card> GetCards(CardsType typeOfCards, bool isRandomOrder)
        {
            List<Card> cards = new List<Card>();
            switch (typeOfCards)
            {
                case CardsType.Tarot:
                    GenerateTarot(ref cards);
                    break;
            }
            if (isRandomOrder)
            {
                Randomize(ref cards);
            }
            return cards;
        }

        /// <summary>
        /// Permet de renvoyer une liste flushé en random triple.
        /// </summary>
        /// <param name="cards"></param>
        private void Randomize(ref List<Card> cards)
        {
            Random rand1=new Random(DateTime.Now.Millisecond+DateTime.Now.Second);
            List<Card> flushed=new List<Card>(cards);
            foreach (Card card in cards)
            {
                int randomI = rand1.Next(1,cards.Count - 1);
                if (flushed[randomI] != null)
                {
                    randomI = rand1.Next(1, cards.Count - 1);
                    if (flushed[randomI] != null)
                    {
                        randomI = rand1.Next(1, cards.Count - 1);
                        if (flushed[randomI] != null)
                        {
                            randomI++;
                            if (flushed[randomI] != null)
                            {
                                randomI=randomI-2;
                            }
                        }
                    }
                }
                flushed[randomI] = card;
                //cards.Remove(card);
            }
            cards = flushed;
        }

        /// <summary>
        /// Créer un paquet de tarot.
        /// </summary>
        /// <param name="cards"></param>
        private void GenerateTarot(ref List<Card> cards)
        {
            cards.Add(new Card
            {
                Name = "Excuse",
                SortOrder = 78,
                PointValue = 4.5m
            });
            for (int i = 1; i <= 21; i++)
            {
                Card c = new Card
                             {
                                 Name = "Atout " + i,
                                 SortOrder = i,
                                 PointValue = 0.5m
                             };
                if (i == 1 || i == 21)
                {
                    c.PointValue = 4.5m;
                }
                cards.Add(c);
            }
            for (int outerIndex = 1; outerIndex <= 4; outerIndex++)
            {
                string currentNameZone = "";
                switch (outerIndex)
                {
                    case 1:
                        currentNameZone = "coeur ";
                        break;

                    case 2:
                        currentNameZone = "carreaux ";
                        break;

                    case 3:
                        currentNameZone = "trèfle ";
                        break;

                    case 4:
                        currentNameZone = "pique ";
                        break;

                }
                for (int i = 0; i < 14; i++)
                {
                    Card c = new Card
                    {
                        Name = currentNameZone + i,
                        SortOrder = i,
                        PointValue = 0.5m
                    };
                    if (i > 10)
                    {
                        switch (i)
                        {
                            case 11:
                                c.Name = currentNameZone + "valet";
                                c.PointValue = 1.5m;
                                break;
                            case 12:
                                c.Name = currentNameZone + "cavalier";
                                c.PointValue = 2.5m;
                                break;
                            case 13:
                                c.Name = currentNameZone + "dame";
                                c.PointValue = 3.5m;
                                break;
                            case 14:
                                c.Name = currentNameZone + "roi";
                                c.PointValue = 4.5m;
                                break;
                        }
                    }
                    cards.Add(c);
                }
            }
        }
    }
}
