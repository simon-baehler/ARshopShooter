
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;


public abstract class HumanAI : MonoBehaviour, IInputClickHandler, IFocusable
{
    protected const float ANIM_SPEED = 0.5f;
    protected const int ANIM_SPEED_RUN = 2;
    protected const int NORMAL_SPEED = 1;
    protected const int RUN_SPEED_MIN = 3;
    protected const int RUN_SPEED_MAX = 6;
    
    protected const float MASS_MIN = 1;
    protected const float MASS_MAX = 10;
    
    protected Animator anim = null;
    protected Rigidbody rigidbody = null;
    protected AIRig tRig = null;
    protected EntityRig tEntity;
    protected Vector3 oldLocation = new Vector3();
    public GameObject NavTargetsGO;
    
    private const float THRESHHOLD = 0.01f;
    protected bool isFocused;
    
    protected void init()
    {
        anim = GetComponent<Animator>();
        isFocused = false;
        tRig = gameObject.GetComponentInChildren<AIRig>();
        tEntity = gameObject.GetComponentInChildren<EntityRig>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        tRig.AI.WorkingMemory.SetItem<float>("speed", 2.0f);
        tRig.AI.WorkingMemory.SetItem<string>("state",EnumState.EStates.Normal.ToString());
        anim.SetFloat("Speed", 1);
        oldLocation = transform.position;
        
        if (NavTargetsGO == null)
        {
            NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
        }
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
    /// 
    /// </summary>
    /// <param name="state"></param>
    public void SetState(EnumState.EStates state)
    {
        tRig.AI.WorkingMemory.SetItem<string>("state", state.ToString());
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
        if (GameObject.Find("CursorOnHolograms") != null)
        {
            isFocused = true;
            GameObject.Find("CursorOnHolograms").transform.localScale = new Vector3(3, 3, 3);
        }
    }

    /// <summary>
    /// When we gaze out the hologram
    /// </summary>
    public void OnFocusExit()
    {
        isFocused = false;
        GameObject.Find("CursorOnHolograms").transform.localScale = new Vector3(1.15f ,1.15f ,1.15f);
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
    
    /// <summary>
    /// Function called when the AI entre in the safe zone
    /// </summary>
    private void InSafeZone()
    {
        var randomDist = Random.Range(0.1f, 6);
        tRig.AI.Motor.CloseEnoughDistance = randomDist;
        SetState(EnumState.EStates.Saved);
    }

    private void OnSelect()
    {
    }
}