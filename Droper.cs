using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droper : MonoBehaviour
{
    float range = 0f;
    bool up_again = true;
    public float speed;
    public float distance;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (range < distance && up_again == true)
        {
            transform.Translate(0,-speed, 0);
            range += speed;
        }

        else
        {
            up_again = false;
            transform.Translate(0, +speed/10, 0);
            range -= speed/10;
            if (range <= 0)
            {
                up_again = true;
            }

        }
        
    }
}
