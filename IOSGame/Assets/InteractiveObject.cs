using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
{
    public virtual void OnInteract()
    {
        Debug.Log(name);
    }
}
