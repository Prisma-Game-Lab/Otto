using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Tongue : MonoBehaviour {

    Transform intObject = null;

    public Animator anim;
    private GameObject player;

    [HideInInspector]
    public static bool Agarrado;
    private Collider coliderLingua;

    // Use this for initialization
    void Awake()
    {
    }
    void Start () {
        coliderLingua = GetComponent<Collider>();
        player = GameObject.Find("Player");
        Agarrado = false;
    }
    
    // Update is called once per frame
    void Update () {
        if (intObject != null){
            //print(intObject);   
        }
    } 

    public void ShowTongue(bool active)
    {
        if (active == false)
        {
            coliderLingua.enabled = false;
            freeTongue();
            anim.SetBool("dentroBoca", false);

            
        } else {
            coliderLingua.enabled = true;
            anim.SetBool("dentroBoca", true);
        }
    }

    private void freeTongue() {
        
        if (intObject != null)
        {
            if (intObject.tag == "Objeto Interagível")
            {
                InteragibleObjects obj = intObject.GetComponent<InteragibleObjects>();

                if (obj != null && obj.isFollowingPlayer && obj.Fixed == false  ) //
                {
                    print("liberou ");
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
            Agarrado = true;
        }
    }

    // Se soltar o botão enquanto estiver tocando o objeto ele irá puxar o objeto ou ser puxado
    // Se ele largar fora do objeto nada acontece
    private void OnTriggerExit(Collider other)
    {
        intObject = null; 
        
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
                obj.pullObject(player.transform);
            }
        }
    }


}
