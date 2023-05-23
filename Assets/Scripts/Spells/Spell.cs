using UnityEngine;
using System.Collections;

public abstract class Spell 
{
	private int mpCost; //the cost of mp to use this spell
	private string spellName;
	private AbstractCharacter character; //the character to upon whom this skill is equipped
	private MapNode[] areaOfEffect; 

	public abstract void Cast(AbstractCharacter target); //call this method in the character code when the spell is cast
}
