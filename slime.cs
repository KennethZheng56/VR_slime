using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class slime : MonoBehaviour
{
    public float split_time = 10f;
    public GameObject text;
    public float die_time = 5f;
    public GameObject newChild;
    public Material frozen;
    public Color origin;
    public int cooked = 0;
    public int freeze = 0;
    private float time;
    private GameObject tempCollision = null;
    private GameObject tempText = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        origin = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        // float chance = UnityEngine.Random.Range(0, 1);
        if (freeze <= 50)
        {
            time += Time.deltaTime;
            if (tempText != null)
            {
                Destroy(tempText);
                tempText = null;
            } 
            // else if (transform.F)
            GetComponent<Renderer>().material.color = origin;
        } else if (freeze >= 500)
        {
            freeze = 500;
            if (GetComponent<GrabInteractable>() == null)
            {
                Grabbable gb = gameObject.AddComponent<Grabbable>();
                GrabInteractable gi = gameObject.AddComponent<GrabInteractable>();
                HandGrabInteractable hgi = gameObject.AddComponent<HandGrabInteractable>();
                gi.InjectOptionalPointableElement(gb);
                gi.InjectRigidbody(GetComponent<Rigidbody>());
                hgi.InjectOptionalPointableElement(gb);
                hgi.InjectRigidbody(GetComponent<Rigidbody>());
            }
            
            
            GetComponent<MeshRenderer>().material = frozen; 
            // if (tempText == null){
            //     GameObject freeze_text = Instantiate(text, gameObject.transform);
            //     freeze_text.transform.localPosition = new Vector3(5.5f, -.75f,0);
            //     tempText = freeze_text;
            // }
            gameObject.layer = LayerMask.NameToLayer("Slicable");
            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(true);
            
            foreach (Transform child in transforms)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Slicable");
            }

            
        }
        // if (freeze > 0)
        // {
        //     freeze -= 1;
            
        // } 
        
        if (split_time <= time)
        {
            // split 
            GameObject child1 = Instantiate(newChild);
            tempCollision = child1;
            // increase death time and split time
            child1.GetComponent<slime>().split_time = split_time + 1;
            child1.GetComponent<slime>().die_time = die_time + 0.5f;
            child1.GetComponent<Renderer>().material.color += new Color(0.05f, -0.05f, -0.1f, -0.05f);
            // add some speed
            Rigidbody rb1 = child1.GetComponent<Rigidbody>();
            rb1.AddForce(child1.transform.right * 4, ForceMode.Impulse);

            time = 0;
            split_time += 1;
            GetComponent<Renderer>().material.color += new Color(0.05f, -0.05f, -0.1f, -0.05f);

        }

        // kill itself
        if (time >= die_time)
        {
            Destroy(gameObject);
        }
    }

    // public void SetLayerRecursively(GameObject obj, )
    public void Cooking(int time)
    {
        cooked += time;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (tempCollision && collision.gameObject == tempCollision)
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>(), true);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (tempCollision && collision.gameObject == tempCollision)
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>(), true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (tempCollision && collision.gameObject == tempCollision)
        {
            tempCollision = null;
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>(), false);
        }
    }
}
