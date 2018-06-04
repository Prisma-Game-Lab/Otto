using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public Vector3 chk_pt;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") // Checa se o Collider é o player
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.green;

            chk_pt = this.transform.position;
            Renderer[] rend_child = GetComponentsInChildren<Renderer>();
            foreach(Renderer child in rend_child)
            {
                //Debug.Log(child.name);
                child.GetComponent<Renderer>().material.color = Color.green;
                GameManager.respawn_point = chk_pt; 
            }
        }
    }

}