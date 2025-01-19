using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrail : MonoBehaviour
{
    public GameObject trailObject;
    public bool isEffective = false;
    // Start is called before the first frame update
    void Start()
    {
        trailObject.GetComponent<TrailRenderer>().enabled = isEffective;
    }

    // Update is called once per frame
    public void TrailOn()
    {
        isEffective = !(isEffective);
        trailObject.GetComponent<TrailRenderer>().enabled = isEffective;

    }

    void Update()
    {
        trailObject.transform.position = Input.mousePosition;
    }
}
