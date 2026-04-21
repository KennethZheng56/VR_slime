using UnityEngine;

public class thumbStick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CharacterController player;
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var tSAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick, OVRInput.Controller.RTouch);
        Vector3 position = new Vector3(tSAxis.x, 0, tSAxis.y); // no jump
        player.Move(position * Time.deltaTime * 3.0f);

        
    }
}
