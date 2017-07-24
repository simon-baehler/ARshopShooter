using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveZone : MonoBehaviour
{
    private HashSet<GameObject> IA = new HashSet<GameObject>();
    public GameObject civilList;
    public GameObject shooter;
    private Transform ts;
    private int nrbActiveCivilian;
    private int AILayerID;

    // Use this for initialization
    private void Start()
    {
        nrbActiveCivilian = ChildCountActive(civilList.GetComponent<Transform>());
        AILayerID = LayerMask.NameToLayer("IA");
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (civilList == null && shooter == null) return;
        if (IA.Count == nrbActiveCivilian && shooter.GetComponent<ShopShooter>().GetState() ==
            EnumState.EStates.Caught.ToString())
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != AILayerID) return;
        other.gameObject.SendMessage("InSafeZone");
        IA.Add(other.gameObject);
    }

    public int ChildCountActive(Transform t)
    {
        int k = 0;
        foreach (Transform c in t)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return k;
    }
}