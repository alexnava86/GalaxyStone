using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	#region Variables

	public static CameraManager Instance{get; private set;}
	#endregion

	#region Properties

	#endregion

	#region MonoBehaviour
	private void Start () 
	{
	
	}
	private void OnEnable()
	{

	}
	private void Update () 
	{

	}
	private void LateUpdate()
	{

	}
	private void OnDisable()
	{

	}
	#endregion

	#region Methods
	public static void SetCameraToMapNode(MapNode node)
	{

	}
	#endregion

	#region Coroutines
	public static IEnumerator LerpCameraToMapNode(MapNode node)
	{
		//disable input?
		//while()
		{
			yield return null;
		}
		//reenable input
		yield return null;
	}	
	#endregion
}
