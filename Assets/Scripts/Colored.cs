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

    public enum Corenum
    {
        verde,
        azul,
        vermelho
    };
    [Tooltip("essencia cor que o player precisa ter para se camuflar nesse objeto")]
    public Corenum cores;

    // Use this for initialization
    void Start()
	{
		if (cor == null)
			cor = GetComponent<Renderer>().material;
	}

	private void OnCollisionEnter(Collision col)
    {
		/*
		Collider other = col.collider;
        if (ganhaCor && other.tag == "Tongue")
        {
			other.GetComponent<ChangeColor>().AddColor(cor);
            this.gameObject.SetActive(false);
        }
        */
    }

	// Update is called once per frame
	void Update()
	{}
}
