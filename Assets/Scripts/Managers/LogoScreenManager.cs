using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoScreenManager : MonoBehaviour 
{
	#region Variables
	public GameObject logo;
	public Image screen;
	private Transform logoPos;
    #endregion

	#region MonoBehaviour
	private void Start()
	{
		logoPos = logo.transform;
		StartCoroutine(FadeIn());
    }
    #endregion

	#region Coroutines
	private IEnumerator DropLogo()
	{
		while(logoPos.position.y > 0)
		{
			logoPos.position = new Vector2(logoPos.position.x, logoPos.position.y - 1f);
			yield return null;
		}
		yield return new WaitForSeconds(2.5f);
		StartCoroutine(FadeOut());
		yield return null;
    }

	private IEnumerator FadeIn()
	{
		while(screen.color.a > 0f)
		{
			screen.color = new Color(0f, 0f, 0f, screen.color.a - 0.025f);
			yield return null;
        }
		StartCoroutine(DropLogo());
		yield return null;
    }

	private IEnumerator FadeOut()
	{
		while(screen.color.a < 1f)
        {
			screen.color = new Color(0f, 0f, 0f, screen.color.a + 0.025f);
            yield return null;
        }
		Application.LoadLevel("IntroCutscene");
        yield return null;
    }
    #endregion
}
