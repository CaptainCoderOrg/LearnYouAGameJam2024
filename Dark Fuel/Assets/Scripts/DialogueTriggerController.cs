using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerController : MonoBehaviour
{
    public LevelController LevelController;
    public DialogueController Dialogue => LevelController.HUD.Dialogue;
    public string Message;
    public void Awake()
    {
        LevelController = FindFirstObjectByType<LevelController>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.tag == "Player")
        {
            Dialogue.Show(Message);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody.tag == "Player")
        {
            Dialogue.Hide();
        }
    }
}
