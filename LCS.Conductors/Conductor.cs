/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 10:13
 */
using System;
using System.Collections.Generic;

namespace LCS.Conductors
{
	/// <summary>
	/// Class that declares conductor behavior.
	/// </summary>
	public abstract class Conductor : IDisposable
	{
		/// <summary>
		/// Array of printable input channel names.
		/// </summary>
		public abstract String[] InputNames
		{
			get;
		}
		
		/// <summary>
		/// Array of printable output channel names.
		/// </summary>
		public virtual String[] OutputNames
		{
			get
			{
				List<String> x = new List<string>();
				for (int i = 0; i < Connections.Length; i++)
				{
					x.Add(String.Format("signal_out_{0}", i));
				}
				return x.ToArray();
			}
		}
		
		/// <summary>
		/// Defines behavior on receiving signal from previous element in the grid.
		/// </summary>
		/// <param name="channel">Input channel.</param>
		/// <param name="voltage">Signal voltage in volts.</param>
		/// <param name="current">Signal current in amperes.</param>
		/// <exception cref="@InvalidOperationException">Invalid input channel.</exception>
		public abstract void ReceiveSignal(Int32 channel, Single voltage, Single current);
		
		/// <summary>
		/// Links conductor to next element in grid.
		/// </summary>
		/// <param name="outputChannel">Conductor's output channel.</param>
		/// <param name="target">Target conductor.</param>
		/// <param name="targetChannel">Target conductor's input channel.</param>
		/// <exception cref="@InvalidOperationException">Output channel number is out of bounds.</exception>
		public virtual void Link(Int32 outputChannel, Conductor target, Int32 targetChannel)
		{
			if (outputChannel < 0 || outputChannel > Connections.Length)
			{
				throw new InvalidOperationException("Output channel number is out of bounds.");
			}
			Connections[outputChannel].Add(new LinkTarget(target, targetChannel));
		}
		
		protected List<LinkTarget>[] Connections;
		
		/// <summary>
		/// Sends signal through selected output channel.
		/// </summary>
		/// <param name="channel">Output channel.</param>
		/// <param name="voltage">Signal's voltage.</param>
		/// <param name="current">Signal's current.</param>
		protected virtual void SendToAll(Int32 channel, Single voltage, Single current)
		{
			Int32 ReceiversAmount = Connections[channel].Count;
			for (int i = 0; i < ReceiversAmount; i++)
			{
				LinkTarget lt = Connections[channel][i];
				lt.Target.ReceiveSignal(lt.Channel, voltage, current);
			}
		}
		
		public void Dispose()
		{
			for (int i = 0; i < Connections.Length; i++)
			{
				Connections[i].Clear();
			}
		}
		
		~Conductor()
		{
			Dispose();
		}
	}
}