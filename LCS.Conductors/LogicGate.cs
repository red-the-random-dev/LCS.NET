/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 15:23
 */
using System;

namespace LCS.Conductors
{
	public delegate Boolean LogicGateBase(Boolean a, Boolean b);
	
	/// <summary>
	/// Customizable logic gate with two inputs.
	/// </summary>
	public class LogicGate : Conductor
	{
		public static Boolean AND(Boolean a, Boolean b)
		{
			return (a && b);
		}
		
		public static Boolean OR(Boolean a, Boolean b)
		{
			return (a || b);
		}
		
		public static Boolean XOR(Boolean a, Boolean b)
		{
			return (a != b);
		}
		
		public static Boolean Impl(Boolean a, Boolean b)
		{
			return (a || !b);
		}
		
		public LogicGate(LogicGateBase condition)
		{
			Condition = condition;
			Connections = new System.Collections.Generic.List<LinkTarget>[1];
			Connections[0] = new System.Collections.Generic.List<LinkTarget>();
		}
		
		public override void ReceiveSignal(int channel, float voltage, float current)
		{
			Single intensity = 0.0f;
			if (voltage != 0)
			{
				intensity = current / voltage;
			}
			
			switch (channel)
			{
				case 0:
					PinACurrent = current;
					PinAVoltage = voltage;
					break;
				case 1:
					PinBCurrent = current;
					PinBVoltage = voltage;
					break;
				case 2:
					IntensityIfTrue = intensity;
					break;
				default:
					throw new InvalidOperationException("Invalid input channel.");
			}
			
			if (Condition(PinACurrent * PinAVoltage != 0, PinBCurrent * PinBVoltage != 0))
			{
				SendToAll(0, Math.Max(PinAVoltage, PinBVoltage), Math.Max(PinAVoltage, PinBVoltage) * IntensityIfTrue);
			}
			else
			{
				SendToAll(0, 0.0f, 0.0f);
			}
		}
		
		public override string[] InputNames
		{
			get
			{
				return new String[]{"signal_a_in", "signal_b_in", "value_if_true"};
			}
		}
		
		public override string[] OutputNames
		{
			get
			{
				return new String[]{"state_out"};
			}
		}
		
		protected LogicGateBase Condition;
		protected Single IntensityIfTrue = 1.0f;
		
		protected Single PinACurrent;
		protected Single PinBCurrent;
		protected Single PinAVoltage;
		protected Single PinBVoltage;
	}
}
