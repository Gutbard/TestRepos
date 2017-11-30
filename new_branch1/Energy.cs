using UnityEngine;

public class Energy
{
	public float powerConsumption; //watts per hour
	public float powerRechargeSpeed; //watts per hour
	public float chargeVolume;
	public float powerConsumptionThreshold = 0.45f;
	public float timeMultiplier;

	public virtual void ConsumingState()
	{
		chargeVolume -= powerConsumption * Time.deltaTime * timeMultiplier;
	}

	public virtual void RechargingState()
	{
		chargeVolume += powerRechargeSpeed * Time.deltaTime * timeMultiplier;
	}

	public virtual void LifeCycleDiminish()
	{
		
	}
}


