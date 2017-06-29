using HoloToolkit.Unity.InputModule;
using UnityEngine;


public class Civillian : HumanAI, IInputClickHandler
{
    private const float ANIM_SPEED = 0.5f;
    private const int ANIM_SPEED_RUN = 2;
    private const int NORMAL_SPEED = 1;
    private const int RUN_SPEED_MIN = 4;
    private const int RUN_SPEED_MAX = 6;

    // Use this for initialization
    private void Start()
    {
        init();
        HP = 100;
        tRig.AI.WorkingMemory.SetItem<float>("speed", 1);
        tRig.AI.WorkingMemory.SetItem<string>("state", "run");
        tRig.AI.WorkingMemory.SetItem<string>("moveESC", "ESC");
        tRig.AI.WorkingMemory.SetItem<int>("HP", HP);
        anim.SetFloat("Speed", ANIM_SPEED);
    }

    // Update is called once per frame
    private void Update()
    {
        if (HP == 0)
        {
            setState("dead");
            anim.SetBool("Dead", true);
        }
        switch (getState())
        {
            case "normal":
                //print("normal");
                tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
                anim.SetFloat("Speed", ANIM_SPEED);
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
                float randomSpeed = Random.Range(RUN_SPEED_MIN, RUN_SPEED_MAX);
                anim.SetFloat("Speed", ANIM_SPEED_RUN);
                tRig.AI.WorkingMemory.SetItem<float>("speed", randomSpeed);
                break;
            case "saved":
                
                if (!isMoving())
                {
                    anim.SetFloat("Speed", 0);
                }
                else
                {
                    anim.SetFloat("Speed", ANIM_SPEED);
                    tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
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
        setState("run");
    }

    private void OnSelect()
    {
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
        OnSelect();
    }
}