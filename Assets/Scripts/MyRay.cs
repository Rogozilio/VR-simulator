using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRay : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 SeconPoint;

    private void Start()
    {
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
    }
    //public Transform target;
    //Ray ray;
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray (transform.position, transform.forward);
        lineRenderer.SetPosition(0, transform.position);
        SeconPoint = new Vector3(transform.position.x+1*Mathf.Sin(transform.rotation.x), transform.position.y + 1 * Mathf.Sin(transform.rotation.y), transform.position.z + 1 * Mathf.Sin(transform.rotation.z));
        lineRenderer.SetPosition(1, SeconPoint);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawLine(ray.origin, hitInfo.point);
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 2, false);
            Debug.Log("red");
        }
        else
        {
            //Gizmos.color = Color.green;
            //Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 100);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green, 2, false);
            Debug.Log("green");
        }
    }
}
