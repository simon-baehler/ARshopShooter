
using HoloToolkit.Unity.InputModule;
using RAIN.Entities.Aspects;
using UnityEngine;


public class Adam :   HumanAI, IInputClickHandler
{
	private RAINAspect civil;
     
	// Use this for initialization
	void Start () {
		init();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving())
		{
			anim.SetFloat("Speed", 0);
		}
		else
		{
			anim.SetFloat("Speed", 2);
		}
	}

	public void sayRassuring()
	{
		civil = tRig.AI.WorkingMemory.GetItem<RAINAspect>("moveTarget");
		civil.MountPoint.gameObject.GetComponent<Civillian>().setState("run");
		civil.MountPoint.gameObject. GetComponent<Animator>().SetBool("panic", false);
	}
}
