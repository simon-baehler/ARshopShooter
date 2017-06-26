﻿
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using RAIN.Core;


public class HumanAI : MonoBehaviour, IInputClickHandler, IFocusable
{
    protected Animator anim = null;
    protected Rigidbody rigidbody = null;
    protected AIRig tRig = null;
    protected float speed;
    protected int HP;
    protected Vector3 oldLocation = new Vector3();
    protected string state = "";
    public GameObject NavTargetsGO;

    private const float THRESHHOLD = 0.01f;

    public bool isAlife()
    {
        return HP > 0;
    }
    
    protected void init()
    {
        HP = 100;
        speed = 2;
        state = "normal";
        anim = GetComponent<Animator>();
        tRig = gameObject.GetComponentInChildren<AIRig>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        tRig.AI.WorkingMemory.SetItem<float>("speed", speed);
        tRig.AI.WorkingMemory.SetItem<string>("state", state);
        tRig.AI.WorkingMemory.SetItem<int>("HP", HP);
        anim.SetFloat("Speed", speed);
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
        oldLocation = transform.position;
        return THRESHHOLD < res;
    } 
   
    protected string getState()
    {
        return tRig.AI.WorkingMemory.GetItem<string>("state");
    }

    protected void setState(string state)
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