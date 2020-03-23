using System;
using System.Collections;
using System.Collections.Generic;
using GamestroConfig;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Gamestro
{
    /// <summary>
    /// Manages transition of mixer state values
    /// </summary>
    public class GamestroMixerManager : MonoBehaviour
    {
        //contains 27 best combinations for audio mixer, user can add his/her own combination according to his requirement
        [SerializeField]
        private List<MixerStateValue> mixerStateValue;

        //reference to time input field
        public InputField transitionTime;

        //reference to gamestro audio mixer
        public AudioMixer audioMixer;

        //action called to update momentum, depth and power sliders in canvas UI
        public static Action<MixerStateValue> OnUpdateMDPSliders;

        //action called at start to create grid buttons; calls CreateGrid function in class GamestroGridManager 
        public static Action<int> OnInitializeGrid;

        //action called at start to set grid button appearance for initial state
        public static Action<State> OnSetStateButton;

        //stores current state value; used if values are transitioned over given time
        private MixerStateValue currentStateValue;

        private void Awake()
        {
            //calls ChangeMixerViaTime when OnChangeMixerState action is invoked from GamestroGridButton class
            GamestroGridButton.OnChangeMixerState += ChangeMixerViaTime;
        }

        private void OnDestroy()
        {
            GamestroGridButton.OnChangeMixerState -= ChangeMixerViaTime;
        }

        private void Start()
        {
            //stores number of mixer state value listed by user in inspector
            int stateCount = mixerStateValue.Count;
            State initialState = State.STATE1;

            if (stateCount > 0)
            {
                //calls CreateGrid function of GamestroGridManager
                OnInitializeGrid(stateCount);

                //set grid button for initial state
                OnSetStateButton(initialState);

                //Setting default sound to be played by gamestro mixer on start of scene
                ChangeMixer(initialState);
                
            }
        }

        //Changes mixer values without lerp
        private void ChangeMixer(State state)
        {
            MixerStateValue stateValue = mixerStateValue[(int)state];
            SetMixerValues(stateValue, true);
        }

        //sets mixer value and updates Momentum, Depth and Power (MDP) sliders;
        private void SetMixerValues(MixerStateValue stateValue, bool updateCurrentState)
        {
            audioMixer.SetFloat("Momentum", stateValue.momentum);
            audioMixer.SetFloat("Depth", stateValue.depth);
            audioMixer.SetFloat("Power", stateValue.power);

            OnUpdateMDPSliders(stateValue);

            if (updateCurrentState)
            {
                currentStateValue = stateValue;
            }
        }

        //Changes mixer values smoothly over given time
        private void ChangeMixerViaTime(State state)
        {
            string timeStr = transitionTime.text;
            float time;

            if (IsValidTransitionTime(timeStr, out time))
            {
                StopAllCoroutines();
                StartCoroutine(LerpMixerValues(state, time));
            }
            else
            {
                ChangeMixer(state);
            }
        }

        //checks if user entered a valid time in the input box
        private bool IsValidTransitionTime(string str, out float time)
        {
            bool isFloat = float.TryParse(str, out time);

            return (isFloat && time > 0);
        }

        //lerp mixer values from current state to selected state in given time (time is in seconds)
        private IEnumerator LerpMixerValues(State state, float time)
        {
            float t = 0;
            MixerStateValue stateValue = mixerStateValue[(int)state];
            while (t < time && (time - t > 0.12f))
            {
                float momentum = Mathf.Lerp(currentStateValue.momentum, stateValue.momentum, t / time);
                float depth = Mathf.Lerp(currentStateValue.depth, stateValue.depth, t / time);
                float power = Mathf.Lerp(currentStateValue.power, stateValue.power, t / time);

                MixerStateValue newValue = new MixerStateValue(state, momentum, depth, power);
                SetMixerValues(newValue, false);
                t += Time.deltaTime;

                yield return null;
            }

            SetMixerValues(stateValue, true);
        }
    }
}