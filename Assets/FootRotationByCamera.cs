using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootRotationByCamera : MonoBehaviour
{
    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, Camera.transform.eulerAngles.y + 180, 0);
    }
}
