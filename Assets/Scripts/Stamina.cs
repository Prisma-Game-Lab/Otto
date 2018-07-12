using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

    /*
     * Para destaca uma determinada cor eu estou trocando o sprite dela.
     * O sprite 'anel' é o default
     * O sprite anelSelecionado é mais destacado
     * 
     */
	public Sprite anel;
	public Sprite anelSelecionado;

    /*
     * Guarda uma referencia direta para cada anel
     * O ColorRing é composto de 3 aneis e um Circulo Central
     * O circulo central tem uma uma imagem por fora, que é usada como mascara.
     * É o sprite dela que trocamos quando queremos destacar uma cor
     * E uma cor que é mascarada (é so um quadrado mesmo).
     * 
     * Eu estou guardando referencias diretas para nao precisar percorrer com tanta frequencia.
     * 
     * As variaveis abaixo sao importantes para fazer a animacao.
     * Base y é posicao y onde o objeto comeca
     * Base height é a altura dele
     * 
     * A mascara precisa se afastar do circulo se movendo para baixo ate que complete toda sua altura
     */
    private Image[] _rings = new Image[3];
    private Image _centralColorMask;
    private Image _centralColor;
//	private float _maxHeight;
	private float _baseY;
    private float _baseHeight;
	private void Start()
	{
		/*
		 * Mapeando os aneis, o circulo central e a imagem dele
		 */
		foreach (Transform t in transform)
		{
			switch (t.name)
			{
				case "CentralColor":
					_centralColorMask = t.GetComponent<Image>();
					break;
				case "RedRing":
                    _rings[0] = t.GetComponent<Image>();
					break;
				case "GreenRing":
					_rings[1] = t.GetComponent<Image>();
					break;
				case "BlueRing":
                    _rings[2] = t.GetComponent<Image>();
					break;
			}
					
		}
		foreach (Transform t in _centralColorMask.transform)
		{
			if(t.name == "CentralImage")
			_centralColor = t.GetComponent<Image>();
			//print("encontrei CentralImage " + _centralColor.name);
        }

        /* fill amount determina a secao de circulo da mascara que sera exibida,7
         * o que por sua vez que porcao do anel sera exibido.
         * Comeca em zero porque o personagem comeca sem cor e nao queremos que
         * nenhuma cor seja visivel nesse momento
         */
		_rings[0].fillAmount = 0;
		_rings[1].fillAmount = 0;
		_rings[2].fillAmount = 0;

        //print(_centralColorMask.rectTransform.localPosition.ToString());

        //Guardando os valores iniciais das posicoes para referencias futuras
        _baseY = _centralColorMask.rectTransform.localPosition.y;
        _baseHeight = _centralColorMask.rectTransform.sizeDelta.y;

        //Isso faz o circulo central sumir, so queremos ve-lo quando o camaleao estiver camuflado
		setCentralColorToNull();

		//print(_centralColorMask.rectTransform.sizeDelta.ToString());
		//print(_centralColor.rectTransform.sizeDelta.ToString());
	}
    //Faz a animacao da cor central
	public void UpdateStamina(float stamina, float maxStamina)
	{
		//print("stamina / maxStamina => " + stamina + " / " + maxStamina + " = " + (stamina / maxStamina));

		/*
         * O circulo central funciona usando um quadrado que é uma mascara para um circulo.
         * Temos de lentamente mover o quandrado (mascara), para cima e para baixo para revelar mais ou menos do circulo.
         * Fazemos isso de acordo com a stamina. Até ai tudo bem.
         * Acontece que no unity, o objeto mascarado deve necessariamente ser um filho da mascara
         * Entao quando voce move a mascara o filho se move junto '--
         * Bem, isso também é uma merda.
         * 
         * Entao voce precisa mover o quadrado para baixo e ao mesmo tempo mover o circulo dentro
         * dele para cima para compensar (!!)
         * E bem, isso é muito merda.
         * 
         */
		_centralColorMask.rectTransform.localPosition = new Vector3(0, _baseY - _baseHeight + (stamina/maxStamina *_baseHeight), 0);
		_centralColor.rectTransform.localPosition = new Vector3(0, -_baseY + _baseHeight - (stamina / maxStamina * _baseHeight), 0);   
	}
    /*
     * Troca a cor central para a cor desejada
     * Destaca o anel correspondente
     * Retorna os outros aneis para suas formas normais
     * 
     * 
     * Note que no unity é necessário usar um for para capturar o primeiro elemento filho, mesmo que ele seja o unico
     * Bem, isso é uma merda.
     */
	public void setCentralColor(Colored.Corenum cor)
	{
		_centralColor.color = Colored.GetColor(cor);
        
		Image rImage = null;
		foreach (Image r in _rings)
		{
			foreach (Transform t in r.transform)
			{
				rImage = t.GetComponent<Image>();
			}
			if (rImage.color == Colored.GetColor(cor))
			{
				r.sprite = anelSelecionado;
			}
			else
			{
				r.sprite = anel;
			}
		}
        
	}
    //Faz a cor central sumir
	public void setCentralColorToNull()
    {
		//0,0,0 seria preto, mas estou colocando o alpha como zero tambem
        //entao nao importa a cor, isso vai sumir
		_centralColor.color = new Color(0,0,0,0);

        //Se nao estamos selecionando cor alguma, entao nenhum anel deve ser destacado
        foreach (Image r in _rings)
        {
            r.sprite = anel;
        }
    }

    /*
     * Adiciona uma cor, remaneja os aneis para que possa caber mais um
     */
	public void ganhaCor(List<Colored.Corenum> coresDisponiveis)
	{

        /*
         * Os aneis (ativos) tem o mesmo tamanho. Mas comecam em lugares diferente,
         * e para isso rotacionamos eles.
         * Tambem temos de fazer cada anel preenchero suficiente para ocupar sua porcao do circulo
         * 
         * Eu estou pintando os aneis conforme o camaleao ganha cores.
         * As cores que estao representadas na gameScene sao meramente ilustrativas.
         * 
         * Isso da um pouco mais de flexibilidade.
         * No momento o que determina as cores realmente, e a funcao GetColor que esta em Colored
         * 
         * 
         */


		float rotacaoArco = 360/coresDisponiveis.Count;
		double tamArco = 1.0 / coresDisponiveis.Count;
		float rotacao = 0;
		//print("oba nova cor");
		for (int i = 0; i < _rings.Length;i++){
			Image r = _rings[i];
			Image rImage = null;
			foreach (Transform t in r.transform)
			{
				rImage = t.GetComponent<Image>();
			}
			//print("coresDisponiveis agora sao " + coresDisponiveis.Count);

			if (i < coresDisponiveis.Count)
			{
				//print(rImage.name);
				rImage.color = Colored.GetColor(coresDisponiveis[i]);
				//print("vou pintar o anel de nome " + rImage.name + "com cor " + rImage.color.ToString());
			}
			else
			{
                //print("deixei o anel de id " + i + " transparente");
				rImage.color = new Color(0, 0, 0, 0);
			}

			//print(r.name + " vai ter uma rotacao " + rotacao + " e vai preencher " + tamArco);
          
            //Euler angles sao graus de 0 ate 360
            //é para usar euler angles e nao rotation
			//rotation é um formato interno do unity e nao é muito facil de usar

			r.rectTransform.localEulerAngles = new Vector3(0, 0, rotacao);
			r.fillAmount = (float) tamArco;
			rotacao += rotacaoArco;
		}

	}
}
