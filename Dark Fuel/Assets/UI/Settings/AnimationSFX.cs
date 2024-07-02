using UnityEngine;
public class AnimationSFX : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] Clips;
    public void Play()
    {
        AudioSource.clip = Clips[Random.Range(0, Clips.Length)];
        AudioSource.Play();
    }
}