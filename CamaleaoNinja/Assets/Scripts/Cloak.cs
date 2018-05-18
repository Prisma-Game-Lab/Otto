using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloak : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		Colored colScript = other.gameObject.GetComponent<Colored>();
		ChangeColor changeColorScript = GetComponentInParent<ChangeColor>();
		if (colScript != null)
		{
			print("cloak do item " + changeColorScript.gameObject.name + " encostou em " + colScript.gameObject.name);

			if (colScript.ganhaCor)
			{
				changeColorScript.AddColor(colScript.cor);
			}
			else
			{
				changeColorScript.OnCollisionEnterCor(colScript);
			}
		}
        
    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {


        Colored corScript = other.gameObject.GetComponent<Colored>();
		if (corScript != null)
		{
			gameObject.GetComponentInParent<ChangeColor>().OnCollisionExitCor(corScript);
		}
    }

}
