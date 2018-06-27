using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{


    private List<Colored> _coloredEmContato = new List<Colored>();
                      /* A forma como trocamos a cor foi mudada
                    * Troca a cor atraves de um material:
                    * Essa e a cor forma nova
                    * */
    private Color _defaultMaterial;
                      /*
                    * A forma como trocamos a cor foi mudada
                    * Troca a cor atraves de um material:
                    * Essa e a cor forma antiga
                    * */
   // private List<Material> _defaultMaterial = new List<Material>();
    private List<Colored.Corenum> _coresDisponiveis = new List<Colored.Corenum>();

    public GameObject model;

    // Use this for initialization
    void Start()
    {



        gameObject.layer = 9;
        /*
         * layer 9 e a do player quando nao esta camuflado
         * layer 11 e a layer dos enemies
         * layer 12 e a do player quando ele esta camuflado.
         * essas duas layers nao colidem uma com a outra
         * 
         **/
        /* A forma como trocamos a cor foi mudada
                   * Troca a cor atraves de um material:
                   * Essa e a cor forma nova
                   * */
        Renderer rend;
        rend = model.GetComponent<Renderer>();
        
       // rend.material.shader = Shader.Find("_Color");
		_defaultMaterial = rend.material.GetColor("_Color");
		print("cor default: " + _defaultMaterial.ToString());
      //  rend.material.SetColor("_Color", Color.grey);

//        rend.material.shader = Shader.Find("Specular");
//        rend.material.SetColor("_SpecColor", Color.magenta);
        /*
      * A forma como trocamos a cor foi mudada
      * Troca a cor atraves de um material:
      * Essa e a cor forma antiga
      * */
        /*
foreach (Transform child in model.GetComponentInChildren<Transform>())
{
_defaultMaterial.Add(child.GetComponent<Renderer>().material);
}
*/
        //_defaultMaterial = GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void Update()
    { }

    void OnCollisionEnter(Collision col) { }
    void OnCollisionExit(Collision col) { }
    void OnCollisionStay(Collision col) { }

    public void OnCollisionEnterCor(Colored col)
    {
        print("encostei em alguem colorido " + col.gameObject.name + " com cor " + col.cor.name);
        _coloredEmContato.Add(col);

    }
    public void OnCollisionExitCor(Colored col)
    {
        print("soltei de alguem colorido " + col.gameObject.name);
        _coloredEmContato.Remove(col);

        //verificar se estou encostado em alguem colorido
        //  se estou muda para a cor
        //  se nao estou volto para a cor default
        //Se nao estava camuflado, entao nao passo a ficar

		if (IsCamuflado())
		{
			SetCamufla(true);
		}

        //preciso de uma forma de verificar todos os itens em colisao comigo e todos que sao colored
        //  encontrei uma forma de fazer o segundo mas nao o primeiro. Para fazer manualmente:
        //      posso inserir numa lista todos os itens coloridos e retira-los quando collision exit
        //      dai e so checar essa lista
    }

    public void SetCamufla(bool b)
    {
        Renderer rend;

        /*
         *  Passe b como verdadeiro para camuflar.
         *  Passe b como falso para voltar a cor default
         *  Se b for verdadeiro mas nao houver cor em contato que possa camuflar entao ele volta para a cor default.
        */

        if (b)
        {
            foreach (Colored c in _coloredEmContato)
            {
                //print("procurando " + c.cores + " em cores disponiveis");
                if (_coresDisponiveis.Contains(c.cores))
                {
					/* A forma como trocamos a cor foi mudada
                     * Essa e a nova forma, trocamos a cor do shader
                     * */

					//print("trocando cor para " + c.cores.ToString());
                    rend = model.GetComponent<Renderer>();

                    //rend.material.shader = Shader.Find("_Color");
					rend.material.SetColor("_Color", Colored.GetColor(c.cores));

                    //rend.material.shader = Shader.Find("Specular");
                    //rend.material.SetColor("_SpecColor", c.cores);


                    /*
                     * A forma como trocamos a cor foi mudada
                     * Troca a cor atraves de um material:
                     * Essa e a cor forma antiga
                     * */
                     /*
                    foreach (Transform child in model.GetComponentInChildren<Transform>())
                    {
                        child.GetComponent<Renderer>().material = c.cor;
                    }
                    */
                    //Avisa o ColorRing que a cor do centro precisa ser mudada
                    GetComponent<Player>().staminaHandler.setCentralColor(c.cores);

                    //GetComponent<Renderer>().material = c;
                    gameObject.layer = 12;
                    /*
                     * layer 9 e a do player quando nao esta camuflado
                     * layer 11 e a layer dos enemies
                     * layer 12 e a do player quando ele esta camuflado.
                     * essas duas layers nao colidem uma com a outra
                     * 
                     **/
                    return;
                }
            }
        }
        gameObject.layer = 9;
        /*
         * layer 9 e a do player quando nao esta camuflado
         * layer 11 e a layer dos enemies
         * layer 12 e a do player quando ele esta camuflado.
         * essas duas layers nao colidem uma com a outra
         * 
         **/

        //Avisa o ColorRing que o centro deve sumir
        GetComponent<Player>().staminaHandler.setCentralColorToNull();
        /* A forma como trocamos a cor foi mudada
         * Essa e a nova forma, trocamos a cor do shader
         * */
        rend = model.GetComponent<Renderer>();

        //rend.material.shader = Shader.Find("_Color");
		rend.material.SetColor("_Color", _defaultMaterial);

        //rend.material.shader = Shader.Find("Specular");
		//rend.material.SetColor("_SpecColor", _defaultMaterial);


        /*
         * A forma como trocamos a cor foi mudada
         * Troca a cor atraves de um material:
         * Essa e a cor forma antiga
         * */
        /*
         *  int i = 0;
       foreach (Transform child in model.GetComponentInChildren<Transform>())
       {
          // child.GetComponent<Renderer>().material = _defaultMaterial[i];
           child.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
           i++;
       }
       //GetComponent<Renderer>().material = _defaultMaterial;
       */
    }

    public void AddColor(Colored.Corenum c)
    {
        if (!_coresDisponiveis.Contains(c))
        {
            _coresDisponiveis.Add(c);
            //Avisa o ColorRing que a uma cor foi adicionada, para que ele possa se remanejar
            GetComponentInParent<Player>().staminaHandler.ganhaCor(_coresDisponiveis);
        }
    }
    public bool IsCamuflado()
    {
		
		Renderer rend;
        rend = model.GetComponent<Renderer>();
		//rend.material.shader = Shader.Find("_Color");
		return rend.material.GetColor("_Color") != _defaultMaterial;
    }
}