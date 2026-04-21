using UnityEngine;

public class magic_pot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject next;
    public GameObject player;
    public GameObject congrats_screen;
    private bool hasChild;
    private bool GoalDone;
    private bool test;
    void Start()
    {
        hasChild = false;
        GoalDone = false;
        test = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GoalDone && !test)
        {
            GameObject final = Instantiate(congrats_screen, player.transform);
            Transform item = player.transform.Find("OVRCameraRig").transform.Find("TrackingSpace");
            final.transform.parent = item;
            test = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = null;
        if (hasChild){
            myCollider = collision.GetContact(0).thisCollider;
            }
        if (collision.collider.CompareTag("Slime") && !hasChild)
        {
            GameObject item = collision.collider.gameObject;
            Renderer slimeCol = item.GetComponent<Renderer>();
            Renderer potCol = GetComponent<Renderer>();

            // Compute bounding-box volume
            float slimeVolume = slimeCol.bounds.size.magnitude;

            float potVolume = potCol.bounds.size.magnitude;

            if (slimeVolume < potVolume)
        {
            // volume max = .2^3
            item.transform.parent = transform;
            item.transform.localPosition = new Vector3(0,0,0);
            var rb = item.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = true;
            hasChild = true;
        }
        }

        Debug.Log(collision.collider.tag);

        if (hasChild && (collision.collider.CompareTag("Stove") || (myCollider && myCollider.CompareTag("Stove"))))
        {
            // change = true;
            GameObject child = transform.GetChild(0).gameObject;
            Destroy(child);
            GameObject clone = Instantiate(next, transform);
            GoalDone = true;
            hasChild = false;
        } 
    }
}
