using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricSystem
{
   

    class App
    {
        static void Main(string[] args)
        {
            var generator = new PowerSource("Generator", 100, 10.5, 0.85);
            generator.PrintSpecs();
            var invertor = new PowerSource("Inverter", 20, 0.69, 0.6);
            invertor.PrintSpecs();
            generator.SetRealPower(90);
            invertor.SetRealPower(50);
        }
    }

    public class PowerSource
    {
        public string Name;
        private double _ratedRealPower;
        private double _ratedPowerFactor;
        private double _ratedVoltage;
        private double _realPowerOutput;
        private double _reactivePowerOutput;

        public PowerSource(string name, double ratedRealPower, double ratedVoltage, double ratedPowerFactor)
        {
            Name = name;
            _ratedRealPower = ratedRealPower;
            _ratedVoltage = ratedVoltage;
            if (ratedPowerFactor >= 0.8 && ratedPowerFactor <= 1)
                _ratedPowerFactor = ratedPowerFactor;
            else
            {
                Console.WriteLine($"\n!Введеннон значение коэффицента мощности {ratedPowerFactor} некорректно. Принятно 1 для источник {name}!\n");
                _ratedPowerFactor = 1;
            }
        }
        public double GetRatedQ()
        {
            return _ratedRealPower * Math.Tan(Math.Acos(_ratedPowerFactor));
        }
        public void PrintSpecs()
        {
            Console.WriteLine($"Номинальные параметры источника \"{Name}\":");
            Console.WriteLine($"Номинальная активная мощность: {_ratedRealPower} МВт");
            Console.WriteLine($"Номинальная реактивная мощность: {GetRatedQ()} Мвар");
            Console.WriteLine($"Номинальное напряжение: {_ratedVoltage} кВ");
        }
        public double GetRealPower() { return _realPowerOutput; }
        public double GetReactivePower() { return _reactivePowerOutput; }

        public void SetRealPower(double realPower)
        {
            if (realPower < 0 || realPower > _ratedRealPower)
            {
                Console.WriteLine("Задаваемое значение активной мощности превышает номинал");
                return;
            }
            else _realPowerOutput = realPower;
        }
        public void SetReactivePower(double reactivePower)
        {
            var ratedQ = GetRatedQ();
            if (reactivePower > ratedQ || reactivePower < -ratedQ)
            {
                Console.WriteLine("Задаваемое значение реактивной мощности превышает номинал");
                return;
            }
            else _reactivePowerOutput = reactivePower;
        }

    }
}
