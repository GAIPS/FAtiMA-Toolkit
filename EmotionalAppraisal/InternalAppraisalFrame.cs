using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;

namespace EmotionalAppraisal
{
	public partial class EmotionalAppraisalAsset
	{
		private class InternalAppraisalFrame : IAppraisalFrame
		{
			private Dictionary<object, ComponentAppraisalFrame> nestedFrames = new Dictionary<object, ComponentAppraisalFrame>();
			private Dictionary<string, AppraisalVariable> appraisalVariables = new Dictionary<string, AppraisalVariable>();

			public IEventRecord AppraisedEvent { get; private set; }

			public IEnumerable<string> AppraisalVariables
			{
				get { return this.appraisalVariables.Keys; }
			}

			public bool IsEmpty
			{
				get { return this.appraisalVariables.Count == 0; }
			}

			public long LastChange
			{
				get;
				private set;
			}

			public InternalAppraisalFrame()
			{
				AppraisedEvent = null;
				LastChange = 0;
			}

			public void Reset(IEventRecord evt)
			{
				this.AppraisedEvent = evt;
				this.LastChange = 0;
				this.nestedFrames.Clear();
				this.appraisalVariables.Clear();
			}

			public IWritableAppraisalFrame RequestComponentFrame(object componentObject, short weight)
			{
				ComponentAppraisalFrame frame;
				if (!this.nestedFrames.TryGetValue(componentObject, out frame))
				{
					frame = new ComponentAppraisalFrame(this, componentObject, weight);
					this.nestedFrames[componentObject] = frame;
				}
				return frame;
			}

			public float GetAppraisalVariable(string appraisalVariable)
			{
				AppraisalVariable v;
				if (this.appraisalVariables.TryGetValue(appraisalVariable, out v))
					return v.NormalizedValue();
				return 0f;
			}

			public bool ContainsAppraisalVariable(string appraisalVariable)
			{
				return this.appraisalVariables.ContainsKey(appraisalVariable);
			}

			public bool TryGetAppraisalVariable(string appraisalVariable, out float value)
			{
				AppraisalVariable v;
				if (this.appraisalVariables.TryGetValue(appraisalVariable, out v))
				{
					value = v.NormalizedValue();
					return true;
				}
				value = 0;
				return false;
			}

			#region Nested Classes

			private struct Pair
			{
				public float value;
				public short weight;

				public Pair(float value, short weight) : this()
				{
					this.value = value;
					this.weight = weight;
				}
			}

			private class AppraisalVariable
			{
				private Dictionary<object, Pair> values;
				public short Weight
				{
					get;
					private set;
				}

				public AppraisalVariable()
				{
					this.values = new Dictionary<object, Pair>();
					this.Weight = 0;
				}

				public bool AddValue(object componentType, float value, short weight)
				{
					Pair p;
					if (this.values.TryGetValue(componentType, out p))
					{
						if (p.value == value)
							return false;

						p.value = value;
					}
					else
					{
						p = new Pair(value, weight);
						this.values[componentType] = p;
						this.Weight += weight;
					}

					return true;
				}

				public bool ContainsComponent(object component)
				{
					return this.values.ContainsKey(component);
				}

				public float NormalizedValue()
				{
					if (this.Weight <= 0)
						return 0;

					return this.values.Aggregate(0f, (acc, pair) => acc + (pair.Value.value * pair.Value.weight)) / this.Weight;
				}
			}

			private class ComponentAppraisalFrame : IWritableAppraisalFrame
			{
				private InternalAppraisalFrame parentFrame;
				private object component;
				private short weight;

				public IEventRecord AppraisedEvent
				{
					get {
						return this.parentFrame.AppraisedEvent;
					}
				}

				public IEnumerable<string> AppraisalVariables
				{
					get {
						return this.parentFrame.appraisalVariables.Where(p => p.Value.ContainsComponent(this.component)).Select(p => p.Key);
					}
				}

				public bool IsEmpty
				{
					get {
						return this.parentFrame.appraisalVariables.Where(p => p.Value.ContainsComponent(this.component)).Count() == 0;
					}
				}

				public long LastChange
				{
					get;
					private set;
				}

				public ComponentAppraisalFrame(InternalAppraisalFrame parentFrame, object component, short weight)
				{
					this.parentFrame = parentFrame;
					this.component = component;
					this.weight = weight;
					this.LastChange = 0;
				}

				public void SetAppraisalVariable(string appraisalVariableName, float value)
				{
					AppraisalVariable v;
					if (!this.parentFrame.appraisalVariables.TryGetValue(appraisalVariableName, out v))
					{
						v = new AppraisalVariable();
						this.parentFrame.appraisalVariables[appraisalVariableName] = v;
					}

					if (v.AddValue(this.component, value, this.weight))
						parentFrame.LastChange = LastChange = DateTime.Now.Ticks;
				}

				public float GetAppraisalVariable(string appraisalVariable)
				{
					return parentFrame.GetAppraisalVariable(appraisalVariable);
				}

				public bool ContainsAppraisalVariable(string appraisalVariable)
				{
					return parentFrame.ContainsAppraisalVariable(appraisalVariable);
				}

				public bool TryGetAppraisalVariable(string appraisalVariable, out float value)
				{
					return parentFrame.TryGetAppraisalVariable(appraisalVariable, out value);
				}
			}

			#endregion
		}
	}
}
