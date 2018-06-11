using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visao_color : MonoBehaviour {

    public Color _AlertColor;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            /*Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.red;*/

            Renderer[] rend_child = GetComponentsInChildren<Renderer>();
            foreach (Renderer child in rend_child)
            {
                //Debug.Log(child.name);
                child.GetComponent<Renderer>().material.color = Color.red;
            }

        }
    }
}