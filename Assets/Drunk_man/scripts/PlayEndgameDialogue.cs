using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndgameDialogue : MonoBehaviour
{
    [SerializeField]
    private Dialogue _dialogue;
    
    public void PlayEndGameDialogue()
    {
        DialogueManager.instance.StartDialogue("Boss", null, _dialogue);
    }
}
