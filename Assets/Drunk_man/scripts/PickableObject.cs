using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public string objectKey;
    
    void Pickup()
    {
        Inventory.instance.objectKeys.Add(objectKey);
        Destroy(gameObject);
    }
}
