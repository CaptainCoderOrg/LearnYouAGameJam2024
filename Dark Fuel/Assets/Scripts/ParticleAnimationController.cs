using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleAnimationController : MonoBehaviour
{
    public ParticleSystem Particles;
    public UnityEvent OnFinished;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Assert(Particles != null);
        Particles.Stop();
    }

    public void Play(AnimationEvent @event)
    {
        Particles.Play();
    }

    public void Stop(AnimationEvent @event)
    {
        Particles.Stop();
        OnFinished?.Invoke();
    }
}
