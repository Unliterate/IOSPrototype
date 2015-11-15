using UnityEngine;
using System.Collections;

public class DescructibleObject : InteractiveObject
{
    public override void OnInteract()
    {
        base.OnInteract();

        Destroy(gameObject);

    }
}
