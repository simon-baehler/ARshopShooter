
using HoloToolkit.Unity.InputModule;
using UnityEngine;


public class ShopShooter : HumanAI, IInputClickHandler
{
    //private bool isAggressive;
    //private Civillian civillianComponent;
    //private int AILayerID;
    //private GameObject goCivil;
    
    private const int ANIM_SPEED = 1;
    private const int NORMAL_SPEED = 2;
    private const int RUN_SPEED = 4;
    private const int NBR_POLICE_SEEN_FOR_SURRENDER = 3;
    
    

    // Use this for initialization
    private void Start()
    {
        init();
        setState("stopped");
        //isAggressive = false;
        tRig.AI.WorkingMemory.SetItem<int>("nbrSeenPoliceForSurr", NBR_POLICE_SEEN_FOR_SURRENDER);
        //tRig.AI.WorkingMemory.SetItem<bool>("chasingMode", false);
        tRig.AI.WorkingMemory.SetItem<int>("speed", NORMAL_SPEED);
        oldLocation = transform.position;
        //AILayerID = LayerMask.NameToLayer("IA");
        anim.SetFloat("Speed", ANIM_SPEED);
    }

    // Update is called once per frame
    private void Update()
    {
        switch (getState())
        {
            case "normal":
                anim.SetFloat("Speed", NORMAL_SPEED);
                /*if (tRig.AI.WorkingMemory.GetItem<GameObject>("varCivil") != null)
                {
                    goCivil = tRig.AI.WorkingMemory.GetItem<GameObject>("varCivil");
                    tRig.AI.WorkingMemory.SetItem<GameObject>("follow", goCivil);
                    civillianComponent = goCivil.gameObject.GetComponent<Civillian>();
                    tRig.AI.WorkingMemory.SetItem<bool>("targetIsAlife", civillianComponent.isAlife());
                }
                if (tRig.AI.WorkingMemory.GetItem<GameObject>("follow") != null)
                {
                    goCivil = tRig.AI.WorkingMemory.GetItem<GameObject>("follow");
                    tRig.AI.WorkingMemory.SetItem<GameObject>("varCivil", goCivil);
                    civillianComponent = goCivil.gameObject.GetComponent<Civillian>();
                    if (civillianComponent.isAlife())
                        tRig.AI.WorkingMemory.SetItem<bool>("targetIsAlife", civillianComponent.isAlife());
                }*/
                break;
            case "hidded":
                anim.SetFloat("Speed", 0);
                break;
            case "stopped":
                anim.SetFloat("Speed", 0);
                break;
            case "arrested":
                anim.SetFloat("Speed", tRig.AI.WorkingMemory.GetItem<int>("speed") * NORMAL_SPEED);
                tRig.AI.Motor.CloseEnoughDistance = 2.0f;
                break;
            case "hidding":
                anim.SetFloat("Speed", RUN_SPEED);
                break;
        }
    }

    private void OnSelect()
    {
        tRig.AI.WorkingMemory.SetItem<string>("state", "arrested");
    }
    /*private void OnChasingModeOn()
    {
        if (state == "arrested" || state == "stopped" || isAggressive == false) return;
        anim.SetFloat("Speed", speed);
        tRig.AI.WorkingMemory.SetItem<bool>("chasingMode", true);
        tRig.AI.WorkingMemory.SetItem<GameObject>("follow", null);
        tRig.AI.WorkingMemory.SetItem<GameObject>("varCivil", null);
    }

    private void OnChasingModeOff()
    {
        tRig.AI.WorkingMemory.SetItem<bool>("chasingMode", false);
        tRig.AI.WorkingMemory.SetItem<GameObject>("follow", null);
        tRig.AI.WorkingMemory.SetItem<GameObject>("varCivil", null);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.layer == AILayerID && isAggressive == true)
        {
            collision.gameObject.SendMessage("OnStabbed", 100);
            tRig.AI.WorkingMemory.SetItem<GameObject>("follow", null);
            tRig.AI.WorkingMemory.SetItem<GameObject>("varCivil", null);
        }
        */
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        OnSelect();
    }
    private void InSafeZone()
    {
        setState("caugth");
        anim.SetFloat("Speed", 0);
    }
}