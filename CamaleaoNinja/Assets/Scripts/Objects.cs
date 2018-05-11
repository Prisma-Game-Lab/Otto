using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour {

    enum Colors { Red, Green, Blue }; 

    public GameObject Player;

    private Colors _currentColor;

	// Use this for initialization
	void Start () {
        // Tem que ser publica essa setagem
        _currentColor = Colors.Green;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string getCurrentColor()
    {
        if (_currentColor == Colors.Green)
            return "Verde";
        return "";
    }
    void checkPlayerContact()
    {

    }
}
