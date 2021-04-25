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

    [SerializeField]
    private Animator _animator;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Dialogue selectedDialogue = startDialogue;
        if (DialogueManager.instance.events.Contains(eventConditionDialogue))
            selectedDialogue = conditionalDialogue;
        else
            selectedDialogue = startDialogue;
            
        DialogueManager.instance.StartDialogue(npcName, portret, selectedDialogue, _animator);
    }
}
