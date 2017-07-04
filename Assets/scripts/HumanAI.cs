
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
    /// <summary>
    /// return the current state of AI
    /// </summary>
    /// <returns></returns>
    public string GetState()
    {
        return tRig.AI.WorkingMemory.GetItem<string>("state");
    }

    /// <summary>
    /// set the state of the AI
    /// </summary>
    /// <param name="state"></param>
    public void SetState(string state)
    {
        tRig.AI.WorkingMemory.SetItem<string>("state", state);
    }
    
    /// <summary>
    /// When we do the tap movement on a hologram
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    /// <summary>
    /// When we gaze in the hologram
    /// </summary>
    public void OnFocusEnter()
    {
        GameObject.Find("CursorOnHolograms").transform.localScale = new Vector3(3,3,3);
    }

    /// <summary>
    /// When we gaze out the hologram
    /// </summary>
    public void OnFocusExit()
    {
        GameObject.Find("CursorOnHolograms").transform.localScale = new Vector3(1.15f ,1.15f ,1.15f);
    }

    /// <summary>
    /// Function called when the AI entre in the safe zone
    /// </summary>
    private void InSafeZone()
    {
        var randomDist = Random.Range(0.1f, 6);
        tRig.AI.Motor.CloseEnoughDistance = randomDist;
        SetState("saved");
    }

    /// <summary>
    /// Function for checking if the AI is moving (used for setting the animation)
    /// </summary>
    /// <returns></returns>
    protected bool IsMoving()
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
    /// <summary>
    /// Function for create a visual sensor on the object
    /// </summary>
    /// <param name="IsActive"></param>
    /// <param name="SensorName"></param>
    /// <param name="HorizontalAngle"></param>
    /// <param name="PositionOffset"></param>
    /// <param name="RequireLineOfSight"></param>
    /// <returns></returns>
    protected VisualSensor CreateVisualSensor(bool IsActive, string SensorName, int HorizontalAngle, Vector3 PositionOffset,
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

    /// <summary>
    /// Function for create a RAINAspectr on the object
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    protected RAINAspect CreateRainAspect(string name)
    {
        RAINAspect aspect = new VisualAspect();
        aspect.AspectName = name;
        aspect.MountPoint = gameObject.transform;
        return aspect;
    }

    private void OnInDanger()
    {
    }
    
}