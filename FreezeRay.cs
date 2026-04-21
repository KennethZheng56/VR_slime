using UnityEngine;

public class FreezeRay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray SlimeDetector = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(SlimeDetector, out hit)){
            Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);
            if (hit.collider.tag == "Slime")
            {
                
                // Kill the Slime and replace it with a ball
                if (hit.collider.gameObject.GetComponent<slime>().freeze <= 500){
                    GameObject clone = Instantiate(hit.collider.gameObject);
                    clone.GetComponent<slime>().freeze += 2;
                    Destroy(hit.collider.gameObject);
                }
                
                
            }
        }
    }
}
