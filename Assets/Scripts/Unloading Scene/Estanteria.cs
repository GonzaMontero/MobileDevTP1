using UnityEngine;
using System.Collections;

public class Estanteria : PalletManager
{	
	public Cinta CintaReceptora;//cinta que debe recibir la bolsa
	public Pallet.Values Valor;
	PilaPalletMng Contenido;
	public bool Anim = false;
	
	
	//animacion de parpadeo
	public float Intervalo = 0.7f;
	public float Permanencia = 0.2f;
	float AnimTempo = 0;
	public GameObject ModelSuelo;
	public Color32 ColorParpadeo;
	Color32 ColorOrigModel;
	
	//--------------------------------//	
	
	void Start () 
	{
		Contenido = GetComponent<PilaPalletMng>();
		Debug.Log(ModelSuelo.name);
		ColorOrigModel = ModelSuelo.GetComponent<Renderer>().material.color;
	}
	
	void Update () 
	{
		//animacion de parpadeo
		if(Anim)
		{
			AnimTempo += Time.deltaTime;
			if(AnimTempo > Permanencia)
			{
				if(ModelSuelo.GetComponent<Renderer>().material.color == ColorParpadeo)
				{
					AnimTempo = 0;
					ModelSuelo.GetComponent<Renderer>().material.color = ColorOrigModel;
				}
			}
			if(AnimTempo > Intervalo)
			{
				if(ModelSuelo.GetComponent<Renderer>().material.color == ColorOrigModel)
				{
					AnimTempo = 0;
					ModelSuelo.GetComponent<Renderer>().material.color = ColorParpadeo;
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		PalletManager recept = other.GetComponent<PalletManager>();
		if(recept != null)
		{
			Give(recept);
		}
	}
	
	//------------------------------------------------------------//
	
	public override void Give(PalletManager receptor)
	{
        if (HasPallets()) {
            if (controller.GetMovingPallet() == null) {
                if (receptor.RecievePallet(Pallets[0])) {
                    //enciende la cinta y el indicador
                    //cambia la textura de cuantos pallet le queda
                    CintaReceptora.Encender();
                    controller.LeavePallet(Pallets[0]);
                    Pallets[0].GetComponent<Renderer>().enabled = true;
                    Pallets.RemoveAt(0);
                    Contenido.Sacar();
                    ApagarAnim();
                    //Debug.Log("pallet entregado a Mano de Estanteria");
                }
            }
        }
    }
	
	public override bool RecievePallet (Pallet pallet)
	{
		pallet.reciever = CintaReceptora.gameObject;
		pallet.porter = this.gameObject;
		Contenido.Agregar();
		pallet.GetComponent<Renderer>().enabled = false;
		return base.RecievePallet (pallet);
	}
	
	public void ApagarAnim()
	{
		Anim = false;
		ModelSuelo.GetComponent<Renderer>().material.color = ColorOrigModel;
	}
	public void EncenderAnim()
	{
		Anim = true;
		ModelSuelo.GetComponent<Renderer>().material.color = ColorOrigModel;
	}
}
