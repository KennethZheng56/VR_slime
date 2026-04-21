using NUnit.Framework.Internal;
using Oculus.Interaction.GrabAPI;
using Oculus.Interaction.Input;
using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit
public class pickup : MonoBehaviour
{
    public GameObject right_hand;
    public GameObject random_obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // HandGrabAPI()
    }

    // Update is called once per frame
    void Update()
    {
        // if ()
    }

    void OnCollisionEnter(Collision collision)
    {
        IHand tester = right_hand.GetComponent<IHand>();
        if (collision.body.tag == "Hand")
        {
            Instantiate(random_obj, gameObject.transform);
        }
    }
}
