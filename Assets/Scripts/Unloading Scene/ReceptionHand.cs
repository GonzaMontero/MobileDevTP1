using UnityEngine;
using System.Collections;

public class ReceptionHand : PalletManager 
{
	public bool HasPallet = false;
	
	void FixedUpdate () 
	{
		HasPallet = HasPallets();
	}
	
	void OnTriggerEnter(Collider other)
	{
		PalletManager recept = other.GetComponent<PalletManager>();
		if(recept != null)
		{
			Give(recept);
		}
		
	}

	public override bool RecievePallet(Pallet pallet)
	{
		if(!HasPallets())
		{
			pallet.porter = this.gameObject;
			base.RecievePallet(pallet);
			return true;
		}
		else
			return false;
	}
	
	public override void Give(PalletManager receptor)
	{
		switch (receptor.tag)
		{
		case "Mano":
			if(HasPallets())
			{
				if(receptor.name == "Right Hand")
				{
					if(receptor.RecievePallet(Pallets[0]))
					{
						Pallets.RemoveAt(0);
					}
				}				
			}
			break;
			
		case "Cinta":
			if(HasPallets())
			{
				if(receptor.RecievePallet(Pallets[0]))
				{
					Pallets.RemoveAt(0);
				}
			}
			break;
			
		case "Estante":
			break;
		}
	}
}
