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

    [SerializeField]
    private Animator _animator;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        DialogueManager.instance.StartDialogue(npcName, portret, startDialogue, _animator);
    }
}
