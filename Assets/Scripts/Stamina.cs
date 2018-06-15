using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

	public Sprite anel;
	public Sprite anelSelecionado;

    private float _maxHeight;
	private Image[] _rings = new Image[3];
	private Image _centralColorMask;
    private Image _centralColor;
	private float _baseY;
    private float _baseHeight;
	private void Start()
	{
		foreach (Transform t in transform)
		{
			print("verificando" + t.name);
			switch (t.name)
			{
				case "CentralColor":
					print("==> encontrei! " + t.name);
					_centralColorMask = t.GetComponent<Image>();
					break;
				case "RedRing":
                    print("==> encontrei! " + t.name);
                    _rings[0] = t.GetComponent<Image>();
					break;
				case "GreenRing":
                    print("==> encontrei! " + t.name);
					_rings[1] = t.GetComponent<Image>();
					break;
				case "BlueRing":
                    print("==> encontrei! " + t.name);
                    _rings[2] = t.GetComponent<Image>();
					break;
			}
					
		}
		foreach (Transform t in _centralColorMask.transform)
		{
			if(t.name == "CentralImage")
			_centralColor = t.GetComponent<Image>();
			print("encontrei CentralImage " + _centralColor.name);
        }

		_rings[0].fillAmount = 0;
		_rings[1].fillAmount = 0;
		_rings[2].fillAmount = 0;

        //print(_centralColorMask.rectTransform.localPosition.ToString());
        _baseY = _centralColor.rectTransform.localPosition.y;
        _baseHeight = _centralColor.rectTransform.sizeDelta.y;
		setCentralColorToNull();
		//print(_centralColorMask.rectTransform.sizeDelta.ToString());
		//print(_centralColor.rectTransform.sizeDelta.ToString());
	}

	public void UpdateStamina(float stamina, float maxStamina)
	{
		//print("stamina / maxStamina => " + stamina + " / " + maxStamina + " = " + (stamina / maxStamina));
		_centralColorMask.rectTransform.localPosition = new Vector3(0, -_baseY - _baseHeight + (stamina / maxStamina * _baseHeight), 0);
		_centralColor.rectTransform.localPosition = new Vector3(0, _baseY + _baseHeight - (stamina/maxStamina * _baseHeight)  , 0);    
	}
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
	public void setCentralColorToNull()
    {
		_centralColor.color = new Color(0,0,0,0);

        foreach (Image r in _rings)
        {
            r.sprite = anel;
        }
    }
	public void ganhaCor(List<Colored.Corenum> coresDisponiveis)
	{
		float rotacaoArco = 360/coresDisponiveis.Count;
		double tamArco = 1.0 / coresDisponiveis.Count;
		float rotacao = 0;
		print("oba nova cor");
		for (int i = 0; i < _rings.Length;i++){
			Image r = _rings[i];
			Image rImage = null;
			foreach (Transform t in r.transform)
			{
				rImage = t.GetComponent<Image>();
			}
			print("coresDisponiveis agora sao " + coresDisponiveis.Count);

			if (i < coresDisponiveis.Count)
			{
				print(rImage.name);
				rImage.color = Colored.GetColor(coresDisponiveis[i]);
				print("vou pintar o anel de nome " + rImage.name + "com cor " + rImage.color.ToString());
			}
			else
			{
                print("deixei o anel de id " + i + " transparente");
				rImage.color = new Color(0, 0, 0, 0);
			}

			//TODO trocar a imagem para que a cor selecionada fique destacada
			//TODO consertar o circulo central                                  =>DONE
			//TODO fazer com que as tres cores nao aparecam ja de inicio        =>DONE
			//TODO fazer o circulo central desaparecer durante o carregamento
			//TODO pintar a cor de cada anel da cor certa                       =>DONE

			print(r.name + " vai ter uma rotacao " + rotacao + " e vai preencher " + tamArco);
          
			r.rectTransform.localEulerAngles = new Vector3(0, 0, rotacao);
			r.fillAmount = (float) tamArco;
			rotacao += rotacaoArco;
		}

	}
}
