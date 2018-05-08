using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    public float Velocidade = 2f;

    private void Start()
    {

    }

    private void Update()
    {
        move();

    }

    public void move()
    {
        transform.Translate(-1 * Input.GetAxis("Vertical") * Velocidade * Time.deltaTime, 0f, Input.GetAxis("Horizontal") * Velocidade * Time.deltaTime);
    }



}

    
