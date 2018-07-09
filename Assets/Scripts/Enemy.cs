using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour {

    public float VisionDistance;
    public float TimeReaction;
    public float Attackvelocity;
    public float PontaEsquerda = 14;
    public float PontaDireita = -14.71f;
    public int _Direction = 1;

    private Rigidbody _rb;

    public GameObject Player;
    private Player _playerScript;

    // Use this for initialization
    void Start () {

        _playerScript = Player.GetComponent<Player>();

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

        // Inimigo nao colide com player se ele estiver camuflado
        //if (Player.GetComponent<ChangeColor>().IsCamuflado())
        //{
        //    _rb.detectCollisions = false;
        //} else
        //{
        //    _rb.detectCollisions = true;
        //}

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


    /* OnCollision... - Verifica se algum objeto tocou no collider do inimigo */

    private void OnCollisionEnter(Collision collision)
    {
		/* Se existe gameObject na variável publica "Player" do inimigo,
        * verifica se o GameObject do colider que encostou no inimigo (collision) possui a tag Player (para ver se foi o Player que encostou nele),
        * caso seja o player verifica se ele não está camuflado, se ele estiver camuflado ele não pode sofrer um ataque. 
        * Caso tudo seja verdade, o player perde uma vida e seja a ultima vida do player, chama a tela de gameOver  
        */
		print("Player null? " + (Player == null));
        if (Player != null)
        {
            if (collision.gameObject.tag == "Player" && !Player.GetComponent<ChangeColor>().IsCamuflado())
            {
                _playerScript.loseLife();
                if (_playerScript.Lifes == 0)
                {
                    SceneManager.LoadScene("LoseScene");
                }
            }
        }
        
    }

    private void OnCollisionStay(Collision collision){}

    private void OnCollisionExit(Collision collision){}
}
