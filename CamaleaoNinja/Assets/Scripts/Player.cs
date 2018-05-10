using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    enum Colors {Red, Green, Blue};

    [SerializeField]
    public float velocity = 2f;
    public float MaxStamina;
    public float TongueDistance;
    public int Lifes;

    private Colors[] _allowedColors;
    private float _currentStamina;
    private Colors _currentColor;
    private Vector3 _lastCheckpoint;


    private void Start()
    {

    }

    private void Update()
    {
        move();

    }

    public void move()
    {
        transform.Translate(-1*Input.GetAxis("Horizontal") * velocity * Time.deltaTime, 0f, -1*Input.GetAxis("Vertical") * velocity * Time.deltaTime);
    }

    public void cloak()
    {

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

    private void addColor()
    {

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

    
