
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using RAIN.Core;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;


public class HumanAI : MonoBehaviour, IInputClickHandler, IFocusable
{
    protected Animator anim = null;
    protected Rigidbody rigidbody = null;
    protected AIRig tRig = null;
    protected int HP;
    protected Vector3 oldLocation = new Vector3();
    public GameObject NavTargetsGO;
    
   

    private const float THRESHHOLD = 0.01f;

    public bool isAlife()
    {
        return HP > 0;
    }
    
    protected void init()
    {
        HP = 100;
        anim = GetComponent<Animator>();
        tRig = gameObject.GetComponentInChildren<AIRig>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        tRig.AI.WorkingMemory.SetItem<float>("speed", 2.0f);
        tRig.AI.WorkingMemory.SetItem<string>("state","normal");
        tRig.AI.WorkingMemory.SetItem<int>("HP", HP);
        anim.SetFloat("Speed", 1);
        oldLocation = transform.position;
    }
    protected bool isMoving()
    {
        var resultX = oldLocation.x > transform.position.x
            ? oldLocation.x - transform.position.x
            : transform.position.x - oldLocation.x;
        var resultZ = oldLocation.z > transform.position.z
            ? oldLocation.z - transform.position.z
            : transform.position.z - oldLocation.z;
        var res = resultZ + resultX;
        print(res);
        oldLocation = transform.position;
        return THRESHHOLD < res;
    } 
   
    public string getState()
    {
        return tRig.AI.WorkingMemory.GetItem<string>("state");
    }

    public void setState(string state)
    {
        tRig.AI.WorkingMemory.SetItem<string>("state", state);
    }
    protected void OnStabbed(int value)
    {
        if(HP - value >= 0)
            HP = HP - value;
    }
    protected void onDie()
    {
        Destroy(gameObject);
        // Removes this script instance from the game object
        Destroy(this);
    }
    
    // Use this for initialization
    private void Start()
    {
        init();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnFocusEnter()
    {
        //throw new System.NotImplementedException();
    }

    public void OnFocusExit()
    {
        //throw new System.NotImplementedException();
    }
}