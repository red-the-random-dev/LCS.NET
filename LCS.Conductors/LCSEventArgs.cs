/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 13:55
 */
using System;

namespace LCS.Conductors
{
	/// <summary>
	/// Class that contains event data used by EventConductor.
	/// </summary>
	public class LCSEventArgs
	{
		public LCSEventArgs(Single voltage, Single current)
		{
			Voltage = voltage;
			Current = current;
		}
		
		public readonly Single Voltage;
		public readonly Single Current;
		
		public Single Resistance
		{
			get
			{
				return Voltage / Current;
			}
		}
		
		public Single Intensity
		{
			get
			{
				return Current / Voltage;
			}
		}
	}
}
