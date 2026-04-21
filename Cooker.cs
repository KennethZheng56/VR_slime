using UnityEngine;

public class Cooker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool turnedOn = true;
    public bool getTime;
    public float time;
    void Start()
    {
        getTime = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        
        if (turnedOn && collision.collider.tag == "Slime")
        {
            collision.collider.GetComponent<Renderer>().material.color += new Color(0,-0.05f,0);
            slime slime = collision.collider.GetComponent<slime>();
            if (slime.enabled){
                slime.Cooking(1);
                
                if (slime.cooked >= 50)
                {
                    getTime = true;
                    collision.collider.GetComponent<Renderer>().material.color = Color.brown;// new Color(130,75,30,225);
                    slime.enabled = false;
                }
            }
            if (getTime)
            {
                time += Time.deltaTime;
            }

            if (time >= 10)
            {
                collision.collider.GetComponent<Renderer>().material.color = Color.black;
            }

        }
    }
}
