using System;

namespace SolarBonusScheme
{
    class Program
    {
        static string[] plan = { "Status Quo", "Solar Booster Basic", "Solar Booster 1-Year Plan", "Solar Booster 2-Year Plan", "Solar Booster 3-Year Plan" };
        static double[] charge = { 0, 10, 16, 18, 20 };
        static double[,] consumption = { { 341.131, 682.918, 399.378 }, { 179.386, 506.736, 590.159 }, { 237.101, 651.596, 691.7 }, { 336.427, 732.182, 409.419 } };
        static double[] price = { 0.4826, 0.1933, 0.1076 };
        static double[,] generation = { { 40.18, 352.88, 0.41 }, { 8.77, 280.51, 0.15 }, { 38.94, 404.46, 1.79 }, { 89.25, 369.02, 2.15 } };
        static double[] feedintariff = { 0.061, 0.061, 0.08, 0.1, 0.122 };

        static void Main()
        {
            double no_solar = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    no_solar += consumption[i, j] * price[j];
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
                            cost[plan] += (consumption[i, j] * price[j]) - (generation[i, j] * feedintariff[plan]);
                        }
                        else
                        {
                            var usage = consumption[i, j] - generation[i, j];
                            if (usage >= 0)
                            {
                                cost[plan] += usage * price[j];
                            }
                            else
                            {
                                cost[plan] += -usage * feedintariff[plan];
                            }
                        }
                    }
                }
                cost[plan] += charge[plan] * 12;
            }

            for (int p = 0; p < 5; p++)
            {
                Console.WriteLine(String.Format("{0}: ${1}", plan[p], cost[p]));
            }
            Console.ReadLine();
        }
    }
}