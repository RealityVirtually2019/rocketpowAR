using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
	public AudioClip doorOpenSFX;
	public AudioClip doorCloseSFX;
	private AudioSource _audioSource;

	public void openElevatorDoors()
	{
		AudioSource audioSource = GetComponent<AudioSource>();
		if (!audioSource.isPlaying)
		{
			audioSource.clip = doorOpenSFX;
			audioSource.Play();
			GetComponent<Animator>().Play("ElevatorDoorOpen");
		}
	}

	public void closeEleatorDoors()
	{
		AudioSource audioSource = GetComponent<AudioSource>();
		if (!audioSource.isPlaying)
		{
			audioSource.clip = doorCloseSFX;
			audioSource.Play();
			GetComponent<Animator>().Play("ElevatorDoor");
		}
	}
}