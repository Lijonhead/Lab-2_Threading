namespace MoreThreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car("The Hare ");
            Car car2 = new Car("The tortoise ");

            Thread car1Thread = new Thread(car1.Drive);
            Thread car2Thread = new Thread(car2.Drive);

            Console.WriteLine("Starting the race! \nInput 'status' to see the status of the cars");
            car1Thread.Start();
            car2Thread.Start();

            car1Thread.Join();
            car2Thread.Join();
            while (!car1.Finished || !car2.Finished)
            {
                if (Console.ReadLine().ToLower() == "status")
                {
                    Console.WriteLine($"Status update:");
                    Console.WriteLine($"{car1.Name}: {car1.Distance} km, Speed: {car1.Speed} km/h");
                    Console.WriteLine($"{car2.Name}: {car2.Distance} km, Speed: {car2.Speed} km/h");
                }
            }

            Console.WriteLine($"{(car1.Distance > car2.Distance ? car1.Name : car2.Name)} won the race!");
        }
    }
    

    public class Car
    {
        public string Name { get; set; }
        public double Distance { get; set; } = 0;
        public double Speed { get; set; } = 120;
        public bool Finished { get; set; } = false;

        public Car(string name)
        {
            Name = name;
        }

        public void Drive()
        {
            while (Distance < 10)
            {
                
                Distance += Speed * 0.5 / 3600; 

                
                if (DateTime.Now.Second % 30 == 0)
                {
                    HandleRandomEvent();
                    Thread.Sleep(500);
                }

                Thread.Sleep(500); 
            }
            Finished = true;
        }

        private void HandleRandomEvent()
        {
            Random rand = new Random();
            double probability = rand.NextDouble();

            if (probability <= 0.02) 
            {
                Console.WriteLine($"{Name} has a puncture! Stopping for 20 seconds.");
                Thread.Sleep(20000); 
            }
            else if (probability <= 0.1) 
            {
                Console.WriteLine($"{Name} has engine failure! Reducing speed by 1km/h.");
                Speed -= 1;
            }
            else if (probability <= 0.2) 
            {
                Console.WriteLine($"{Name} has a bird on the windshield! Stopping for 10 seconds.");
                Thread.Sleep(10000); 
            }
            else if (probability <= 0.04) 
            {
                Console.WriteLine($"{Name} is out of gas! Stopping for 30 seconds.");
                Thread.Sleep(30000); 
            }
        }
    }
}
