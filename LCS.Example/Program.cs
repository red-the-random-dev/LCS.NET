/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 14:23
 */
using System;
using LCS.Conductors;

namespace LCS.Example
{
	class Program
	{
		public static void EventConductor_On(Object sender, LCSEventArgs e)
		{
			Console.WriteLine("Conductor is on!");
		}
		public static void EventConductor_Pow(Object sender, LCSEventArgs e)
		{
			Console.WriteLine("Voltage: {0}, Current: {1}", e.Voltage, e.Current);
		}
		public static void EventConductor_Off(Object sender, LCSEventArgs e)
		{
			Console.WriteLine("Conductor is off!");
		}
		
		public static void Main(string[] args)
		{
			Emitter em = new Emitter(5.0f);
			Wire w = new Wire(1.0f);
			Delay d = new Delay(250);
			
			EventConductor ec = new EventConductor();
			ec.TurnedOn += EventConductor_On;
			ec.TurnedOff += EventConductor_Off;
			ec.Powered += EventConductor_Pow;
			
			em.Link(0, d, 0);
			d.Link(0, w, 0);
			w.Link(0, ec, 0);
			
			em.FlipOn(1.0f);
			em.FlipOn(1.5f);
			em.FlipOff();
			em.FlipOn(1.0f);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}