using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRay : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject FakePoint;
    private Vector3 addition;
    bool RayHitManometr;
    Vector3 PositionForPanel;
    public GameObject Panel;
    private ManometrData MD;
    public Quaternion Rot;
    //public GameObject Player;

    //Gradient gradient;
    //GradientColorKey[] colorKey;
    //GradientAlphaKey[] alphaKey;
    private float timer;

    private void Start()
    {
        {
            addition = new Vector3(0.05f, -0.05f, 0);
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
            timer = 0f;
            RayHitManometr = false;
            //lineRenderer.startColor = Color.green;
            //lineRenderer.endColor = Color.red;
            //gradient = new Gradient();
            //colorKey = new GradientColorKey[2];
            //colorKey[0].color = Color.red;
            //colorKey[0].time = 0.0f;
            //colorKey[1].color = Color.blue;
            //colorKey[1].time = 1.0f;

            //alphaKey = new GradientAlphaKey[2];
            //alphaKey[0].alpha = 1.0f;
            //alphaKey[0].time = 0.0f;
            //alphaKey[1].alpha = 0.0f;
            //alphaKey[1].time = 1.0f;

            //gradient.SetKeys(colorKey, alphaKey);

            //lineRenderer.colorGradient = gradient;

        }
    }
    //public Transform target;
    //Ray ray;
    // Update is called once per frame
    void Update()
    {
        if (RayHitManometr)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }

        var direction = (transform.position - FakePoint.transform.position) / (transform.position - FakePoint.transform.position).magnitude;

        Ray ray = new Ray (transform.position + addition, direction);
        lineRenderer.SetPosition(0, transform.position + addition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1, false);
            if (hitInfo.collider.CompareTag("Manometr"))
            {
                Debug.Log("Found manometr");
                RayHitManometr = true;
                lineRenderer.SetPosition(1, hitInfo.point);
                lineRenderer.enabled = true;
                MD = hitInfo.collider.gameObject.GetComponent<ManometrData>();
                if (timer >= 2f)
                {
                    if (!MD.hasPannel)
                    {
                        PositionForPanel = hitInfo.collider.gameObject.transform.position + new Vector3(0, 0.3f, 0);
                        Debug.Log(PositionForPanel);

                        Rot.eulerAngles = new Vector3(hitInfo.collider.gameObject.transform.rotation.x + MD.xAdd, hitInfo.collider.gameObject.transform.rotation.y + MD.yAdd, hitInfo.collider.gameObject.transform.rotation.z + MD.zAdd);
                        //Rot = new Quaternion(hitInfo.collider.gameObject.transform.rotation.w, hitInfo.collider.gameObject.transform.rotation.x, hitInfo.collider.gameObject.transform.rotation.y + MD.AdditionRotation.y, hitInfo.collider.gameObject.transform.rotation.z + MD.AdditionRotation.z);
                        GameObject panel = Instantiate(Panel, PositionForPanel, Rot);
                        PanelController PC = panel.GetComponent<PanelController>();
                        PC.Pressure = MD.Pressure;
                        PC.MD = MD;
                        hitInfo.collider.gameObject.GetComponent<ManometrData>().hasPannel = true;
                    }
                }
            }
            else
            {
                lineRenderer.enabled = false;
                RayHitManometr = false;
            }
        }
        else
        {
            lineRenderer.enabled = false;
            RayHitManometr = false;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green, 2, false);
        }
    }
}
