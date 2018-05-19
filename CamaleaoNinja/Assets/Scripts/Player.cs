using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    public float velocity = 2f;
    public float velocityRotation = 0.15F;
    public float MaxStamina;
    public float TongueDistance;
    public int Lifes;

    public Transform cam;

    private float _currentStamina;
    private Vector3 _lastCheckpoint;
    private Vector3 _controlRight;
    private Vector3 _controlForward;



    private void Start()
    {
        _controlRight = Vector3.Cross(cam.up, cam.forward);
        _controlForward = Vector3.Cross(cam.right, Vector3.up);
    }

    private void Update()
    {
        move();
    }

    public void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * velocity * Time.deltaTime * -1;
        float moveVertical = Input.GetAxis("Vertical") * velocity * Time.deltaTime * -1;

        Vector3 movement = (_controlRight * moveHorizontal) + (_controlForward * moveVertical);

        Vector3 targetDirection = new Vector3(moveHorizontal, 0f, moveVertical);
        targetDirection = cam.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        // Quaternion.LookRotation(cam.forward,cam.up)
        //transform.rotation = Quaternion.LookRotation(movement);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y, transform.localEulerAngles.z);
        //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

        transform.Translate(movement);
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

    
