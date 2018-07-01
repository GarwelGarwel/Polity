namespace Polity
{
    public class Date
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public override string ToString() => Year + "-" + Month.ToString("D2") + "-" + Day.ToString("D2");

        public void NextTurn()
        {
            if (Day < 30) Day++;
            else
            {
                Day = 1;
                if (Month < 12) Month++;
                else
                {
                    Month = 1;
                    Year++;
                }
            }
        }

        public Date() { }

        public Date(int y, int m, int d)
        {
            Year = y;
            Month = m;
            Day = d;
        }
    }
}
