using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameItem : MonoBehaviour
{
	public string itemName;
	protected Item item;
	protected bool isBroken;
	protected bool isFull, isEmpty;
	protected float content;
	protected int full;
	//not used yet
	protected bool isUsing;
	protected AudioClip deploySound;
	protected Energy energy;
	protected bool hasAnimation;
	protected float itemHealth;

	public bool IsUsing {
		get { return isUsing;}
	}
	public virtual bool IsFull(){
		isFull = content >= full;
		return isFull;
	}
	public virtual bool IsEmpty(){
		isEmpty = content <= 0;
		return isEmpty;
	}
	public virtual ResourceType WhatIsThisContaining {
		get{ return (ResourceType)Enum.Parse (typeof(ResourceType), item.ItemName); }
	}

	public virtual void OnItemAdd(){

	}
	public virtual void OnItemAdd(float amount, ResourceType itemType){
		
	}
	public virtual void OnItemRemove(){
		item = new Item ();
		content = 0;
		isFull = false;
		isEmpty = true;
		CheckContainer ();
		Inventory.main.activeItemImage.SetActive (false);
	}
	public virtual void CheckContainer(){
		
	}
	public virtual void OnToolUse(){
		
	}
	public virtual void OnToolReload(){
		
	}
	public virtual void OnHolster(){

	}
	public void PickUp()
	{
		//13 - item
		if (transform.parent != null && transform.parent.gameObject.layer == 13) {
			Destroy (transform.parent.gameObject);
		} else if (transform.parent == null) {
			Destroy (gameObject);
		}
		Material rial= new Material(Shader.Find("FX/Water"));

	}
	public virtual void OnToolRepair()
	{
		if (isBroken) {
			Inventory.main.attentionText.text = "Инструмент сломан! Замените или почините его! (R)";
			Inventory.main.GetActiveQuickSlotItem().GetComponent<Image> ().color = Color.red;

			if (Input.GetKeyDown (KeyCode.R)) {
				Inventory.main.GetActiveQuickSlotItem().GetComponent<Image> ().color = Color.white;
				string quickItemName = Inventory.main.GetActiveQuickSlotItem().item.ItemName;
				Inventory.main.attentionText.text = "Инструмент " + quickItemName + " заменен(а) на рабочий!";
				itemHealth = 100;
				isBroken = false;
			}
		}
	}

	protected void ToolBreaking()
	{
		if (itemHealth <= 0) 
			isBroken = true;
		else
			itemHealth -= 0.5f;
	}

}

