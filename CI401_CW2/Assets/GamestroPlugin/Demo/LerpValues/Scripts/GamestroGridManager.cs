using GamestroConfig;
using UnityEngine;

namespace Gamestro
{
    /// <summary>
    /// Creates grid of gamestro stage buttons. Manages their click events.
    /// </summary>
    public class GamestroGridManager : MonoBehaviour
    {
        //reference to grid container's transform
        public Transform gridContainer;

        //reference to prefab of button to be created in grid container
        public GameObject buttonPrefab;
     
        private void Awake()
        {
            //intialize grid and set initial sound when scene is started
            GamestroMixerManager.OnInitializeGrid += CreateGrid;    
        }

        private void OnDestroy()
        {
            GamestroMixerManager.OnInitializeGrid -= CreateGrid;
        }      
        
        //creates grid of buttons for different gamestro stages
        private void CreateGrid(int stateCount)
        {
            for (int i = 0; i < stateCount; i++)
            {
                GameObject obj = Instantiate(buttonPrefab) as GameObject;
                obj.transform.SetParent(gridContainer);
                obj.name = (i + 1).ToString();

                int index = i;

                GamestroGridButton gridButton = obj.GetComponent<GamestroGridButton>();
                gridButton.buttonText.text = obj.name;
                gridButton.State = (State)index; //converting index to State enum
            }
        }
    }
}