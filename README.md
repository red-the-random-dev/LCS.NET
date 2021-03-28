# LCS.NET
Logic Circuitry Simulation library for .NET

The idea of this library is simulation of wired logic grid that can interact with other objects via events or conductors' properties.
It can be imported in required .NET project, so you can make implementations for your own elements that can play part in the grid.
The simulation is not meant to be completely accurate as it does not require grid to be looped.

The base of each element is set of inputs and outputs that can take signal of certain voltage and current. Signal can be characterized like following:

+ **Resistance**: Defines relation between voltage and current. Measured in Ohms.
+ **Intensity**: Mostly used as value. Defines relation between current and voltage. Measured in Ohms^-1.

## Example
We want to make a scheme that consists of *emitter*, *wire* and *event conductor*. Let's define neeeded elements:
```csharp
Emitter src = new Emitter(5.0f); // Creates power source with voltage of 5 Volts
Wire w = new Wire(1.0f); // Creates wire with resistance of 1 Ohm
EventConductor ec = new EventConductor(); // Creates end point that will fire events
```
Then, let's define function that will print something whenever there's signal appearing in the event conductor:
```csharp
// Function that will be fired when there is signal appearing in the conductor
static void PrintWhenOn(Object sender, LCSEventArgs e)
{
  Console.WriteLine("Power is on!");
}
```
We need to add this function to `TurnedOn` event of event conductor:
```csharp
ec.TurnedOn += PrintWhenOn;
```
Whenever conductor has non-zero signal, the event will be fired.

Now we need to connect conductors:
```csharp
src.Link(0, w, 0); // Link source to wire's input
w.Link(0, ec, 0); // Link wire to event conductor's input
```
To make everything running, we need to turn on emitter:
```csharp
src.FlipOn(1.0f); // Send 5V 5A signal from source (intensity = 1.0)
```
After this, we should see following output:
```
Power is on!
```

(C) 2021 red-the-random-dev

This software is distributed under GPL license.
