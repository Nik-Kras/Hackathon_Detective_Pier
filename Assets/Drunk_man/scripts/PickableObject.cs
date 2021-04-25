using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableObject : MonoBehaviour, IPointerClickHandler 
{
    [SerializeField] private string[] _objectKeys;
    [SerializeField] private Dialogue _dialogue;
    
    public string objectName;
    public Sprite objectPortret;
    
    void Pickup()
    {
        foreach (var objectKey in _objectKeys)
        {
            Inventory.instance.OnPickup(objectKey);
        }

        if (_dialogue)
            DialogueManager.instance.StartDialogue(objectName, objectPortret, _dialogue);

        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Pickup();
    }
}
