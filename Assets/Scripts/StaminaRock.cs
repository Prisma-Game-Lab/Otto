using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaRock: MonoBehaviour {

    

    /*
     * Eu estou guardando referencias diretas para nao precisar percorrer com tanta frequencia.
     * 
     */
    private Image[] _rocks = new Image[3];
	private Image _centralRock;
	private Image _selectedRock;
	private Image _sombraDeCima;
    private Image _sombraDeBaixo;
	public Color defaultColor;
	private void Start()
	{
		/*
		 * Mapeando os aneis, o circulo central e a imagem dele
		 */
		foreach (Transform t in transform)
		{
			//print("verificando" + t.name);
			switch (t.name)
			{
				case "CentralImage":
					//print("==> encontrei! " + t.name);
					_centralRock = t.GetComponent<Image>();
					break;
				case "YellowImage":
                   // print("==> encontrei! " + t.name);
                    _rocks[0] = t.GetComponent<Image>();
					break;
				case "GreenImage":
                   // print("==> encontrei! " + t.name);
					_rocks[1] = t.GetComponent<Image>();
					break;
				case "BlueImage":
                   // print("==> encontrei! " + t.name);
                    _rocks[2] = t.GetComponent<Image>();
					break;
                    
				case "sombraDeBaixoCentral":
                   // print("==> encontrei! " + t.name);
					_sombraDeBaixo = t.GetComponent<Image>();
					break;
				case "sombraDeCimaCentral":
                   // print("==> encontrei! " + t.name);
					_sombraDeCima = t.GetComponent<Image>();
                    break;
			}
            
        }
		defaultColor = new Color((float)167 / 255, (float)254 / 255, (float)241 / 255);
		_centralRock.color = defaultColor;
		_centralRock.enabled = false;
		_sombraDeBaixo.enabled = false;
		_sombraDeCima.enabled = false;
		_rocks[0].enabled = false;
		_rocks[1].enabled = false;
		_rocks[2].enabled = false;

		_rocks[0].fillAmount = 0;
		_rocks[1].fillAmount = 0;
		_rocks[2].fillAmount = 0;

        //print(_centralColorMask.rectTransform.localPosition.ToString());

        //Isso faz o circulo central sumir, so queremos ve-lo quando o camaleao estiver camuflado
		setCentralColorToNull();

		//print(_centralColorMask.rectTransform.sizeDelta.ToString());
		//print(_centralColor.rectTransform.sizeDelta.ToString());
	}
    //Faz a animacao da cor central
	public void UpdateStamina(float stamina, float maxStamina){
	//	print(stamina + " / " + maxStamina);
		//if (_selectedRock != null)
			_centralRock.fillAmount = stamina/maxStamina;      
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
		_centralRock.color = Colored.GetColor(cor);
		_selectedRock = _rocks[(int)cor];
        
	}
    //Faz a cor central sumir
	public void setCentralColorToNull()
	{
		_selectedRock = null;
		_centralRock.color = defaultColor;
    }

    /*
     * Adiciona uma cor, remaneja os aneis para que possa caber mais um
     */
	public void ganhaCor(List<Colored.Corenum> coresDisponiveis)
	{

		foreach (Colored.Corenum cor in coresDisponiveis)
		{
            //print("A: " + cor.ToString());
           // print("B: " + (int)cor);
            //print("C: " + Colored.GetColor(cor));
			if (_rocks[(int)cor].enabled == false)
			{
				//print("ativando pedra " + cor.ToString());
				_rocks[(int)cor].enabled = true;
				_rocks[(int)cor].fillAmount = 1;
			}
		}
        _centralRock.enabled = true;
        _sombraDeBaixo.enabled = true;
        _sombraDeCima.enabled = true;

	}
}
