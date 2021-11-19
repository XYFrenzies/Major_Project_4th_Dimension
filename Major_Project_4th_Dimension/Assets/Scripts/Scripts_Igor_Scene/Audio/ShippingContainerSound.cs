using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingContainerSound : MonoBehaviour
{
    public bool grounded = false;
    AudioSource source;
    public ArmStateManager arm;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        arm = FindObjectOfType<ArmStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        if (grounded && !source.isPlaying && arm.currentState == arm.pullState)
            SoundPlayer.Instance.PlaySoundEffect("Dragging", source);
        else if (!grounded || (source.isPlaying && arm.currentState != arm.pullState))
        {
            source.Stop();
        }
    }

    public bool GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down); // Shoot a ray down

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.5f)) // If the ray hits the ground
        {
            grounded = true; // is the player on the ground?

            return true;
        }

        grounded = false;
        return false;

    }
}
