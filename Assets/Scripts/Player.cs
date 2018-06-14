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
    public float stamina;
    public float MaxStamina = 20;
    public float TongueDistance;
    public int Lifes = 3;
    public float AttackCooldown = 3.0f;
    public float BlinkingCooldown = 0.5f;
    Rect staminaRect;
    Texture2D staminaTexture;
    //public Texture2D staminaTexture;


    public Text vidasText;
    public Transform cam;

    private float _currentStamina;
    private Vector3 _lastCheckpoint;
    private Quaternion tongue_direction;
    private bool _wasAttacked = false;
    private bool _opaque = true;


    private void Start()
    {
        stamina = MaxStamina;
        staminaRect = new Rect(Screen.width / 10, Screen.height * 9 /10, Screen.width / 3, Screen.height / 20);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.green);
        staminaTexture.Apply();

        vidasText.text = "Vidas: " + Lifes;
       
    }

    private void Update()
    {
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), Time.deltaTime * velocityRotation);
        }

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
            if (child.tag == "Tongue")
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
        }
    }

    private void StaminaUpdate()
    {
        if (GetComponent<ChangeColor>().IsCamuflado())  //stamina sendo gasta pois esta camuflado
        {
            stamina -= Time.deltaTime;
            if (stamina < 0)  //stamina nao pode ser negativa
            {
                stamina = 0;
                GetComponent<ChangeColor>().SetCamufla(false);
            }
        }else if (stamina < MaxStamina) //stamina recuperando
        {
            stamina += Time.deltaTime;
        }

    }

    void OnGUI()
    {
        float ratio = stamina / MaxStamina;
        float rectWidth = ratio*Screen.width / 5;
        staminaRect.width = rectWidth;
        GUI.DrawTexture(staminaRect, staminaTexture);
    }


    // IEnumerator funciona a parte do tempo de compilação e nesse caso ele vai fazer o plyaer piscar quando for atacado
    IEnumerator BlinkingPlayer()
    {
        print("ENTREI NA COROTINA");
        Color alphaColor = this.GetComponent<MeshRenderer>().material.color;

        // Roda esse código até o tempo de AttackCooldown acabar
        for (float j = AttackCooldown; j >= 0; j -= Time.deltaTime)
        {
            alphaColor.a = 0;
            this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
            yield return new WaitForSeconds(BlinkingCooldown);
            alphaColor.a = 1;
            this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, alphaColor, BlinkingCooldown * Time.deltaTime);
            yield return new WaitForSeconds(BlinkingCooldown);

            /*

            print("TO RODANDO A COROTINA");
            if (_opaque)
            {
                alphaColor.a = 0;

                // Diminui o alpha aos poucos
                for (float i = BlinkingCooldown; i >= 0; i -= Time.deltaTime)
                {
                    this.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, i);
                }

                yield return new WaitForSeconds(BlinkingCooldown);
                //yield WaitForSeconds(0.2);


                _opaque = true;

                // faz essa transformação durante o BlinkingCooldown
                for (float i = BlinkingCooldown; i >= 0; i -= Time.deltaTime)
                {
                    // set color with i as alpha
                    this.GetComponent<MeshRenderer>().material.color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, 0, BlinkingCooldown * Time.deltaTime);
                    img.color = new Color(1, 1, 1, i);
                    yield return null;
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

    
