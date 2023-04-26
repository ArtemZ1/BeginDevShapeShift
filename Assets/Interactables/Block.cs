using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Interactable
{
    protected override void Interact()
    {
        print("Interacted with " + gameObject.name);
    }
}
