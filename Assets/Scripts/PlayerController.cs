using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;
    //public GameObject scada;
    //public SteamVR_Action_Boolean y_button;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    //private bool y_btn;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        //y_btn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            //transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction,Vector3.up);
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
            //scada.transform.position = speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
        }

        /*if (y_button.GetState(inputSource))
        {
            Debug.Log("Success!");
        }*/
        //bool st = y_button.GetState(SteamVR_Input_Sources.Any);
        //if (st)
        //{
        //    scada.SetActive(true);
        //}
        //else
        //{
        //    scada.SetActive(false);
        //}
    }
}
