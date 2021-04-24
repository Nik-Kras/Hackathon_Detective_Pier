using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour, IPointerClickHandler 
{
    public Dialogue startDialogue;
    public string npcName;
    public Sprite portret;
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        DialogueManager.instance.StartDialogue(npcName, portret, startDialogue);
        Debug.Log(name + " Game Object Clicked!");
    }
}
