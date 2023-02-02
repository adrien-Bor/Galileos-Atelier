using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{
    [SerializeField] AudioClip audioDoor;
    public bool test = true;

    // Start is called before the first frame update
    void Start()
    {
        test = false;
    }

    // Update is called once per frame
    void Update()
    {
        test = false;
    }

    /// <summary>
    /// Should open the door
    /// </summary>
    public void OpenDoor()
    {
        Debug.Log("Opening Door");
        this.transform.Translate(Vector3.forward * 20f);
        AudioSource.PlayClipAtPoint(audioDoor, Camera.main.transform.position);

        // Play sound? Move progressivelly?
    }
}
