using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[AddComponentMenu("Cinemachine/Impulse Listener")]
public class CinemachineImpulseListener : MonoBehaviour
{
    public float Gain = 1f;
    public bool Use2DDistance = false;

    private void Reset()
    {
        Gain = 1f;
        Use2DDistance = false;
    }

    // Add more implementation as needed based on your use case
}
