using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Tongue : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {

    }
    void Start () {
        GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
    } 

    public void ShowTongue(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        checkObject(other);
    }

    // Se soltar o botão enquanto estiver tocando o objeto ele irá puxar o objeto ou ser puxado
    // Se ele largar fora do objeto nada acontece
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Saí de objeto");
    }

    private void OnTriggerStay(Collider other)
    {
        checkObject(other);
    }

    private void checkObject(Collider other)
    {
        if (other.tag == "Objeto Interagível")
        {
            InteragibleObjects obj = other.GetComponent<InteragibleObjects>();

            if (obj.Fixed == false)
            {
                obj.pullObject(this.transform.parent);
            }
            else if (obj.Fixed == true)
            {
                obj.pullPlayer(this.transform.parent);
            }
        }
    }


}
