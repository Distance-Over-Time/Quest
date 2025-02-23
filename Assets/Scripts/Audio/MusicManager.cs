using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class MusicManager : MonoBehaviour
{
    // audio
    private EventInstance musicInstance;
    public static MusicManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)

        {
            Debug.LogError("Found more than one Music Manager in the scene.");
        }

        instance = this;
    }

    public void ChangeParameter(string name, float value)
    {
        musicInstance.setParameterByName(name, value);
    }

    // Start is called before the first frame update

    private void Start()
    {
        musicInstance = AudioManager.instance.CreateEventInstance(FMODEvents.instance.music);
        musicInstance.start();
    }

}
