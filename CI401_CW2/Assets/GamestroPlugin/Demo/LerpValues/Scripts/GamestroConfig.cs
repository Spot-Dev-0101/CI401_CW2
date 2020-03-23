using System;

namespace GamestroConfig
{
    /// <summary>
    /// Namespace GamestroConfig contains classes and enums used in sample code.
    /// </summary>
    public enum State {STATE1=0, STATE2, STATE3, STATE4, STATE5,
        STATE6, STATE7, STATE8, STATE9, STATE10, STATE11, STATE12,
        STATE13, STATE14, STATE15, STATE16, STATE17, STATE18, STATE19,
        STATE20, STATE21, STATE22, STATE23, STATE24, STATE25, STATE26, STATE27};

    public enum SliderType {MOMENTUM, DEPTH, POWER};

    //Stores audio mixer values processed while changing STATE from code in GamestroMixerManager
    [Serializable]
    public class MixerStateValue
    {
        public State state;
        public float momentum;
        public float depth;
        public float power;

        public MixerStateValue()
        {
        }

        public MixerStateValue(State _state, float _momentum, float _depth, float _power)
        {
            state = _state;
            momentum = _momentum;
            depth = _depth;
            power = _power;
        }

        public override string ToString()
        {
            return "\nMomentum :" + momentum.ToString() + "\nDepth :" + depth.ToString() + "\nPower :" + power.ToString();
        }
    }
}
