using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Water");
    }
}
