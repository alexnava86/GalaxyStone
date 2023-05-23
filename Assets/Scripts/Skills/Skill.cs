using UnityEngine;
using System.Collections;

public abstract class Skill
{
	private string skillName; //name of the skill
	private AbstractCharacter character; //character to whom this skill belongs
	public string SkillName
	{
		get
		{
			return skillName;
		}
		set
		{
			skillName = value;
		}
	}
	public AbstractCharacter Character
	{
		get
		{
			return character;
		}
		set
		{
			character = value;
		}
	}

	abstract public void Use(AbstractCharacter target);
}
