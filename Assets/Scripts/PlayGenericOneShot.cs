using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayGenericOneShot : MonoBehaviour
{


    [SerializeField]
    [field: SerializeField] public EventReference UI_start { get; private set; }

    private string soundEvent = null;

    public void PlaySoundEvent()
    {
        if (soundEvent != null)
        {
            AudioManager.instance.PlayOneShot(UI_start, this.transform.position);
        }
    }
}
