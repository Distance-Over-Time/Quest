using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio; 

public class AudioManager : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)

        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }

        instance = this;
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
