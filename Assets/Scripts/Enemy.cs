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

    private Rigidbody _rb;

    // Use this for initialization
    void Start () {
		//transform.Rotate(0, 180, 0);
		gameObject.layer = 11;
        /*
         * layer 9 e a do player quando nao esta camuflado
         * layer 11 e a layer dos enemies
         * layer 12 e a do player quando ele esta camuflado.
         * essas duas layers nao colidem uma com a outra
         * 
         **/
        _rb = GetComponent<Rigidbody>();
    }
	
	// FixedUpdate para mover o inimigo com RigidBody
	void FixedUpdate () {

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
            {
                print("Player esta camuflado :P");
            }
            else
            {
                // chase player
                GetComponentInParent<Patrol>().GotoPlayer();
            }

		}
    }
    //public void OnTriggerEnter(Collider col)
    //{
    //    ChangeColor player = col.GetComponent<ChangeColor>();
    //    if (player != null)
    //        if (col.tag == "Player" && !player.IsCamuflado())
    //        {
    //            print("Colidiu");
    //        }
    //}
}
