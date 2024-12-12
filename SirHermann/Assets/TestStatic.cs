using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestStatic : MonoBehaviour
{
    private static int a = 0;
    void Start()
    {
        //Debug.Log($"{a}");
        a++;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoad()
    {
        a = 0;
    }
}
