using UnityEngine;
using System.Collections;

public class ConfirmationPromptCursor : MonoBehaviour 
{
	private ConfirmationPrompt parent;

	private void Start()
	{
		parent = this.GetComponentInParent<ConfirmationPrompt>();
	}
	private void Update()
	{
		this.transform.position = Input.mousePosition;
    }
	private void OnTriggerEnter2D(Collider2D collider)
	{
		switch(collider.gameObject.name)
		{
		case "YesText":
			parent.Toggle(true);
			break;
		case "NoText":
			parent.Toggle(false);
			break;
		}
	}
	private void OnTriggerStay2D(Collider2D collider)
	{
		switch(collider.gameObject.name)
		{
		case "YesText":
			if(Input.GetMouseButtonDown(0) != false)
			{
				parent.Select(true);
			}
			break;
        case "NoText":
			if(Input.GetMouseButtonDown(0) != false)
			{
				parent.Select(false);
			}
            break;
        }
    }
}
