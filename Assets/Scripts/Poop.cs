using UnityEngine;
using System.Collections;

public class Poop : MonoBehaviour 
{
	int example = 0; //int is DATA TYPE that holds integer values (whole numbers 1,2,3,4,5,etc but not decimal values 1.2, 3.4,), exmaple is variable name, = 0 is an assignment.

	void Start () 
	{
		Debug.Log (example); //Console prints 0...
		example = 5; 
		//Debug.Log (example); //Console prints 5 if this line is uncommented...
	}
	
	void Update () 
	{
	}
}
