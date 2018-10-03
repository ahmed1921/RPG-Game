using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerControl : MonoBehaviour {
    [SerializeField] float walMoveStopRadius = 0.2f;
    [SerializeField] float AttackMoveRadius = 5f;

    bool isInDirectMode =false;
    ThirdPersonCharacter ThirdPersonController;
    CameraRaycaster cameraRayCaster;
    Vector3 currentDestination, clickPoint;

	// Use this for initialization
	void Start () {
        cameraRayCaster = Camera.main.GetComponent<CameraRaycaster>();
        ThirdPersonController = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
	}
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.G)) //G for gamepad. TODO allow player to map later or add to menu.
        {
            isInDirectMode = !isInDirectMode;
            currentDestination = transform.position;
        }
        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessIndirectMovement();
        }
    }
    private void ProcessDirectMovement()
    { 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
       Vector3 CameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
       Vector3  Movement = v * CameraForward + h * Camera.main.transform.right;
        ThirdPersonController.Move(Movement, false, false);

    }

    private void ProcessIndirectMovement()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRayCaster.hit.point;
            print("Cursor Raycast hit" + cameraRayCaster.CurrentLayer);

            switch (cameraRayCaster.CurrentLayer)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walMoveStopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, AttackMoveRadius);
                    break;
                default:
                    print("Inside Default");
                    break;
            }


        }
        WalDestination();
    }

    private void WalDestination()
    {
        Vector3 playertoClickPoint = currentDestination - transform.position;
        if (playertoClickPoint.magnitude >= 0)
        {
            ThirdPersonController.Move(playertoClickPoint, false, false);
        }
        else
        {
            ThirdPersonController.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination,float shortening)
    {
        Vector3 reductionvector = (destination - transform.position).normalized * shortening;
        return destination - reductionvector;

    }


    void OnDrawGizmos()
    {
        //Draw movement Gizmos
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination,0.1f);
        Gizmos.DrawSphere(clickPoint, 0.15f);

        //Draw Attack Gizmos
        Gizmos.color = new Color(1,1,1);
        Gizmos.DrawWireSphere(transform.position, AttackMoveRadius);

    }
}

