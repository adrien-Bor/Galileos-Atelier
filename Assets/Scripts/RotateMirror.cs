using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateMirror : MonoBehaviour
{
    // public GameObject mirror;
    public float interact_range;
    private GameObject light_source;
    private GameObject perso;
    private Vector3 dir;

    public Mirror MirrorPrefab;
    private List<Mirror> Mirrors = new List<Mirror>();

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
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("pos:" + transform.position);
            //variableForPrefab = Resources.Load("prefabs/prefab1", GameObject) as GameObject;
            //GameObject MirrorPrefab = (GameObject)Resources.Load("Models/Mirror", typeof(GameObject));
            Vector3 pos = transform.position;
            pos.y = 6.5f;        // to make the mirror stand above the ground
            Mirror mirror = Instantiate<Mirror>(MirrorPrefab, pos, Quaternion.identity);
            mirror.Rotate(45);
            // Mirror mirror = gameObject.AddComponent<Mirror>() as Mirror;

            print("d: " + mirror.d);

            Mirrors.Add(mirror);

            light_source.GetComponent<LightSource>().PrepareRecomputePath();
            StartCoroutine(ExampleCoroutine());
            //light_source = GameObject.Find("Light source");
            //light_source.GetComponent<LightSource>().RecomputePath();
        }
        */
        /*
        if (Input.GetMouseButtonDown(1))
        {
            // right click
            GalileoController persoComponent = perso.GetComponent<GalileoController>();
            dir = persoComponent.dir.normalized;
            // Idk why the character orientation is not the same as the world orientation
            dir.z = dir.y;
            dir.y = 0.0f;

            Ray ray = new Ray(transform.position + Vector3.up, dir);
            RaycastHit hit;

            //If the ray hit
            if (Physics.Raycast(ray, out hit, interact_range))
            {
                //And if it hit a mirror
                Mirror mir = hit.collider.gameObject.GetComponent<Mirror>();

                if (mir != null)
                {
                    mir.DestroyGameObject();
                    //Destroy(mir);
                    
                    light_source.GetComponent<LightSource>().PrepareRecomputePath();
                    StartCoroutine(ExampleCoroutine());
                }
            }
        }
        */

        if (Input.GetKeyDown(KeyCode.Space))  // space bar
        {
            //Find the closest thing on the trajectory

			
            GalileoController persoComponent = perso.GetComponent<GalileoController>();
            dir = persoComponent.dir.normalized;
            // Idk why the character orientation is not the same as the world orientation
            dir.z = dir.y; 
            dir.y = 0.0f;

            // print("Perso dir " + dir);
            // print(transform.rotation * Vector3.forward);

            // Ray going in the character moving direction
            //Ray ray = new Ray(transform.position + Vector3.up, transform.rotation * Vector3.forward);

            // Ray going in the character looking direction (from the mouse)

            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Mirror");

            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }

            Mirror mir = closest.GetComponent<Mirror>();
            mir.Rotate();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (mir.isTouchedByRay)
            {
                light_source.GetComponent<LightSource>().PrepareRecomputePath();
                StartCoroutine(ExampleCoroutine());
                //light_source = GameObject.Find("Light source");
                //light_source.GetComponent<LightSource>().RecomputePath();
            }

            /*
            Ray ray = new Ray(transform.position + Vector3.up, dir);
            RaycastHit hit;

            //If the ray hit
            if (Physics.Raycast(ray, out hit, interact_range))
            {
                //Debug.DrawLine(transform.position, hit.point);

                //And if it hit a mirror
                Mirror mir = hit.collider.gameObject.GetComponent<Mirror>();
                
                if (mir != null)
                {   
                    mir.Rotate();
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    if (mir.isTouchedByRay)
                    {
                        light_source.GetComponent<LightSource>().PrepareRecomputePath();
                        StartCoroutine(ExampleCoroutine());
                        //light_source = GameObject.Find("Light source");
                        //light_source.GetComponent<LightSource>().RecomputePath();
                    }
                }
            }
            */
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
