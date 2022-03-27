using System;

namespace DesignPatterns
{
    public class Singleton
    {
        public string UserName { get; set; }
        private static Singleton singleton;
        public static Singleton Instance
        {
            get {
                if(singleton == null)
                {
                    singleton = new Singleton();
                }
                return singleton;
            }
            private set { }
        }
        private Singleton() { }
    }

    public class Prototype
    {
        private int denumerator;
        public int number;
        public Prototype(int Number)
        {
            this.number = Number;
            denumerator = new Random().Next(10, 250);
        }

        public string devide() => (number/denumerator).ToString();

        public Prototype clone()
        {
            Prototype cloneObj = new Prototype(this.number);
            cloneObj.denumerator = this.denumerator;
            return cloneObj;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Singleton singletonObject1 = Singleton.Instance;
            singletonObject1.UserName = "Nafis Sadik";
            Console.WriteLine("Singleton Object 1 : " + singletonObject1.UserName);

            Singleton singletonObject2 = Singleton.Instance;
            Console.WriteLine("Singleton Object 2 : " + singletonObject2.UserName);
            singletonObject2.UserName = "Raju vai";

            Console.WriteLine("Singleton Object 1 : " + singletonObject1.UserName);
            Console.WriteLine("Singleton Object 2 : " + singletonObject2.UserName);

            Prototype prototype = new Prototype(99999999);
            Console.WriteLine("Output # 1 : " + prototype.devide());
            Prototype prototypeClone = prototype.clone();
            Console.WriteLine("Output # 2 : " + prototypeClone.devide());
        }
    }
}

// Start from Facade design pattern