using UnityEngine;

public class world_border : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject worldBorder;
    private float width;
    private float length;
    // private float self_h = 0f;
    void Start()
    {
        MeshFilter meshFilter = worldBorder.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Bounds bound = meshFilter.mesh.bounds;

            Vector3 localscale = worldBorder.transform.localScale;
            width = bound.size.x * localscale.x;
            length = bound.size.z * localscale.z;
        }
        // Renderer renderer = GetComponent<Renderer>();
        // if (renderer != null)
        // {
        //     self_h = renderer.bounds.size.y;
        // }

    }

    // Update is called once per frame
    void Update()
    {
        float times = 1;
        if (transform.position.x >= width/2 + worldBorder.transform.position.x || transform.position.x <= -width/2 + worldBorder.transform.position.x)
        {
            if (transform.position.x < 0)
            {
                times = -1;
            }
            transform.position = new Vector3(times* width/2 + worldBorder.transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= length/2 + worldBorder.transform.position.z || transform.position.z <= -length/2 + worldBorder.transform.position.z)
        {
            if (transform.position.z < 0)
            {
                times = -1;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, times * length/2 + worldBorder.transform.position.z);
        }

        // if (transform.position.y <= worldBorder.transform.position.y)
        // {
        //     transform.position = new Vector3(transform.position.x, worldBorder.transform.position.y + self_h, transform.position.z);
        //     GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        // }
    }
}
