using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	private List<Material> _coresEmContato = new List<Material>();
	private List<Material> _coresDisponiveis = new List<Material>();
	private Material _defaultMaterial;

	// Use this for initialization
	void Start () {
		_defaultMaterial = GetComponent<Renderer>().material;

	}
	
	// Update is called once per frame
	void Update()
    {}

	void OnCollisionEnter(Collision col){}
	void OnCollisionExit(Collision col){}
	void OnCollisionStay(Collision col){}
    
	public void OnCollisionEnterCor(Colored col)
	{
        print("encostei em alguem colorido " + col.gameObject.name + " com cor " + col.cor.name);
        _coresEmContato.Add(col.cor);
	}
	public void OnCollisionExitCor(Colored col)
	{
        print("soltei de alguem colorido " + col.gameObject.name);
        _coresEmContato.Remove(col.cor);

        //verificar se estou encostado em alguem colorido
        //  se estou muda para a cor
        //  se nao estou volto para a cor default
        //Se nao estava camuflado, entao nao passo a ficar
		if(GetComponent<Renderer>().material != _defaultMaterial)
		    SetCamufla(true);

        //preciso de uma forma de verificar todos os itens em colisao comigo e todos que sao colored
        //  encontrei uma forma de fazer o segundo mas nao o primeiro. Para fazer manualmente:
        //      posso inserir numa lista todos os itens coloridos e retira-los quando collision exit
        //      dai e so checar essa lista
    }

	public void SetCamufla(bool b)
	{

        /*
         *  Passe b como verdadeiro para camuflar.
         *  Passe b como falso para voltar a cor default
         *  Se b for verdadeiro mas nao houver cor em contato que possa camuflar entao ele volta para a cor default.
        */

		if (b)
		{
			foreach (Material c in _coresEmContato)
			{
				print("procurando " + c.name + " em cores disponiveis");
				if (_coresDisponiveis.Contains(c))
				{
					GetComponent<Renderer>().material = c;
					return;
				}
			}
		}
        GetComponent<Renderer>().material = _defaultMaterial;
	}

	public void AddColor(Material mat)
	{
        if (!_coresDisponiveis.Contains(mat))
        {
            print("peguei a cor de alguem colorido " + mat.name);
            _coresDisponiveis.Add(mat);
        }
	}
	public bool IsCamuflado()
	{
		return GetComponent<Renderer>().material != _defaultMaterial; 
	}
}
