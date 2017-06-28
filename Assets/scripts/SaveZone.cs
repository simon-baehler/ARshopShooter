using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveZone : MonoBehaviour
{


    private HashSet<GameObject> IA = new HashSet<GameObject>();
    public GameObject civilList;
    public GameObject shooter;
    private Transform ts;
    private int AILayerID;

    // Use this for initialization
    private void Start()
    {
        AILayerID = LayerMask.NameToLayer("IA");
    }
    // Update is called once per frame
    private void Update()
    {
        if (civilList != null && IA.Count == civilList.GetComponent<Transform>().childCount + 1)
        {
            SceneManager.LoadScene(1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print("IN");
        if (other.gameObject.layer != AILayerID) return;
        other.gameObject.SendMessage("InSafeZone");
        IA.Add(other.gameObject);
    }
}
