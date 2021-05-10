using System;
using System.Collections.Generic;

namespace bomberman
{
    public enum Prize
    {
        empty, 
     //   bombPlus,
     //   bombaMinus, 
        firePlus,
        fireMinus,
        speedPlus,
        speedMinus
    }
    public static class BonusClass //использовать из других скриптов 
    {
        static Dictionary<Prize, int> percant;
        static List<Prize> listBonus;
        static Random rand = new Random();
        static int kolBonus = 4; 
        public static void Prepare()
        {
            PreparePercent();
            PrepareBonus();
        }
        private static void PreparePercent()
        {
           percant = new Dictionary<Prize, int>();
            percant.Add(Prize.firePlus, 80);
            percant.Add(Prize.fireMinus, 20);
            percant.Add(Prize.speedPlus, 60);
            percant.Add(Prize.speedMinus, 10); 
        }

        private static void PrepareBonus()
        {
            listBonus = new List<Prize>();
            int sum = 0;
            foreach(int item in percant.Values) //полуаем проценты 
            {
                sum += item; 
            }
            do
            {
                int nomBonus = rand.Next(0, sum);
                int tBonus = 0;
                foreach (Prize prize in percant.Keys)
                {
                    tBonus += percant[prize]; 
                    if(nomBonus < tBonus)
                    {
                        listBonus.Add(prize);
                        break; 
                    }
                }
            } while (listBonus.Count < kolBonus);
        }
        public static Prize GetBonus()
        {
            if (listBonus.Count == 0) return Prize.empty;
            Prize prize = listBonus[0];
            listBonus.Remove(prize);
            return prize; 
        }

    }
}
