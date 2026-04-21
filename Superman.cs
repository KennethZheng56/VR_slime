using UnityEngine;

public class Superman : MonoBehaviour
{
    public Transform eye;
    public CharacterController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = eye.localRotation * Vector3.forward;
        
        if(OVRInput.Get(OVRInput.Button.One)){
            player.Move(direction * Time.deltaTime * 0.1f);
        }
    }
}
