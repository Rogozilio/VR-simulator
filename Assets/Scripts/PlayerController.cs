using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public Transform head;
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
        //Vector3 targetHeadPosition = head.position;
        //head.position = new Vector3(transform.position.x, head.position.y, transform.position.z);
        //transform.position = new Vector3(targetHeadPosition.x, transform.position.y, targetHeadPosition.z);

        //Vector3 headMovement = new Vector3(targetHeadPosition.x, 0, targetHeadPosition.z) - new Vector3(transform.position.x, 0, transform.position.z);
        //if ((new Vector2(headMovement.x, headMovement.z)).magnitude > 0.1)
        //{
        //    characterController.Move(speed * Time.deltaTime * headMovement);
        //}

        //Vector3 direction = new Vector3(0, 0, 0);

        if (input.axis.magnitude > 0.1f)
        {
            //Vector3 locomotion = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            //head.position += speed * Time.deltaTime * locomotion;
            //direction += locomotion;
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            //transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction,Vector3.up);
            //characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
            //scada.transform.position = speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }
        /*else
        {
            //Vector3 headMovement = new Vector3(head.position.x, 0, head.position.z) - new Vector3(characterController.transform.position.x, 0, characterController.transform.position.z);
            //direction += headMovement;
        }*/



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

    /*void FixedUpdate()
    {
        //float distanceFromFloor = Vector3.Dot(head.localPosition, Vector3.up);
        //characterController.height = Mathf.Max(characterController.radius, distanceFromFloor);
        //transform.localPosition = new Vector3(transform.localPosition.x, (head.localPosition - 0.5f * distanceFromFloor * Vector3.up).y, transform.localPosition.z);
    }*/
}
