using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODEvents : MonoBehaviour
{
    [field: Header("MIROR")]
    [field: SerializeField] public EventReference miror_rotation { get; private set; }

    [field: Header("PLAYER FOOTSTEPS")]
    [field: SerializeField] public EventReference footsteps { get; private set; }


    [field: Header("UI")]
    [field: SerializeField] public EventReference UI_button { get; private set; }
    [field: SerializeField] public EventReference UI_start { get; private set; }


    [field: Header("SFX")]
    [field: SerializeField] public EventReference sphere_light { get; private set; }
    [field: SerializeField] public EventReference flashlight { get; private set; }
    [field: SerializeField] public EventReference loose { get; private set; }
    [field: SerializeField] public EventReference win { get; private set; }


    [field: Header("MUSIC")]
    [field: SerializeField] public EventReference music_lvl1 { get; private set; }
    [field: SerializeField] public EventReference music_menu { get; private set; }

    public static FMODEvents instance_bizarre { get; private set; }



    private void Awake()
    {
        instance_bizarre = this;
    }
}
