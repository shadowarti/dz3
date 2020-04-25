using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace dz3
{
    class Tools
    {
        Random n = new Random();
        public int gen(int cut) //генерує випадкове число на проміжку (0,cut)
        {
            return n.Next(0, cut);
        }
        public int gen_16() //генерує випадкове 16-бітне число
        {
            return n.Next(32768, 65535);
        }
        public string conv_to2(int a) //переведення числа у двійкове
        {
            return Convert.ToString(a, 2);
        }
        public int count_one(string a) //пдірахунок кількості 1
        {
            return Regex.Matches(a, "1").Count;
        }
        public Unit find_fit(List<Unit> a, Unit best) //знайти найбільш пристосованого індивіда
        {
            int b = best.value;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].value > b)
                {
                    b = a[i].value;
                }
            }
            string c = conv_to2(b);
            return new Unit(b, c, count_one(c));
        }
        public int conv_toint(string b) //переведення з двійкової в десяткову
        {
            int converted = 0;
            int modifier = 1;
            for (int i = 15; i >= 0; i--)
            {
                if (b[i] == '1')
                {
                    converted += modifier;
                }
                modifier *= 2;
            }
            return converted;
        }
        public void generate_next(List<Unit> a) //створити нову популяцію
        {
            List<Unit> children = new List<Unit>();
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].partner != i) //схрещування, якщо індвидід не є сам собі партнером
                {
                    int cut = gen(16);// генерація випадкового числа(індекс), де хромосоми х-батька заміняються на хромосоми y-батька
                    StringBuilder first_child = new StringBuilder();
                    StringBuilder second_child = new StringBuilder();
                    for (int j = 0; j < 16; j++)
                    {
                        if (j < cut)
                        {
                            first_child.Append(a[i].binary[j]);
                            second_child.Append(a[a[i].partner].binary[j]);
                        }
                        else
                        {
                            first_child.Append(a[a[i].partner].binary[j]);
                            second_child.Append(a[i].binary[j]);
                        }
                    }
                    //мутація
                    first_child = mutate(first_child);
                    second_child = mutate(second_child);

                    string b = first_child.ToString();
                    int x = conv_toint(b);
                    int c = count_one(b);
                    children.Add(new Unit(x, b, c));

                    b = second_child.ToString();
                    x = conv_toint(b);
                    c = count_one(b);
                    children.Add(new Unit(x, b, c));
                }
            }
            for (int i = 0; i < children.Count; i++)
            {
                a.Add(children[i]);
            }
            a = choose_next(a);
        }
        public StringBuilder mutate(StringBuilder a) //мутація випадкової хромосоми з шансом 5%
        {
            if (gen(100) < 5)
            {
                int c = gen(16);
                if (a[c] == 0)
                {
                    a[c] = Convert.ToChar(1);
                }
                else
                {
                    a[c] = Convert.ToChar(0);
                }
            }
            return a;
        }
        public List<Unit> choose_next(List<Unit> a) //обирає індивідів для нової популяції
        {
            List<Unit> b = new List<Unit>();
            a.Sort(delegate (Unit x, Unit y) { return x.count1.CompareTo(y.count1); });
            a.Reverse();
            for (int i = a.Count - 1; i >= 20; i--)
            {
                a.RemoveAt(i);
            }

            for (int i = 0; i < a.Count; i++)
            {
                int ptr = gen(a.Count);
                b.Add(a[ptr]);
            }
            return b;
        }
    }
}
