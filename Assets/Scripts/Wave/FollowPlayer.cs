using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform sensor;                      // The sensor to follow
    public float speed = 5f;                      // Lerp speed
    public float movementThreshold = 0.01f;       // Movement detection threshold
    public string moveClipName = "Armature|Walk.001";          // Name of the move animation clip
    public string idleClipName = "Armature|Idle";          // Name of the idle animation clip

    private Animation anim;
    private Vector3 lastPosition;
    private string currentClip = "";

    void Start()
    {
        anim = GetComponent<Animation>();
        lastPosition = transform.position;
        currentClip = idleClipName;

        if (anim[idleClipName] != null)
        {
            anim.Play(idleClipName);
        }
    }

    void Update()
    {
        if (sensor == null) return;

        // Follow movement
        Vector3 targetPos = sensor.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);

        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        bool isMoving = distanceMoved > movementThreshold;

        // Play appropriate clip
        if (isMoving && currentClip != moveClipName && anim[moveClipName] != null)
        {
            anim.CrossFade(moveClipName);
            currentClip = moveClipName;
        }
        else if (!isMoving && currentClip != idleClipName && anim[idleClipName] != null)
        {
            anim.CrossFade(idleClipName);
            currentClip = idleClipName;
        }

        lastPosition = transform.position;
    }
}
