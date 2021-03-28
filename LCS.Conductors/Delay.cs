/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 14:35
 */
using System;
using System.Threading;

namespace LCS.Conductors
{
	/// <summary>
	/// Conductor that forwards signal after certain delay.
	/// </summary>
	public class Delay : Conductor
	{
		public Delay(Int32 delayMilliseconds)
		{
			del = delayMilliseconds;
			Connections = new System.Collections.Generic.List<LinkTarget>[1];
			Connections[0] = new System.Collections.Generic.List<LinkTarget>();
		}
		
		public override string[] InputNames
		{
			get
			{
				return new String[]{"signal_in"};
			}
		}
		
		public override void ReceiveSignal(Int32 channel, Single voltage, Single current)
		{
			if (channel != 0)
			{
				throw new InvalidOperationException("Received signal on non-existent input.");
			}
			
			Thread.Sleep(del);
			SendToAll(0, voltage, current);
		}
		
		public override string[] OutputNames
		{
			get
			{
				return new String[]{"signal_out"};
			}
		}
		
		protected Int32 del;
	}
}
