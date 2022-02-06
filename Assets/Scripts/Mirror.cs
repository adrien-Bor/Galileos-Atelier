using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Vector3 n;
    public Vector3 d;

    public bool translatable = false;
    public Vector3 transPos1, transPos2;

    LightSource lightSourceScript;


    //Initializes variables
    void Awake()
    {
        n = transform.rotation*Vector3.forward;
        d = transform.rotation*Vector3.right;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject lightSource = GameObject.Find("Light source");
        lightSourceScript = lightSource.GetComponent<LightSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Rotates the mirror by an angle of π/2
    public void Rotate()
    {
        print(lightSourceScript.update);
        lightSourceScript.PrepareRecomputePath();
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        transform.Rotate(0,90,0,Space.World);

        n = transform.rotation*Vector3.forward;
        d = transform.rotation*Vector3.right;
        lightSourceScript.RecomputePath();

    }
}
