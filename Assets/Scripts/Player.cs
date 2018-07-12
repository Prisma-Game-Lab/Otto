using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    public float velocity = 4f;
    public float velocityRotation = 5F;
    public float TongueDistance;
    public int Lifes = 3;
    public int AttackSeconds = 5;
    public float BlinkingCooldown = 1f;

    //Referencia para o ColorRing que esta no canvas que esta cena
    public StaminaRock staminaHandler;
    public float stamina;
    public float maxStamina = 20;


    public Text vidasText;
    public Transform cam;
    public Animator playerAnim;

    private float _currentStamina;
    private Vector3 _lastCheckpoint;
    private Quaternion tongue_direction;
    private bool _wasAttacked = false;
    private bool _opaque = true;

    private GameObject plyr;
    private Rigidbody rb_std;
    public float rb_new = 100000;
    public GameObject gameManager;
    public GameManager _gameManagerScript;

    private void Start()
    {
        stamina = maxStamina;

        vidasText.text = "Vidas: " + Lifes;

        plyr = GameObject.Find("Player");
        rb_std = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager");
        _gameManagerScript = gameManager.GetComponent<GameManager>();

    }

    private void Update()
    {
        if (GetComponent<ChangeColor>().IsCamuflado())
        {
            rb_std.mass = rb_new;
        }
        else
            move();
        StaminaUpdate();
    }

    public void move()
    {
        //reading the input:
        float horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalAxis = CrossPlatformInputManager.GetAxis("Vertical");

        //print("vertical " + verticalAxis + " horizontal " + horizontalAxis);

        //camera forward and right vectors:
        var forward = cam.forward;
        var right = cam.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;
        desiredMoveDirection.Normalize();
        
        transform.Translate(desiredMoveDirection * velocity * Time.deltaTime, Space.World);
        

        if (desiredMoveDirection != Vector3.zero)
        {
            playerAnim.SetBool("moving", true);
            if (Tongue.Agarrado == false)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), Time.deltaTime * velocityRotation);
            }   
        } else playerAnim.SetBool("moving", false);



    }

    public void cloak()
    {
        print("Estou invisível");
    }

    public void tongue(bool active)
    {
        Transform[] children = gameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == "Bone004")
            {
                child.GetComponent<Tongue>().ShowTongue(active);
            }
        }
    }

    public void loseLife()
    {
        if (!_wasAttacked)
        {
            Lifes -= 1;
            vidasText.text = "Vidas: " + Lifes;

            // Se o Player for atacado ele vai dar um salto para trás e piscar durante x segundos
            _wasAttacked = true;
            StartCoroutine("BlinkingPlayer");
            _gameManagerScript.Lose();
            //_gameManagerScript.respawn();
        }
    }

	private void StaminaUpdate()
    {
        if (GetComponent<ChangeColor>().IsCamuflado())  //stamina sendo gasta pois esta camuflado
        {
            stamina -= Time.deltaTime;
            if (stamina <= 0)  //stamina nao pode ser negativa
            {
				print("ih! acabou a tinta");
                stamina = 0;
                GetComponent<ChangeColor>().SetCamufla(false);
            }
        }
        else if (stamina < maxStamina) //stamina recuperando
        {
            stamina += Time.deltaTime;
        }
        staminaHandler.UpdateStamina(stamina, maxStamina);
    }


    // IEnumerator funciona a parte do tempo de compilação e nesse caso ele vai fazer o plyaer piscar quando for atacado
    IEnumerator BlinkingPlayer()
    {
        Color alphaColor = this.GetComponent<MeshRenderer>().material.color;

        for (int t = 0; t < AttackSeconds; t += (int)(2 * BlinkingCooldown))
        {
            print(Time.deltaTime);

            alphaColor.a = 0;
            foreach (Transform child in this.GetComponentInChildren<Transform>())
            {
                if (child.tag == "Player Model")
                {
                    foreach (Transform mr in child.GetComponentInChildren<Transform>())
                    {
                        mr.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
                    }
                }
            }
            //this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
            yield return new WaitForSeconds(BlinkingCooldown);
            print("apaguei");
            alphaColor.a = 1;
            foreach (Transform child in this.GetComponentInChildren<Transform>())
            {
                if (child.tag == "Player Model")
                {
                    foreach (Transform mr in child.GetComponentInChildren<Transform>())
                    {
                        mr.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
                    }
                }
                //this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);=
            }
            yield return new WaitForSeconds(BlinkingCooldown);
            print("acendi");
            // Roda esse código até o tempo de AttackCooldown acabar
            /*for (float j = AttackCooldown; j >= 0; j -= Time.deltaTime)
            {
                alphaColor.a = 0;
                this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
                new WaitForSeconds(BlinkingCooldown);
                alphaColor.a = 1;
                this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
                new WaitForSeconds(BlinkingCooldown);



                print("TO RODANDO A COROTINA");
                if (_opaque)
                {
                    alphaColor.a = 0;

                    // Diminui o alpha aos poucos
                    for (float i = BlinkingCooldown; i >= 0; i -= Time.deltaTime)
                    {
                        this.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, i);
                    }

                }
                else // Coloca cor no player
                { 

                    // Aumenta o alpha aos poucos
                    for (float i = 0; i >= BlinkingCooldown; i -= Time.deltaTime)
                    {
                        this.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, i);
                    }

                    _opaque = false;
                    alphaColor.a = 1;
                    this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
                    yield return null;
                }*/

        }

        _wasAttacked = false;
        print("SAI DA COROTINA");
        yield return null;

    }


}

    
