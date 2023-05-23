using UnityEngine;
using System.Collections;

public class GinkaHerb : Item 
{
	new private void Start()
	{
		base.Start();
		this.Cost = 50;
	}
	public override void Use (AbstractCharacter target)
	{
		this.Character.Hp += 50;
	}
}
