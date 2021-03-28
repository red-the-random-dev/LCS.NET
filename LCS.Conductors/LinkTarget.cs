/* 
 * Created by SharpDevelop.
 * Date: 28.03.2021
 * Time: 10:16
 */
using System;

namespace LCS.Conductors
{
	/// <summary>
	/// Pair of conductor link and one of its input channel numbers.
	/// </summary>
	public struct LinkTarget
	{
		public readonly Conductor Target;
		public readonly Int32 Channel;
		
		public LinkTarget(Conductor target, Int32 channel)
		{
			Target = target;
			Channel = channel;
		}
	}
}
