using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player1 : Player 
{
	public delegate void ButtonAction(); //(string button)
	public delegate void AxisAction(string direction); //(int direction)
	public static event ButtonAction OnConfirm;
	public static event ButtonAction OnCancel;
	public static event ButtonAction OnToggleView;
	public static event ButtonAction OnToggleInfo;
	public static event AxisAction OnHorizontal;
	public static event AxisAction OnVertical;

	public static int levelsUnlocked{get; set;}
	public static Player1 Instance{get;private set;}

	private void Start () 
	{
		if (!Instance)    //if no GameManager object exists...
		{
			Instance = this; //set this gameObject as GameManager
			DontDestroyOnLoad (this);    
		}
		else
		{
			Destroy(this); //Otherwise, Destroy this Instance of a GameManager
		}
	}
	new private void OnEnable()
	{
		//Player2.Instance.enabled = false;
		//Player3.Instance.enabled = false;
		//Player4.Instance.enabled = false;
		//Enemy.Instance.enabled = false;
		//base.OnEnable();
    }
    private void Update () 
	{
		if(Input.GetButtonDown("Confirm") && OnConfirm != null)
		{
			OnConfirm();
		}
		if(Input.GetButtonDown("Cancel") && OnCancel != null)
		{
			OnCancel();
		}
		if(Input.GetButtonDown("ToggleInfo") && OnToggleInfo != null)
		{
			OnToggleInfo();
		}

		if(Input.GetButton("ToggleView") && OnToggleView != null)
		{
			if(MapCursor.Instance != null && MapCursor.Instance.enabled != false)
			{
				MapCursor.Instance.enabled = false;
			}

		}
		if(Input.GetButtonUp("ToggleView"))
		{
			if(MapCursor.Instance != null)
			{
				MapCursor.Instance.enabled = true;
			}
		}
		if(Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") != 0)
		{
			if(OnHorizontal != null)
			{
				OnHorizontal("Horizontal"); //OnAxis()
			}
		}
		if(Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") != 0)
		{
			if(OnVertical != null)
			{
				OnVertical("Vertical");
            }
        }

	}
	private void OnDisable()
	{

	}
	private void OnDestroy()
	{

	}

}
