using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveZone : MonoBehaviour
{


    private HashSet<GameObject> IA = new HashSet<GameObject>();
    private GameObject civilList = null;
    private Transform ts;
    private int AILayerID;

    // Use this for initialization
    private void Start()
    {
        AILayerID = LayerMask.NameToLayer("IA");
        civilList = GameObject.Find("civils");
        if(civilList != null && civilList.GetComponent<Transform>() != null)
            ts = civilList.GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (civilList != null && IA.Count == ts.childCount)
        {
            //fin de la partie 
            SceneManager.LoadScene(1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != AILayerID) return;
        other.gameObject.SendMessage("InSafeZone");
        IA.Add(other.gameObject);
    }
}
