using System;

namespace SolarBonusScheme
{
    class Program
    {
        static string[] plans = { "Sell All", "Solar Booster No Contract", "Solar Booster 1-Year Contract", "Solar Booster 2-Year Contract", "Solar Booster 3-Year Contract" };
        static double[] charges = { 0, 10, 16, 18, 20 };
        static double[,] consumptions = { { 341.131, 682.918, 399.378 }, { 179.386, 506.736, 590.159 }, { 237.101, 651.596, 691.7 }, { 336.427, 732.182, 409.419 } };
        static double[] prices = { 0.4826, 0.1933, 0.1076 };
        static double[,] generations = { { 40.18, 352.88, 0.41 }, { 8.77, 280.51, 0.15 }, { 38.94, 404.46, 1.79 }, { 89.25, 369.02, 2.15 } };
        static double[] feedintariffs = { 0.061, 0.061, 0.08, 0.10, 0.122 };

        static void Main()
        {
            double no_solar = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    no_solar += consumptions[i, j] * prices[j];
                }
            }
            Console.WriteLine(String.Format("No Solar: ${0}", no_solar));

            var cost = new double[5];
            for (int plan = 0; plan < 5; plan++)
            {
                cost[plan] = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (plan == 0)
                        {
                            cost[plan] += (consumptions[i, j] * prices[j]) - (generations[i, j] * feedintariffs[plan]);
                        }
                        else
                        {
                            var usage = consumptions[i, j] - generations[i, j];
                            if (usage > 0) // Consume more
                            {
                                cost[plan] += usage * prices[j];
                            }
                            else if (usage < 0) // Generate more
                            {
                                cost[plan] += usage * feedintariffs[plan];
                            }
                        }
                    }
                }
                cost[plan] += charges[plan] * 12;
            }

            for (int plan = 0; plan < 5; plan++)
            {
                Console.WriteLine(String.Format("{0}: ${1}", plans[plan], cost[plan]));
            }
            Console.ReadLine();
        }
    }
}