using UnityEngine;
using System.Collections;

public class BodyPart
{
	private int hp;
	private int mp;
	private int physicalAttack;
	private int physicalDefense;
	private int magicAttack;
	private int magicDefense;
	public int Hp
	{
		get
		{
			return hp;
		}
		set
		{
			hp = value;
		}
	}
	public int Mp
	{
		get
		{
			return mp;
		}
		set
		{
			mp = value;
		}
	}
}
