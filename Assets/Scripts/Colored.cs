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

	public static Color GetColor(Corenum cor)
    {
        switch (cor)
        {
            case Corenum.vermelho:
                return new Color((float)193 / 255, (float)39 / 255, (float)64 / 255);
            case Corenum.verde:
                return new Color(0, (float)205 / 255, (float)7 / 255);
            case Corenum.azul:
                return new Color((float)39 / 255, (float)64 / 255, (float)193 / 255);
        }
        return Color.black;
    }
}
