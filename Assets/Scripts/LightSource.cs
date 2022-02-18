using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{

	public List<Vector3> light_path; //Each angle where the light will turn
	public List<GameObject> beams; //A reference to each beam, to destroy them when the time is needed
	public Vector3 source_direction; //Direction of the first light beam
	public GameObject lightBeamPrefab;

	// Some bools to keep track of what we hit
	public bool isCrystalHit = false;
	public bool update = false;
	public bool isExitDoorHit = false;

	public Diamondo diamond;
	public MoveDoor door;
	//private int frame = 0;

	// Start is called before the first frame update
	void Start()
    {
		RecomputePath();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (update)
		{
			update = false;
			RecomputePath();
		}*/
        //frame++;
        //if (frame == 1)
        //{
        //    PrepareRecomputePath();
        //    frame = 0;
        //}
        //PrepareRecomputePath();
        //RecomputePath();


        //Draw the beam trajectory in debug mode
        //for (int i = light_path.Count - 1; i > 0; --i)
        //    Debug.DrawLine(light_path[i], light_path[i - 1], Color.yellow);

        //print(beams[1].transform.position);
    } 

	//Call this first because beams won't be destroyed until end of frame
	public void PrepareRecomputePath()
	{
		print("PrepareRecomputePath");
		for(int i = 0; i < beams.Count; ++i)
			Destroy(beams[i]);
	}

	public void RecomputePath()
	{
		print("RecomputePath");
		light_path.Clear();
		light_path.Add(transform.position);

		
		beams.Clear();

		Vector3 dir = source_direction;
		Vector3 new_dir = Vector3.zero;
		bool ray_absorbed = false;

		GameObject[] diamonds;
		diamonds = GameObject.FindGameObjectsWithTag("crystal");

		foreach (GameObject diamond in diamonds)
		{
			diamond.GetComponent<Diamondo>().NotHitByRay();
		}

		while (!ray_absorbed)
		{
			//Find the closest thing on the trajectory
			Ray ray = new Ray(light_path[light_path.Count - 1], dir);
			RaycastHit hit;

			//If the ray hit
			if (Physics.Raycast(ray, out hit, 200))
			{
				isCrystalHit = false;
				// New - detect crytals
				if (hit.transform.tag == "crystal")
                {
					isCrystalHit = true;
					//diamond.crystalHit = true;
					GameObject diam = hit.collider.gameObject;
					diam.GetComponent<Diamondo>().HitByRay();
				}


				if (hit.transform.tag == "ExitDoor")
				{
					//hit.transform.SendMessage("OpenDoor");
					GameObject doorObj = hit.collider.gameObject;
					MoveDoor D = doorObj.GetComponent<MoveDoor>();
					print(D.test);
					D.OpenDoor();
					isExitDoorHit = true;
				}
				else
                {
					isExitDoorHit = false;
				}

                //beams.Add(beam);
                //print(!isCrystalHit);

    //            print("hit: "+hit.point);
				//GameObject mir1 = GameObject.Find("Mirror (1)");
				//print(mir1.GetComponent<Mirror>().d);

                //Stop the ray if it does not hit a mirror
                if (hit.collider.gameObject.GetComponent<Mirror>() == null)
				{
					//But continue if it is a crystal or Galileo
					if ( (!isCrystalHit)
					&& hit.collider.gameObject.GetComponent<RotateMirror>() == null) 
						ray_absorbed = true;
				}
				else //Or make it bounce
				{
					//Find the new direction
					Mirror mir = hit.collider.gameObject.GetComponent<Mirror>();
					//print("DIRECTION: "+dir);
					//print(mir.d);
					//print(mir.n);
					new_dir = Vector3.Dot(dir, mir.d) * mir.d - Vector3.Dot(dir, mir.n) * mir.n;
					//print(new_dir);
				}
				if (!isCrystalHit)
				{
					light_path.Add(hit.point);
					light_path.Add(hit.point + new_dir*2);
				}
				else
				{
					//print(dir);
					light_path.Add(hit.point + dir);
				}
				dir = new_dir;

			}
			else
			{
				ray_absorbed = true;
				light_path.Add(light_path[light_path.Count - 1] + dir * 100);
			}

			//Prevent infinite loops
			if(light_path.Count > 100)
				ray_absorbed = true;


		}

        for (int i = 0; i < light_path.Count-1; ++i)
        {
            //Spawn the beam of light
            Vector3 pos = (light_path[i] + light_path[i + 1]) / 2.0f;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, light_path[i + 1] - light_path[i])
                                         * lightBeamPrefab.transform.rotation;
            GameObject beam = Instantiate(lightBeamPrefab, pos, rot);

            //Resize to look like a beam
            float length_tf = (light_path[i] - light_path[i + 1]).magnitude / 2.0f;
			//print(light_path[i]);
			//print("length" + length_tf);
			//length_tf /= (length_tf + (float)1.5) / length_tf; //black magic, don't touch
			beam.transform.localScale = Vector3.up * (length_tf);//+(float)0.1);
            beam.transform.localScale -= ((float)9 / 10) * (Vector3.right + Vector3.forward);
			beam.transform.localScale += Vector3.right * (float)0.3;
			//print(beam.transform.localScale);
			//Add the new point, and the beam to the list
			//light_path.Add(hit.point);
			beams.Add(beam);
        }
        //Debug.Log("Number of bounces: " + (light_path.Count - 1));
    }
}
