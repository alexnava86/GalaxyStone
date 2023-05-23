using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float bgSize;
	public string direction;
	private Vector3 startPos;
	
	private void Start () 
	{
		startPos = this.transform.position;
	}
	
	private void Update () 
	{
		float newPos = Mathf.Repeat(Time.time * scrollSpeed, bgSize);
		switch(direction)
		{
		case "left":
			transform.position = startPos + Vector3.left * newPos;
			break;
		case "right":
			transform.position = startPos + Vector3.right * newPos;
			break;
		case "up":
			transform.position = startPos + Vector3.up * newPos;
			break;
		case "down":
			transform.position = startPos + Vector3.down * newPos;
			break;
		}
	}
}