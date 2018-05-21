using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    public float velocity = 4f;
    public float velocityRotation = 5F;
    public float MaxStamina;
    public float TongueDistance;
    public int Lifes;

    public Transform cam;

    private float _currentStamina;
    private Vector3 _lastCheckpoint;
    private Vector3 _controlRight;
    private Vector3 _controlForward;
    private Quaternion tongue_direction;

    private void Start()
    {
    }

    private void Update()
    {
        move();
    }

    public void move()
    {
        //reading the input:
        float horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalAxis = CrossPlatformInputManager.GetAxis("Vertical");

        //camera forward and right vectors:
        var forward = cam.forward;
        var right = cam.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;
        transform.Translate(desiredMoveDirection * velocity * Time.deltaTime, Space.World);

        if (desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), Time.deltaTime * velocityRotation);
        }


        //if (desiredMoveDirection != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.Slerp(
        //        transform.rotation,
        //        Quaternion.LookRotation(desiredMoveDirection),
        //        Time.deltaTime * velocityRotation
        //    );
        //}
    }

    public void cloak()
    {
        print("Estou invisível");
    }

    public void tongue(bool active)
    {
        Transform[] children = gameObject.transform.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.tag == "Tongue")
            {
                child.GetComponent<Tongue>().ShowTongue(active);
            }
        }
    }

    private void loseLife()
    {

    }

    // respawn até o ultimo respawn
    private void respawn()
    {

    }

    private void loseStamina()
    {

    }

    private void gainStamina()
    {

    }

    private void death()
    {

    }


}

    
