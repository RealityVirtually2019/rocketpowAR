using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour {

    public GameObject ConveyerBeltCenter;
    public Vector3 conveyerBeltBelowElevator;
    public Vector3 conveyerBeltRaised;

    public AudioSource conveyerBeltRaiseSFX;
    public AudioSource conveyerBeltLowerSFX;
    public AudioSource conveyerBeltRotateSFX;

	// Use this for initialization
	void Start () {
        ConveyerBeltCenter.transform.position = conveyerBeltBelowElevator;
	}
	
	// Update is called once per frame
	void Update () {
        //for testing purposes
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateConveyerBeltRight();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            RotateConveyerBeltLeft();
        }*/
	}

    public void RaiseConveyerBelt()
    {
        if (GameFlowManager.Instance.GetGameState() == GlobalGameState.SELECT_EXERCISE)
        {
            conveyerBeltRaiseSFX.Play();
            ConveyerBeltCenter.transform.position = Vector3.Lerp(conveyerBeltBelowElevator, conveyerBeltRaised, 2000f);
        }
    }

    public void LowerConveyerBelt()
    {
        if (GameFlowManager.Instance.GetGameState() == GlobalGameState.SELECT_EXERCISE)
        {
            conveyerBeltLowerSFX.Play();
            ConveyerBeltCenter.transform.position = Vector3.Lerp(conveyerBeltRaised, conveyerBeltBelowElevator, 2000f);
        }
    }

    public void RotateConveyerBeltRight()
    {
        if (GameFlowManager.Instance.GetGameState() == GlobalGameState.SELECT_EXERCISE && !conveyerBeltRotateSFX.isPlaying)
        {
            ConveyerBeltCenter.transform.Rotate(gameObject.transform.position, 45f);
        }
    }

    public void RotateConveyerBeltLeft()
    {
        if (GameFlowManager.Instance.GetGameState() == GlobalGameState.SELECT_EXERCISE && !conveyerBeltRotateSFX.isPlaying)
        {
            ConveyerBeltCenter.transform.Rotate(gameObject.transform.position, -45f);
        }
    }
}
