
using HoloToolkit.Unity.InputModule;
using UnityEngine;


public class ShopShooter : HumanAI, IInputClickHandler
{
    private int nbrSceenPolice;
    private int nbrSceenPoliceForSurrender;
    private bool isAggressive;
    private Civillian civillianComponent;
    private int AILayerID;
    private GameObject goCivil;
    
    

    // Use this for initialization
    private void Start()
    {
        init();
        speed = 3.0f;
        
        setState("normal");
        isAggressive = false;
        nbrSceenPoliceForSurrender = 3;
        tRig.AI.WorkingMemory.SetItem<int>("nbrSeenPoliceForSurr", nbrSceenPoliceForSurrender);
        //tRig.AI.WorkingMemory.SetItem<bool>("chasingMode", false);
        tRig.AI.WorkingMemory.SetItem<int>("speed", 2);
        nbrSceenPolice = 0;
        oldLocation = transform.position;
        AILayerID = LayerMask.NameToLayer("IA");
        anim.SetFloat("Speed", speed);
    }

    // Update is called once per frame
    private void Update()
    {
        state = getState();
        switch (state)
        {
            case "normal":
                anim.SetFloat("Speed", speed);
                if (tRig.AI.WorkingMemory.GetItem<GameObject>("varCivil") != null)
                {
                    //print("varCivil");
                    goCivil = tRig.AI.WorkingMemory.GetItem<GameObject>("varCivil");
                    tRig.AI.WorkingMemory.SetItem<GameObject>("follow", goCivil);
                    civillianComponent = goCivil.gameObject.GetComponent<Civillian>();
                    tRig.AI.WorkingMemory.SetItem<bool>("targetIsAlife", civillianComponent.isAlife());
                }
                if (tRig.AI.WorkingMemory.GetItem<GameObject>("follow") != null)
                {
                    //print("follow");
                    goCivil = tRig.AI.WorkingMemory.GetItem<GameObject>("follow");
                    tRig.AI.WorkingMemory.SetItem<GameObject>("varCivil", goCivil);
                    civillianComponent = goCivil.gameObject.GetComponent<Civillian>();
                    if (civillianComponent.isAlife())
                        tRig.AI.WorkingMemory.SetItem<bool>("targetIsAlife", civillianComponent.isAlife());
                }
                break;
            case "hidded":
                //print("hidded");
                if (!isMoving())
                {
                    anim.SetFloat("Speed", 0);
                }
                break;
            case "stopped":
                //print("stopped");
                anim.SetFloat("Speed", 0);
                break;
            case "arrested":
                //print("arrested");
                anim.SetFloat("Speed", tRig.AI.WorkingMemory.GetItem<int>("speed") * 2);
                tRig.AI.Motor.CloseEnoughDistance = 2.0f;
                //anim.SetFloat("Speed", 0);
                break;
            case "hidding":
                //print("hidding");
                anim.SetFloat("Speed", speed);
                break;
        }
    }

    private void OnSelect()
    {
        print("lolll3l");
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
        if (collision.gameObject.layer == AILayerID && isAggressive == true)
        {
            collision.gameObject.SendMessage("OnStabbed", 100);
            tRig.AI.WorkingMemory.SetItem<GameObject>("follow", null);
            tRig.AI.WorkingMemory.SetItem<GameObject>("varCivil", null);
        }
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        print("lol");
        OnSelect();
    }
    private void InSafeZone()
    {
        setState("caugth");
        anim.SetFloat("Speed", 0);
    }
}