using System.Collections;
using CaptainCoder.DarkFuel;
using UnityEngine;
using UnityEngine.Events;

public class CoasterController : MonoBehaviour
{
    public UnityEvent OnEnter;
    public Transform PlayerLock;
    public PlayerComponents Player;
    public Animator KettleAnimator;
    public KettlePourStateMachineBehaviour KettlePourSMB;
    public MeshRenderer CoasterRenderer;
    public Material ActiveMaterial;
    public Material OriginalMaterial;
    public void Awake()
    {
        Player = FindFirstObjectByType<PlayerComponents>();
        Debug.Assert(Player != null);
        KettlePourSMB = KettleAnimator.GetBehaviour<KettlePourStateMachineBehaviour>();
        KettlePourSMB.OnFinished.AddListener(UnlockPlayer);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.tag == "Player")
        {
            OnEnter?.Invoke();
        }
    }

    public void PausePlayerAndAnimate()
    {
        OriginalMaterial = CoasterRenderer.material;
        CoasterRenderer.material = ActiveMaterial;
        Player.Lock();
        StopAllCoroutines();
        StartCoroutine(LockPosition());
        KettleAnimator.SetBool("isPouring", true);
    }
    public float PositionDuration = 1f;
    private IEnumerator LockPosition()
    {
        Vector3 startPosition = Player.transform.position;
        Quaternion startRotation = Player.Model.transform.rotation;
        float timeRemaining = PositionDuration;
        float totalDuration = timeRemaining;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float percent = 1 - (timeRemaining / totalDuration);
            Player.transform.position = Vector3.Lerp(startPosition, PlayerLock.transform.position, percent);
            Player.Model.transform.rotation = Quaternion.Lerp(startRotation, PlayerLock.transform.rotation, percent);
            yield return null;
        }

        Player.transform.position = PlayerLock.transform.position;
        Player.Model.transform.rotation = PlayerLock.transform.rotation;
    }

    public void UnlockPlayer()
    {
        CoasterRenderer.material = OriginalMaterial;
        Player.Unlock();
    }


}
