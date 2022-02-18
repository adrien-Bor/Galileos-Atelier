using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateMirror : MonoBehaviour
{
    public GameObject mirror;
    public float interact_range;
    private GameObject light_source;
    private GameObject perso;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
       light_source = GameObject.Find("Light source");
       perso = GameObject.Find("Galileo");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mirror = Resources.Load("Mirror") as GameObject;
            print(transform.position);
            Instantiate(mirror, transform.position, Quaternion.identity);
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Find the closest thing on the trajectory

			
            GalileoController persoComponent = perso.GetComponent<GalileoController>();
            dir = persoComponent.dir.normalized;
            // Idk why the character orientation is not the same as the world orientation
            dir.z = dir.y; 
            dir.y = 0.0f;
            
            print("Perso dir " + dir);
            print(transform.rotation * Vector3.forward);

            // Ray going in the character moving direction
            //Ray ray = new Ray(transform.position + Vector3.up, transform.rotation * Vector3.forward);



            // Ray going in the character looking direction (from the mouse)
            Ray ray = new Ray(transform.position + Vector3.up, dir);
            RaycastHit hit;

            //If the ray hit
            if (Physics.Raycast(ray, out hit, interact_range))
            {
                Debug.DrawLine(transform.position, hit.point);

                //And if it hit a mirror
                Mirror mir = hit.collider.gameObject.GetComponent<Mirror>();
                if (mir != null)
                {
                    light_source.GetComponent<LightSource>().PrepareRecomputePath();
                    mir.Rotate();
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    StartCoroutine(ExampleCoroutine());
             
                }
            }
        }
    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForFixedUpdate();
        light_source = GameObject.Find("Light source");
        light_source.GetComponent<LightSource>().RecomputePath();
    }
}
