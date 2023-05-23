using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfirmationPrompt : MonoBehaviour 
{
	#region Variables
	public Image textCursor;
	public Image textBorder;
	public Image textBox;
	public Image textBoxGradient;
	public Text promptText;
	public Text yesText;
	public Text noText;
	private bool yes = true;
	private bool done = false;
	private int textOffset = 0;
	#endregion

	#region Properties
	public bool Yes
	{
		get
		{
			return yes;
		}
		set
		{
			yes = value;
		}
	}
	public int TextOffset
	{
		get
		{
			return textOffset;
		}
		set
		{
			textOffset = value;
		}
	}
	#endregion

	#region MonoBehaviour
	protected void Start () 
	{
		StartCoroutine(Wait());
		OptionsManager.Instance.mouseTrigger.GetComponent<OptionCursor>().enabled = false;
	}
	
	protected void Update () 
	{
		if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetButtonDown("Horizontal"))
		{
			yes = false;
			Toggle(yes);
		}
		if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetButtonDown("Horizontal"))
		{
			yes = true;
			Toggle(yes);
		}
		if(Input.GetButtonDown("Confirm"))
		{
			if(done != false)
			{
				Select(yes);
			}
		}
	}
	protected void OnDestroy()
	{
		if(OptionsManager.Instance.mouseTrigger != null)
		{
			OptionsManager.Instance.mouseTrigger.GetComponent<OptionCursor>().enabled = true;
		}
	}
	#endregion 

	#region Methods / Coroutines
	public virtual void Select(bool confirm)
	{
		if(confirm != false)
		{
			StartCoroutine(OptionsManager.Instance.FadeOut());
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
	public virtual void Toggle(bool confirm)
	{
		switch(confirm)
		{
		case true:
			textCursor.rectTransform.anchoredPosition = new Vector2(250, -30);
			break;
		case false:
			textCursor.rectTransform.anchoredPosition = new Vector2(350, -30);
			break;
		}
	}

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(1f);
		done = true;
		yield return null;
	}
	#endregion
}
