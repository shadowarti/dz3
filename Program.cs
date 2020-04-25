using System;
using System.Collections.Generic;

namespace dz3
{
    class Program
    {
        static void Main(string[] args)
        {
            Tools t = new Tools();
            Unit fittest=new Unit(0,t.conv_to2(0),0);
            int fit_iter = 0;
            int stop = 200; //якщо індивід залиється найкращим протягом stop кількості поколінь, алгоритм зупяє роботу
            List<Unit> first_gen = new List<Unit>();

            for (int i=0; i<20;i++)
            {
                int a = t.gen_16();
                string b = t.conv_to2(a);
                int c = t.count_one(b);
                first_gen.Add(new Unit(a,b,c));
            }

            fittest = t.find_fit(first_gen,fittest);
            int generation = 0;
            Console.WriteLine("Generetaion #"+generation);

            while (fit_iter <stop)
            {
                for (int i = 0; i < first_gen.Count; i++) //обрання партнера
                {
                    first_gen[i].partner = t.gen(first_gen.Count);
                }

                t.generate_next(first_gen); //створити нову популяцію

                Unit new_fit = t.find_fit(first_gen,fittest);
                if (fittest.value == new_fit.value)
                {
                    fit_iter++;
                }
                else
                {
                    fit_iter = 0;
                    fittest = new_fit;
                }
                generation++;
                Console.WriteLine("Generetaion #" + generation);
            }

            Console.WriteLine("Best unit was found on generation# "+(generation-stop)+" with value of "+fittest.value);
            Console.ReadKey();
        }
    }
}
