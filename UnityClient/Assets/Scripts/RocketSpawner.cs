using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour {

    public GameObject rocket;
    public Vector3 RocketBelowElevator;
    public Vector3 RocketUnlaunched;

    public AudioSource raiseRocketSFX;
    public AudioSource rocketLaunchSFX;

	// Use this for initialization
	void Start () {
        
        rocket.transform.position = RocketBelowElevator;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            RaiseRocket();
        }*/
    }

    public void SpawnRocket()
    {
        GameObject.Instantiate(rocket, RocketBelowElevator, Quaternion.identity);
    }

    public void RaiseRocket()
    {
        if(GameFlowManager.Instance.GetGameState() == GlobalGameState.LAUNCH_ROCKETS && !raiseRocketSFX.isPlaying)
        {
            raiseRocketSFX.Play();
            rocket.transform.position = Vector3.Lerp(RocketBelowElevator, RocketUnlaunched, 2000);
        } else
        {
            Debug.Log("Cannot raise rocket from elevator; game not in LAUNCHROCKETS state.");
        }
    }

    public void LaunchRocket()
    {
        if (GameFlowManager.Instance.GetGameState() == GlobalGameState.LAUNCH_ROCKETS && !raiseRocketSFX.isPlaying)
        {
            rocketLaunchSFX.Play();
            //call the animation?
        }

    }
}
