using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicLib
{
    internal class Program
    {
        private static FuzzyInput health;
        private static FuzzyInput ammo;

        private static FuzzyDataHandler handler;

        private static void Main(string[] args)
        {
            FuzzyRuleSet healthRuleSet = new FuzzyRuleSet();
            FuzzyRuleSet ammoRuleSet = new FuzzyRuleSet();
            health = new FuzzyInput(0, 100);
            ammo = new FuzzyInput(0, 100);

            healthRuleSet.AddRule(new FuzzyRule(new Point(0, 0), new Point(0, 1), new Point(30, 0)));
            healthRuleSet.AddRule(new FuzzyRule(new Point(20, 0), new Point(50, 1), new Point(80, 0)));
            healthRuleSet.AddRule(new FuzzyRule(new Point(70, 0), new Point(100, 1), new Point(100, 0)));

            ammoRuleSet.AddRule(new FuzzyRule(new Point(0, 0), new Point(0, 1), new Point(25, 1), new Point(50, 0)));
            ammoRuleSet.AddRule(new FuzzyRule(new PointF(0, 0), new PointF(50, 0.5f), new PointF(100, 0.5f), new PointF(100, 0)));
            ammoRuleSet.AddRule(new FuzzyRule(new Point(25, 0), new Point(50, 1), new Point(75, 0)));

            handler = new FuzzyDataHandler();
            handler.Add(ammo, ammoRuleSet);
            //handler.Add(health, healthRuleSet);

            Test(90);

            Console.ReadKey();
        }

        private static void Test(float value)
        {
            ammo.Value = value;
            Console.WriteLine("Result: " + handler.Evaluate());
        }
    }
}