/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 13:52
 */
using System;

namespace LCS.Conductors
{
	public delegate void LCSEventHandler(Object sender, LCSEventArgs e);
	
	/// <summary>
	/// Embeddable event sender element that can be linked with other conductors.
	/// </summary>
	public class EventConductor : Conductor
	{
		/// <summary>
		/// Invoked whenever signal is received.
		/// </summary>
		public event LCSEventHandler SignalReceived;
		/// <summary>
		/// Invoked when object receives non-zero signal for first time.
		/// </summary>
		public event LCSEventHandler TurnedOn;
		/// <summary>
		/// Invoked when object receives zero signal for first time.
		/// </summary>
		public event LCSEventHandler TurnedOff;
		/// <summary>
		/// Invoked when current value is changed.
		/// </summary>
		public event LCSEventHandler CurrentChanged;
		/// <summary>
		/// Invoked when current direction is changed.
		/// </summary>
		public event LCSEventHandler DirectionChanged;
		/// <summary>
		/// Invoked whenever non-zero signal is received.
		/// </summary>
		public event LCSEventHandler Powered;
		
		protected Single Voltage = 0.0f;
		protected Single Current = 0.0f;
		protected Boolean ForwardDirection;
		
		public EventConductor()
		{
			Connections = new System.Collections.Generic.List<LinkTarget>[0];
		}
		
		public override string[] InputNames {
			get
			{
				return new String[]{"signal_in"};
			}
		}
		
		public override void ReceiveSignal(int channel, float voltage, float current)
		{
			if (channel != 0)
			{
				throw new InvalidOperationException("Invalid input channel.");
			}
			
			if (Voltage == 0.0f && Current == 0.0f && voltage != 0.0f)
			{
				if (TurnedOn != null)
				{
					if (TurnedOn.GetInvocationList().Length > 0)
					{
						TurnedOn(this, new LCSEventArgs(voltage, current));
					}
				}
			}
			else if (Voltage != 0.0f && Current != 0.0f && voltage == 0.0f)
			{
				if (TurnedOff != null)
				{
					if (TurnedOff.GetInvocationList().Length > 0)
					{
						TurnedOff(this, new LCSEventArgs(0.0f, 0.0f));
					}
				}
			}
			if (Current != current)
			{
				if (CurrentChanged != null)
				{
					if (CurrentChanged.GetInvocationList().Length > 0)
					{
						CurrentChanged(this, new LCSEventArgs(voltage, current));
					}
				}
			}
			Boolean Direction = (Voltage >= 0);
			
			if (Direction != ForwardDirection)
			{
				if (DirectionChanged != null)
				{
					if (DirectionChanged.GetInvocationList().Length > 0)
					{
						DirectionChanged(this, new LCSEventArgs(voltage, current));
					}
				}
			}
			
			if (SignalReceived != null)
			{
				if (SignalReceived.GetInvocationList().Length > 0)
				{
					SignalReceived(this, new LCSEventArgs(voltage, current));
				}
			}
			
			if (voltage != 0.0f)
			{
				Powered(this, new LCSEventArgs(voltage, current));
			}
			
			Voltage = voltage;
			Current = current;
			ForwardDirection = Direction;
		}
	}
}
