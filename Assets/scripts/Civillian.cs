using HoloToolkit.Unity.InputModule;
using UnityEngine;



public class Civillian :  HumanAI, IInputClickHandler
{
    private float speedAnim = 0.5f;
    // Use this for initialization
    private void Start () {
        init();
        HP = 100;
        speed = 1;
        tRig.AI.WorkingMemory.SetItem<float>("speed", speed);
        tRig.AI.WorkingMemory.SetItem<string>("state", "normal");
        tRig.AI.WorkingMemory.SetItem<bool>("action", true);
        tRig.AI.WorkingMemory.SetItem<int>("HP", HP);
        anim.SetFloat("Speed", speedAnim);
    }
	// Update is called once per frame
	private void Update () {
	    
        if (HP == 0)
        {
            setState("dead");
            anim.SetBool("Dead", true);
        }
        state = getState();
        switch (state)
        {
            case "normal":
                //print("normal");
                tRig.AI.WorkingMemory.SetItem<float>("speed", 1);
                anim.SetFloat("Speed", 0.5f);
                if (!isMoving())
                {
                    anim.SetFloat("Speed", 0);
                }
                break;
            case "panic":
                //print("panic");
                anim.SetBool("panic", true);
                break;
            case "run":
                float randomSpeed = Random.Range(4, 6);
                anim.SetFloat("Speed", 2);
                tRig.AI.WorkingMemory.SetItem<float>("speed", randomSpeed);
                break;
            case "saved":
                if (!isMoving())
                {
                    anim.SetFloat("Speed", 0);
                }
                else
                {
                    anim.SetFloat("Speed", 0.5f);
                    tRig.AI.WorkingMemory.SetItem<float>("speed", 1);
                }
                break;
        }
    }
    private void InSafeZone()
    {
        var randomDist = Random.Range(0.1f, 6);
        setState("saved");
        tRig.AI.Motor.CloseEnoughDistance = randomDist;
    }
    private void OnPolice()
    {
        
        if (tRig.AI.WorkingMemory.GetItem("varPlayer") == null ||
            (getState() == "panic" || getState() == "dead" || getState() == "saved")) return;
        print("OnPolice");
        setState("run");
        Destroy(gameObject);
        Destroy(this);
    }
    private void OnSelect()
    {
        print("lol2");
        if (getState() == "dead")
        {
            setState("run");
            anim.SetBool("Dead", false);
            HP = 100;
        }
        if (getState() == "panic")
        {
            anim.SetBool("panic", false);
            setState("run");
        }

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Destroy(gameObject);
        Destroy(this);
        print("lol");
        OnSelect();
    }
}
