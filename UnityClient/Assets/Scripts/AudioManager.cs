using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	public AudioSource MusicAudioSource;
	public AudioSource NarrativeAudioSource;
	public AudioSource SoundEffectAudioSource;

	public void Update()
	{
		if (InputManager.Instance.IsTriggerDown())
		{
			PlaySoundEffect(AudioLibrary.Instance.TriggerSound);
		}
	}
	
	public void PlaySoundEffect(AudioClip audioToPlay)
	{
		SoundEffectAudioSource.clip = audioToPlay;
		SoundEffectAudioSource.Play();
	}
	
	public void PlayMusic(AudioClip audioToPlay)
	{
		MusicAudioSource.clip = audioToPlay;
		MusicAudioSource.Play();
	}
	
	public bool IsMusicStillPlaying()
	{
		return MusicAudioSource.isPlaying;
	}
	
	public void PlayNarrative(AudioClip audioToPlay)
	{
		NarrativeAudioSource.clip = audioToPlay;
		NarrativeAudioSource.Play();
	}
	
	public bool IsNarrativeStillPlaying()
	{
		return NarrativeAudioSource.isPlaying;
	}
}