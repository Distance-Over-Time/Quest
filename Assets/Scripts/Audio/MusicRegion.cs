using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class MusicRegion : MonoBehaviour
{
    [SerializeField] string parameterName;


    private void OnTriggerEnter(Collider other)
    {
        MusicManager.instance.ChangeParameter(parameterName, 1.0f);
    }

    private void OnTriggerExit(Collider other)
    {
        MusicManager.instance.ChangeParameter(parameterName, 0.0f);
    }
}
