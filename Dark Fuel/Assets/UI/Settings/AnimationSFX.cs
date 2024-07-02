using UnityEngine;
public class AnimationSFX : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] Clips;
    public void Play()
    {
        if (AudioSource.isPlaying) { return; }
        AudioSource.clip = Clips[Random.Range(0, Clips.Length)];
        AudioSource.Play();
    }

    public void Play(int id)
    {
        AudioSource.clip = Clips[id];
        AudioSource.Play();
    }
}