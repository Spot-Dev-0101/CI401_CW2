using GamestroConfig;
using UnityEngine;
using UnityEngine.UI;

namespace Gamestro
{
    /// <summary>
    /// Manages sliders for MDP : Momentum, Depth and Power
    /// </summary>
    public class GamestroMDPSlider : MonoBehaviour
    {
        //tells type of slider
        public SliderType sliderType;

        //reference of slider text
        public Text sliderText;

        //reference of slider
        public Slider slider;


        private void Awake()
        {
            //UpdateMDPSlider called when action is called from GamestroMixerManager class
            GamestroMixerManager.OnUpdateMDPSliders += UpdateMDPSlider;
        }

        private void OnDestroy()
        {
            GamestroMixerManager.OnUpdateMDPSliders -= UpdateMDPSlider;
        }

        //updates MDP slider value and text according to slider parameter
        private void UpdateMDPSlider(MixerStateValue stateValue)
        {
            float value = 0f;

            switch (sliderType)
            {            
                case SliderType.MOMENTUM:
                    value = stateValue.momentum;
                    break;

                case SliderType.DEPTH:
                    value = stateValue.depth;
                    break;

                case SliderType.POWER:
                    value = stateValue.power;
                    break;
            }

            slider.value = value;
            sliderText.text = sliderType.ToString() + " : " + (Mathf.Round(value * 100) / 100).ToString();
        }
    }
}