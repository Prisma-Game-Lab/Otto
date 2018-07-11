using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Tongue : MonoBehaviour {

    Transform intObject = null;
    Vector3 aux;

    public Animator anim;

    [HideInInspector]
    public static bool Agarrado;
    private GameObject coliderLingua;

    // Use this for initialization
    void Awake()
    {
    }
    void Start () {
        coliderLingua = GameObject.Find("coliderLingua");
        Agarrado = false;
    }
    
    // Update is called once per frame
    void Update () {
        if (intObject != null){
            print(intObject);   
        }
    } 

    public void ShowTongue(bool active)
    {
        if (active == false)
        {
            freeTongue();
            anim.SetBool("dentroBoca", false);
            
        } else {
            anim.SetBool("dentroBoca", true);
        }
    }

    private void freeTongue() {
        
        if (intObject != null)
        {
            if (intObject.tag == "Objeto Interagível")
            {
                InteragibleObjects obj = intObject.GetComponent<InteragibleObjects>();

                if (obj.Fixed == false) //&& obj.isFollowingPlayer
                {
                    //print("liberou ");
                    obj.freeObjectFromtongue();
                    intObject = null;
                    Agarrado = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objeto Interagível"){
            intObject = other.transform;
            checkObject(other);
            //aux = intObject.localScale;
            Agarrado = true;
        }
    }

    // Se soltar o botão enquanto estiver tocando o objeto ele irá puxar o objeto ou ser puxado
    // Se ele largar fora do objeto nada acontece
    private void OnTriggerExit(Collider other)
    {
        intObject = null; 
        //intObject.localScale = aux;
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
                obj.pullObject(this.transform.parent); //plyer.transform.parent
            }
        }
    }


}
