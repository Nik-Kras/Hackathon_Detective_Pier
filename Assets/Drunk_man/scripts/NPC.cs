using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour, IPointerClickHandler 
{
    public Dialogue startDialogue;
    public string npcName;
    public Sprite portret;

    public Dialogue conditionalDialogue;
    public string eventConditionDialogue;

    public Dialogue conditionalDialogue2;
    public string eventConditionDialogue2;

    [SerializeField]
    private Animator _animator;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        DialogueManager.instance._clickPlay.Play();
        
        Dialogue selectedDialogue;
        
        if (DialogueManager.instance.events.Contains(eventConditionDialogue))
            selectedDialogue = conditionalDialogue;
        else if (DialogueManager.instance.events.Contains(eventConditionDialogue2))
            selectedDialogue = conditionalDialogue2;
        else
            selectedDialogue = startDialogue;
            
        DialogueManager.instance.StartDialogue(npcName, portret, selectedDialogue, _animator);
    }
}
