// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;
using OpenAI;

namespace OpenAI.FineTuning
{
    internal readonly partial struct FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum : IEquatable<FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum>
    {
        private readonly string _value;
        private const string AutoValue = "auto";

        public FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum(string value)
        {
            Argument.AssertNotNull(value, nameof(value));

            _value = value;
        }

        public static FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum Auto { get; } = new FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum(AutoValue);

        public static bool operator ==(FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum left, FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum right) => left.Equals(right);

        public static bool operator !=(FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum left, FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum right) => !left.Equals(right);

        public static implicit operator FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum(string value) => new FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum other && Equals(other);

        public bool Equals(FineTuningJobHyperparametersLearningRateMultiplierChoiceEnum other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value) : 0;

        public override string ToString() => _value;
    }
}
