using SingletonPatternDemo.NoThreadSafe;

namespace SingletonPatternDemo;

class Program
{
    static void Main(string[] args)
    {
        Singleton fromTeacher = Singleton.GetInstance();
        fromTeacher.PrintDetails("From Teacher");

        Singleton fromStudent = Singleton.GetInstance();
        fromStudent.PrintDetails("From Student");
    }
}
