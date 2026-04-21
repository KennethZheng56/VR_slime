using UnityEngine;
using Oculus.Interaction;

public class Producer : MonoBehaviour
{
    public GameObject obj;
    public int cooldown = 10;
    private float time;
    private bool check;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = cooldown;
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (OVRInput.Get(OVRInput.Button.Any, OVRInput.Controller.Active) && check)
        {
            Instantiate(obj, transform);
            time = 0;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            check = true;
        }
    }
}
