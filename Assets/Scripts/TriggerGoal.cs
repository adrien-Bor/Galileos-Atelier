using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class TriggerGoal : MonoBehaviour
{
    // Audio when we success
    [field: Header("Audio goal")]
    [field: SerializeField] public EventReference win { get; private set; } 
    SceneLoader sceneLoader;

    //[Header("Door")]
    //public GameObject door;
    //float t;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    /// <summary>
    /// Here we can detect when the character triggers certain object. We can detect which object is and execute anything based on it (go to next level, run an audio clip, etc...)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Name: " + other.gameObject.name);

        if (other.tag == "Goal")
        {
            Debug.Log("You reached the goal! - Going to next level");

            // Code for going to next level
            // ==========

            // Play some clip and load next scene with some delay
            // Important: At some point is the last level, so this will give error. We need to go to Success screen or menu directly
            AudioManager.instance.PlayOneShot(win, this.transform.position);
            Invoke("loadNext", 2f);
            
            // ==========
        }

        /*
        else if (other.tag == "SwitchDoor")
        {
            Debug.Log("Opening door - PROVISIONAL");
            door.transform.Translate(Vector3.forward * 10f);
        }
        */

        // We can put here also actions when we hit an enemy (if tag == "Enemy"...)
        // ==========

        // ==========
    }

    public void loadNext()
    {
        print("...now!");
        sceneLoader.LoadNextScene();
    }
}
