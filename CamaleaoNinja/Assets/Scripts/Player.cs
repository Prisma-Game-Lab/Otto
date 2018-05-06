using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    float Velocidade = 2f;

    private void Start()
    {
        
    }

    private void Update()
    {
        
        transform.Translate(Input.GetAxis("Horizontal") * Velocidade * Time.deltaTime, 0f, Input.GetAxis("Vertical") * Velocidade * Time.deltaTime);
    }

}

    
