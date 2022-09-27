using UnityEngine;
using System.Collections;

public class EstantePartida : PalletManager
{
	//public Cinta CintaReceptora;//cinta que debe recibir la bolsa
	public GameObject ManoReceptora;
	//public Pallet.Valores Valor;
	
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
        if (receptor.RecievePallet(Pallets[0])) {
            Pallets.RemoveAt(0);
        }
    }
	
	public override bool RecievePallet (Pallet pallet)
	{
		//pallet.CintaReceptora = CintaReceptora.gameObject;
		pallet.porter = gameObject;
		return base.RecievePallet (pallet);
	}
}
