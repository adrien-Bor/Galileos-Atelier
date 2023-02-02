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

    public List<GameObject> newMirrors = new List<GameObject>();
    public bool isTouchedByRay = false;

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

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    //Rotates the mirror by an angle of π/2
    public void Rotate(int angle = 45)
    {
        //print(lightSourceScript.update);
        //lightSourceScript.PrepareRecomputePath();
        // Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        transform.Rotate(0,angle,0,Space.World);
        //transform.Translate(0, 0, -5);
        n = transform.rotation*Vector3.forward;
        d = transform.rotation*Vector3.right;
        //lightSourceScript.RecomputePath();

    }
}
