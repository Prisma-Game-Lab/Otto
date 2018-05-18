using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float VisionDistance;
    public float TimeReaction;
    public float Attackvelocity;
    public float PontaEsquerda = 14;
    public float PontaDireita = -14.71f;
    public int _Direction = 1;
    public float velocidade = 5;

    private Rigidbody _rb;

    // Use this for initialization
    void Start () {
        transform.Rotate(0, 180, 0);
        _rb = GetComponent<Rigidbody>();
    }
	
	// FixedUpdate para mover o inimigo com RigidBody
	void FixedUpdate () {
        if (_Direction == 1)
        {
            transform.Translate(Vector3.right * velocidade * Time.deltaTime, Space.World);
            _Direction = 1;
        }
        if (this.transform.position.x > PontaEsquerda)
        {
            _Direction = 2;
            transform.Rotate(0, 180, 0);
        }
        if (_Direction == 2)
        {
            transform.Translate(Vector3.right * -velocidade * Time.deltaTime, Space.World);
            _Direction = 2;
        }
        if (this.transform.position.x < PontaDireita)
        {
            _Direction = 1;
            transform.Rotate(0, 180, 0);
        }
    }

    void setPath()
    {

    }

    public void AttackPlayer()
    {
		Player[] components = FindObjectsOfType<Player>();
		ChangeColor player ;
		foreach (Player p in components)
		{
			player = p.GetComponent<ChangeColor>();
			if (player.IsCamuflado())
				print("Player esta camuflado :P");
			else
				print("Player Morreu =(");
		}
    }
    public void OnTriggerEnter(Collider col)
    {
		//ChangeColor player = col.GetComponent<ChangeColor>();
		//if(player!=null)
		  //  if (col.tag == "Player" && !player.IsCamuflado())
              //  print("Player Morreu =(");
    }
}
