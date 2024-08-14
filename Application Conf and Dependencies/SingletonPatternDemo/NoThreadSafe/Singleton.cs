namespace SingletonPatternDemo.NoThreadSafe
{
    public sealed class Singleton
    {
        private static int counter = 0;

        private static Singleton? Instance;

        public static Singleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Singleton();
            }

            return Instance;
        }

        private Singleton()
        {
            counter++;
            Console.WriteLine($"Counter value: {counter}");
        }

        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }
    }
}