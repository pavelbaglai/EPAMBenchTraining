using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Client
    {
        private double Happiness { get; set; }
        private string Name { get; }
        public Client(string name, double happiness)
        {
            Name = name;
            Happiness = happiness;
        }
        public void Eat(IFood food)
        {
            Console.WriteLine($"Client: Starting to eat food, client: {this}, food: {food}");
            Happiness = food.CalculateHappiness(Happiness);
            Console.WriteLine("Client: Csam csam nyam nyam");
            Console.WriteLine($"Client: Food eaten, client: {this}");
        }

        public override string ToString()
        {
            return $"Client [name={Name}, happiness={Happiness}]";
        }
    }
}
