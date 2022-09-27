using UnityEngine;
using System.Collections;

public class EstanteLlegada : PalletManager
{

	public GameObject Mano;
	public CalibrationController ContrCalib;
	
	public override bool RecievePallet(Pallet p)
	{
        p.porter = this.gameObject;
        base.RecievePallet(p);
        ContrCalib.FinishTutorial();

        return true;
    }
}
