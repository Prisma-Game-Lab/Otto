using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colored : MonoBehaviour
{
	[Header("Cor da Camuflagem")]
	[Tooltip("Cor(material) para a qual o camaleao vai mudar ao estar em contato com este ojeto e tentar se camuflar.")]
	public Material cor;
	[Tooltip("Marque 'verdadeiro' se este objeto for capaz de dar ao camaleao uma nova cor")]
	public bool ganhaCor = false;
    public Animator corAnim;

    public enum Corenum
    {
		amarelo,
        verde,
        azul
    };
    [Tooltip("essencia cor que o player precisa ter para se camuflar nesse objeto")]
    public Corenum cores;

    // Use this for initialization
    void Start()
	{
        corAnim = GetComponent<Animator>();
		if (cor == null)
			cor = GetComponent<Renderer>().material;
	}

    void React()
    {
        corAnim.SetTrigger("explode");
    }

	private void OnCollisionEnter(Collision col)
    {
		Collider other = col.collider;
        if (ganhaCor && other.tag == "Player")
        {
            this.GetComponent<Collider>().enabled = false;
            React();
        }
    }

    // Update is called once per frame
    void Update()
	{}

    /* 
     * Retorna a cor correspondente a partir do enum que esta no topo deste codigo. 
     * Esta funcao é usada no script que controla a stamina
     * 
    */
	public static Color GetColor(Corenum cor)
    {
        /*
         * O construtor de Color recebe um valor entre 0 e 1, por isso estou divindo por 255 
        */

        switch (cor)
        {
            case Corenum.amarelo:
				return new Color((float)255 / 255, (float)255 / 255, (float)0 / 255);
            case Corenum.azul:
                return new Color((float)39 / 255, (float)64 / 255, (float)193 / 255);
            case Corenum.verde:
                return new Color(0, (float)205 / 255, (float)7 / 255);
        }
		return new Color((float)167 / 255, (float)254 / 255, (float)241 / 255);
    }
}
