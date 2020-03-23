using UnityEngine;
using UnityEngine.Audio;

public class GamestroMixerManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    private void Start()
    {
       ChangeMixer(0,1,0);
    }

    private void ChangeMixer(float momentum, float depth, float power)
    {
       audioMixer.SetFloat("Momentum", momentum);
       audioMixer.SetFloat("Depth", depth);
       audioMixer.SetFloat("Power", power);
    }
}
