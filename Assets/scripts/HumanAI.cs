
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;


public class HumanAI : MonoBehaviour, IInputClickHandler, IFocusable
{
    protected Animator anim = null;
    protected Rigidbody rigidbody = null;
    protected AIRig tRig = null;
    protected EntityRig tEntity;
    protected Vector3 oldLocation = new Vector3();
    public GameObject NavTargetsGO;
    

    private const float THRESHHOLD = 0.01f;
    
    protected void init()
    {
        anim = GetComponent<Animator>();
        tRig = gameObject.GetComponentInChildren<AIRig>();
        tEntity = gameObject.GetComponentInChildren<EntityRig>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        tRig.AI.WorkingMemory.SetItem<float>("speed", 2.0f);
        tRig.AI.WorkingMemory.SetItem<string>("state","normal");
        anim.SetFloat("Speed", 1);
        oldLocation = transform.position;
    }
    
    // Use this for initialization
    private void Start()
    {
        init();
    }
    public string getState()
    {
        return tRig.AI.WorkingMemory.GetItem<string>("state");
    }

    public void setState(string state)
    {
        tRig.AI.WorkingMemory.SetItem<string>("state", state);
    }
    
    public void OnInputClicked(InputClickedEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnFocusEnter()
    {
        GameObject.Find("CursorOnHolograms").transform.localScale = new Vector3(3,3,3);
    }

    public void OnFocusExit()
    {
        GameObject.Find("CursorOnHolograms").transform.localScale = new Vector3(1.15f ,1.15f ,1.15f);
    }

    private void InSafeZone()
    {
        var randomDist = Random.Range(0.1f, 6);
        tRig.AI.Motor.CloseEnoughDistance = randomDist;
        setState("saved");
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
   
    protected VisualSensor createVisualSensor(bool IsActive, string SensorName, int HorizontalAngle, Vector3 PositionOffset,
        bool RequireLineOfSight)
    {
        VisualSensor s = new VisualSensor
        {
            IsActive = IsActive,
            SensorName = SensorName,
            MountPoint = gameObject.transform,
            HorizontalAngle = HorizontalAngle,
            PositionOffset = PositionOffset,
            RequireLineOfSight = RequireLineOfSight,
            LineOfSightMask = 1024
        };
        return s;
    }

    protected RAINAspect createRAINAspect(string name)
    {
        RAINAspect aspect = new VisualAspect();
        aspect.AspectName = name;
        aspect.MountPoint = gameObject.transform;
        return aspect;
    }
}