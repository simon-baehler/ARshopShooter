
using HoloToolkit.Unity.InputModule;

public class ShopShooter : HumanAI, IInputClickHandler
{    
    private const int ANIM_SPEED = 1;
    private const int NORMAL_SPEED = 2;
    private const int RUN_SPEED = 4;
    private const int NBR_POLICE_SEEN_FOR_SURRENDER = 3;
    
   
    // Use this for initialization
    private void Start()
    {
        init();
        setState("stopped");
        tRig.AI.WorkingMemory.SetItem<int>("nbrSeenPoliceForSurr", NBR_POLICE_SEEN_FOR_SURRENDER);
        tRig.AI.WorkingMemory.SetItem<int>("speed", NORMAL_SPEED);
        oldLocation = transform.position;
        anim.SetFloat("Speed", ANIM_SPEED);
    }

    // Update is called once per frame
    private void Update()
    {
        switch (getState())
        {
            case "normal":
                anim.SetFloat("Speed", NORMAL_SPEED); 
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
    public void OnInputClicked(InputClickedEventData eventData)
    {
        OnSelect();
    }
    private void OnSelect()
    {
        tRig.AI.WorkingMemory.SetItem<string>("state", "arrested");
    }
    private void InSafeZone()
    {
        setState("caugth");
        anim.SetFloat("Speed", 0);
    }
}