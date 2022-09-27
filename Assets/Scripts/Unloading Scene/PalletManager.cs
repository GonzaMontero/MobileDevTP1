using UnityEngine;
using System.Collections.Generic;

public class PalletManager : MonoBehaviour 
{
	protected List<Pallet> Pallets = new List<Pallet>();
	public DescentController controller;
	protected int counter = 0;
	
	public virtual bool RecievePallet(Pallet pallet)
	{
		Pallets.Add(pallet);
		pallet.Passing();
		return true;
	}
	
	public bool HasPallets()
	{
		
		if(Pallets.Count != 0)
			return true;
		else
			return false;
	}
	
	public virtual void Give(PalletManager reciever)
	{ }
}
