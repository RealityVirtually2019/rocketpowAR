using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	public AudioSource MusicAudioSource;
	public AudioSource AmbientAudioSource;
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
		MusicAudioSource.loop = true;
		MusicAudioSource.Play();
	}

	public void PlayAmbient(AudioClip audioToPlay)
	{
		AmbientAudioSource.clip = audioToPlay;
		AmbientAudioSource.loop = true;
		FadeIn(AmbientAudioSource, 2f);
	}

	public bool IsMusicStillPlaying()
	{
		return MusicAudioSource.isPlaying;
	}

	public void PlayNarrative(AudioClip audioToPlay, float delay = 0f)
	{
		NarrativeAudioSource.clip = audioToPlay;
		if (delay == 0f)
		{
			NarrativeAudioSource.Play();
		}
		else
		{
			NarrativeAudioSource.PlayDelayed(delay);
		}
	}

	public bool IsNarrativeStillPlaying()
	{
		return NarrativeAudioSource.isPlaying;
	}

	public void StopMusic()
	{
		StartCoroutine(FadeOut(MusicAudioSource, 3f));
	}

	public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
	{
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
		audioSource.Stop();
	}

	public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
	{
		audioSource.Play();
		audioSource.volume = 0f;
		while (audioSource.volume < 1)
		{
			audioSource.volume += Time.deltaTime / fadeTime;
			yield return null;
		}
	}
}