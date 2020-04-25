namespace dz3
{
    class Unit
    {
        public int partner { get; set; } //індекс партнера
        public int value { get; set; } //число
        public string binary { get; set; } //двійкове представлення числа
        public int count1 { get; set; } //кількість 1 в двійковому представленні

        public Unit(int a, string b, int c)
        {
            value = a;
            binary = b;
            count1 = c;
        }
    }
}
