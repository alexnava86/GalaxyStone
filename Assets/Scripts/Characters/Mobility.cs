using UnityEngine;
using System.Collections;

public class Mobility : MonoBehaviour 
{
	private Animator anim;
	private Transform pos;
	private Rigidbody2D body;
	private bool blocked = false;
	private int speed = 2;

	private void Awake () 
	{
		if(this.GetComponent<Animator>() != null)
		{
			anim = this.GetComponent<Animator>();
		}
		if(this.GetComponent<Rigidbody2D>() != null)
		{
			body = this.GetComponent<Rigidbody2D>();
        }
        pos = this.transform;
		this.enabled = false;
	}
	private void OnTriggerStay2D(Collider2D collider)
    {
		if(Input.GetButtonDown("Confirm"))
		{
			if(collider.gameObject.name == "ElectricWormOrchardTileset_0")
			{
				//Debug.Log("hittt");
			}
		}
    }
	private void OnTriggerExit2D()
	{

	}
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.name == "Exit")
		{
			switch(Application.loadedLevelName)
			{
			case "NewGameIntro":
				StartCoroutine(NewGameIntro.Instance.FadeOut());
				break;
			}
		}
	}

	private void Update () 
	{

		if(Input.GetAxis("Horizontal") != 0)
		{
			if(Input.GetAxis("Horizontal") < - 0.25)
			{
				if(anim.GetBool("WalkLeft") == false)
				{
					ResetAnim();
					anim.SetBool("WalkLeft", true);
				}
			}
			else if(anim.GetBool("WalkLeft") == true)
			{
				ResetAnim();
				anim.SetBool("IdleLeft", true);
			}

			if(Input.GetAxis("Horizontal") > 0.25)
			{
				if(anim.GetBool("WalkRight") == false)
				{
					ResetAnim();
					anim.SetBool("WalkRight", true);
				}
			}
			else if(anim.GetBool("WalkRight") == true)
			{
				ResetAnim();
				anim.SetBool("IdleRight", true);
			}
		}
		if(Input.GetAxis("Vertical") != 0)
		{
			if(Input.GetAxis("Vertical") < -0.25)
			{
				ResetAnim();
				anim.SetBool("WalkDown", true);
			}
			else if(anim.GetBool("WalkDown") == true)
			{
				ResetAnim();
				anim.SetBool("IdleDown", true);
			}
			if(Input.GetAxis("Vertical") > 0.25)
			{
				ResetAnim();
				anim.SetBool("WalkUp", true);
			}
			else if(anim.GetBool("WalkUp") == true)
			{
				ResetAnim();
				anim.SetBool("IdleUp", true);
			}
		}

	}
	private void FixedUpdate()
	{
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			body.MovePosition(new Vector2(pos.position.x +  (Input.GetAxisRaw("Horizontal") * speed), pos.position.y + (Input.GetAxisRaw("Vertical") * speed)));
        }
    }
    
    private void LateUpdate()
    {
        pos.position = new Vector2 (Mathf.Round (pos.position.x), Mathf.Round (pos.position.y));
        
    }
    //Methods
	private void ResetAnim()
	{
		anim.SetBool ("IdleUp", false);
		anim.SetBool ("IdleDown", false);
		anim.SetBool ("IdleRight", false);
		anim.SetBool ("IdleLeft", false);
		anim.SetBool ("WalkDown", false);
		anim.SetBool ("WalkUp", false);
		anim.SetBool ("WalkRight", false);
		anim.SetBool ("WalkLeft", false);
	}
}
