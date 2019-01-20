using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour {

    public GameObject door1;
    public GameObject door2;

    public Vector3 door1start;
    public Vector3 door2start;
    public Vector3 door1end;
    public Vector3 door2end;

    public AudioSource doorOpenSFX;
    public AudioSource doorCloseSFX;

	// Use this for initialization
	void Start () {
        door1.transform.position = door1start;
        door2.transform.position = door2start;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openElevatorDoors()
    {
        if(!doorOpenSFX.isPlaying || !doorCloseSFX.isPlaying)
        {
            doorOpenSFX.Play();
            door1.transform.position = Vector3.Lerp(door1start, door1end, 1000);
            door2.transform.position = Vector3.Lerp(door2start, door2end, 1000);
        }
    }

    public void closeEleatorDoors()
    {
        if (!doorOpenSFX.isPlaying || !doorCloseSFX.isPlaying)
        {
            doorCloseSFX.Play();
            door1.transform.position = Vector3.Lerp(door1end, door1start, 1000);
            door2.transform.position = Vector3.Lerp(door2end, door2start, 1000);
        }
    }
}
