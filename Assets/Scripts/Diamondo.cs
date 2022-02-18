using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamondo : MonoBehaviour
{
    Light crystalLight;
    LightSource lightSourceScript;
    float defaultIntensity = 2.7f;
    public bool crystalHit;
    public bool doPrint = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject childrenLight = this.transform.GetChild(0).gameObject;
        crystalLight = childrenLight.GetComponent<Light>();
        crystalLight.intensity = defaultIntensity;
        crystalHit = false;

        //

        GameObject lightSource = GameObject.Find("Light source");
        lightSourceScript = lightSource.GetComponent<LightSource>();

        //
        
        GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        GetComponentInChildren<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.position);
        //if (doPrint)
        //    print(crystalHit);
        if (crystalHit)
        {
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GetComponentInChildren<Light>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            GetComponentInChildren<Light>().enabled = false;
        }
    }

    /// <summary>
    /// Send message when hit by ray
    /// </summary>
    public void HitByRay()
    {
        crystalHit = true;
        // print("crystal hit");
        // Debug.Log("I was hit by a Ray + crystalHit: " + crystalHit);
        // crystalLight.intensity = 8f;

        // Still needs to come back to default intensity if the ray does not hit!!!!
    }

    public void NotHitByRay()
    {
        crystalHit = false;
        //print("crystal hit");
    }
}
