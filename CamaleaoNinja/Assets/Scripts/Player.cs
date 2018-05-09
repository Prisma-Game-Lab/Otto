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
        transform.Translate(Input.GetAxis("Horizontal") * velocity * Time.deltaTime, 0f, Input.GetAxis("Vertical") * velocity * Time.deltaTime);
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

    private void cloak()
    {

    }

    private void tongue()
    {

    }

    private void death()
    {

    }



}

    
