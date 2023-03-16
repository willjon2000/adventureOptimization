namespace adventureOptimization
{
    internal partial class Program
    {
        public static class RND
        {
            private static Random Rnd = new Random();
            public static int Range(int a, int b)
            {
                return Rnd.Next(a, b);
            }

        }

    }
}