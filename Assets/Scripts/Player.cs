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
    Rect staminaRect;
    Texture2D staminaTexture;
    //public Texture2D staminaTexture;


    public Text vidasText;
    public Transform cam;

    private float _currentStamina;
    private Vector3 _lastCheckpoint;
    private Quaternion tongue_direction;

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
        if(!GetComponent<ChangeColor>().IsCamuflado()) //Verifica se o player está camuflado. Se estiver ele não se move
            move();
        StaminaUpdate();
    }

    public void move()
    {
        //reading the input:
        float horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalAxis = CrossPlatformInputManager.GetAxis("Vertical");

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
        Lifes -= 1;
        vidasText.text = "Vidas: " + Lifes;
    }

    // respawn até o ultimo respawn
    private void respawn()
    {

    }

    private void StaminaUpdate()
    {
        if (GetComponent<ChangeColor>().IsCamuflado())
        {
            stamina -= Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                GetComponent<ChangeColor>().SetCamufla(false);
            }
        }else if (stamina < MaxStamina)
        {
            stamina += Time.deltaTime;
        }

    }

    private void death()
    {

    }
    
    void OnGUI()
    {
        float ratio = stamina / MaxStamina;
        float rectWidth = ratio*Screen.width / 5;
        staminaRect.width = rectWidth;
        GUI.DrawTexture(staminaRect, staminaTexture);
    }


}

    
