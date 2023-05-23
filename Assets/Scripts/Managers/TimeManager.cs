using UnityEngine;
using System;
using System.Collections;

public class TimeManager : MonoBehaviour
{
	public static DateTime LastPlayed { get; set; }
	public static TimeSpan TimePlayed { get; set; }
	public static TimeManager Instance { get; private set; }

	private void Start ()
	{
		if (!Instance)
		{
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (this.gameObject);
		}
		StartCoroutine (Initialize ());
	}

	private void OnDisable ()
	{
		StopCoroutine (Initialize ());
	}

	private IEnumerator Initialize ()
	{
		while (this.enabled != false)
		{
			TimePlayed.Add (new TimeSpan (0, 0, 1));
			//Debug.Log("Sec = " + timePlayed.seconds + " Min = " + timePlayed.minutes + " Hr = " + timePlayed.hours);
			yield return new WaitForSeconds (1f);
		}
		yield return null;
	}
}

