using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Distance travelled by this piece of background 
        float distance = mainCam.transform.position.x * parallaxEffect;
        float temporal = (mainCam.transform.position.x * (1 - parallaxEffect));

        // Calculate new position on x axis, y and z axis dont change
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        // Check if were out of bounds (since were only moving in one direction we dont need to check both directions but its ok)
        if (temporal >= startPosition + length)
        {
            startPosition += length;
        }
        else if (temporal <= startPosition - length)
        {
            startPosition -= length;
        }

    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public float length;
    public float startPosition;
    public float parallaxEffect;

    // GameObjects ////////////////////////////////////////////////////////////////////////////////
    public Camera mainCam;
}
