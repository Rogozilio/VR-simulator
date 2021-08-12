using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag : MonoBehaviour
{
    public GameObject MapCase;
    public GameObject Camera;
    private Vector3 offset;
    private Quaternion oldRotation;
    public SCADA_Controller SCADA_Controller;

    public bool OnBag = false;

    void Start()
    {
        offset = transform.position - Camera.transform.position;
    }

    private void Awake()
    {
        MapCase.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void MakePhysical()
    {
        MapCase.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "MapCase")&&(SCADA_Controller.onHand==false))
        {
            Debug.Log("other.tag " + other.tag);
            MapCase.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            oldRotation = transform.rotation;
            MapCase.transform.rotation = Quaternion.Euler(oldRotation.x, oldRotation.y, 0);
            //MapCase.transform.parent = transform;
            OnBag = true;
        }
        
    }

    void Update()
    {
        transform.position = Camera.transform.position + offset;
        transform.eulerAngles = new Vector3(0, Camera.transform.eulerAngles.y + 180, 0);
    }
}
