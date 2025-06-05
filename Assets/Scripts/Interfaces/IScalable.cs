using System;

namespace Interfaces
{
    public interface IScalable
    {
        float CurrentValue { get; }
        float MaxValue { get; }
        event Action<float> OnValueChanged;
    }
}