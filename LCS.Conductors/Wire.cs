/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 10:27
 */
using System;
using System.Collections.Generic;

namespace LCS.Conductors
{
	/// <summary>
	/// Conductor with fixed resistance that forwards signal from input to output.
	/// </summary>
	public class Wire : Conductor
	{
		protected Single Resistance;
		
		/// <summary>
		/// Instantiates new Wire element.
		/// </summary>
		/// <param name="resistace">Resistance in Ohms.</param>
		public Wire(Single resistace = 0.0f)
		{
			Resistance = resistace;
			Connections = new List<LinkTarget>[1];
			Connections[0] = new List<LinkTarget>();
		}
		
		public override void ReceiveSignal(Int32 channel, Single voltage, Single current)
		{
			if (channel != 0)
			{
				throw new InvalidOperationException("Received signal on non-existent input.");
			}
			
			if (current == 0.0f)
			{
				SendToAll(0, voltage, 0.0f);
				return;
			}
			Single activeResistance = voltage / current;
			activeResistance = (activeResistance > 0 ? activeResistance + Resistance : activeResistance - Resistance);
			SendToAll(0, voltage, voltage / activeResistance);
		}
		
		public override String[] InputNames
		{
			get
			{
				return new String[]{"signal_in"};
			}
		}
		
		public override String[] OutputNames
		{
			get
			{
				return new String[]{"signal_out"};
			}
		}
	}
}
