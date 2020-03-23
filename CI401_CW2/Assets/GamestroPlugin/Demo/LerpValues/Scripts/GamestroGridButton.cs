using GamestroConfig;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gamestro
{
    /// <summary>
    /// Manages grid button
    /// Used to change grid button appearance
    /// Calls OnChangeMixerState to change state of gamestro audio mixer
    /// </summary>
    public class GamestroGridButton : MonoBehaviour
    {
        //used to reset all buttons
        static Action OnReset;

        //action called to change mixer state on button click
        public static Action<State> OnChangeMixerState;

        public Button myButton;
        public Text buttonText;
        public Sprite overrideSprite;

        public State State { get; set; }

        private void Awake()
        {
            GamestroMixerManager.OnSetStateButton += SetInitialStateButton;    
        }

        private void OnDestroy()
        {
            GamestroMixerManager.OnSetStateButton -= SetInitialStateButton;
        }

        // Start is called before the first frame update
        void Start()
        {
            GamestroGridButton.OnReset += Reset;

            myButton = this.GetComponent<Button>();
            myButton.onClick.AddListener(() => GridButtonClicked());
        }

        //called on button click
        private void GridButtonClicked()
        {
            OnChangeMixerState(State);
            OnReset();
            SetAsSelected();
        }
        
        //set appearance of button if its state is selected
        private void SetInitialStateButton(State initialState)
        {
            if (State == initialState)
            {
                SetAsSelected();
            }            
        }

        //change appearance of clicked button
        private void SetAsSelected()
        {
            myButton.image.overrideSprite = overrideSprite;
            buttonText.color = new Color(241 / 255f, 241 / 255f, 241 / 255f, 1f);
        }

        //reset appearance of button
        private void Reset()
        {
            myButton.image.overrideSprite = null;
            buttonText.color = new Color(198 / 255f, 195 / 255f, 198 / 255f, 1f);
        }
    }
}
