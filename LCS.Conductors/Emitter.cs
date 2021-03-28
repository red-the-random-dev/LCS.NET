/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 13:36
 */
using System;
using System.Threading.Tasks;

namespace LCS.Conductors
{
	/// <summary>
	/// Source of current with fixed voltage.
	/// </summary>
	public class Emitter : Conductor
	{
		public Emitter(Single voltage)
		{
			Voltage = voltage;
			Connections = new System.Collections.Generic.List<LinkTarget>[1];
			Connections[0] = new System.Collections.Generic.List<LinkTarget>();
		}
		
		protected Single Voltage;
		
		public override String[] InputNames
		{
			get
			{
				return new String[]{"intensity_in"};
			}
		}
		
		public override void ReceiveSignal(int channel, float voltage, float current)
		{
			if (channel != 0)
			{
				throw new InvalidOperationException("Invalid input channel.");
			}
			if (voltage != 0.0f && current != 0.0f)
			{
				FlipOn(current / voltage);
			}
		}
		
		/// <summary>
		/// Send signal of certain intensity.
		/// </summary>
		/// <param name="intensity">Intensity of signal. (intensity = current / voltage)</param>
		public void FlipOn(Single intensity)
		{
			if (intensity == 0.0f)
			{
				FlipOff();
				return;
			}
			
			SendToAll(0, Voltage, Voltage*intensity);
		}
		
		public void FlipOff()
		{
			foreach (LinkTarget lt in Connections[0])
			{
				lt.Target.ReceiveSignal(lt.Channel, 0.0f, 0.0f);
			}
		}
	}
}
