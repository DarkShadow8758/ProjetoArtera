using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class TimeBody : MonoBehaviour
{

    bool isRewinding = false;
    public float recordTime = 5f;
    [SerializeField] private AudioSource rewindSound;
    [SerializeField] private AudioClip rewindingSound;

    List<PointInTime> pointsInTime;
    Rigidbody rb;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
            rb.isKinematic = true;
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
            rb.isKinematic = false;
        }
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind ()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
            //play sound
            if (!rewindSound.isPlaying)
            {
                rewindSound.clip = rewindingSound;
                rewindSound.Play();
            }
            
        }
        else
        {
            StopRewind();
        }
        
    }
    void Record()
    {
        if (pointsInTime.Count > Mathf.Round( recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count -1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        Debug.Log(transform.position);
    }

    public void StartRewind ()
    {
        isRewinding = true;
    }
    public void StopRewind ()
    {
        isRewinding = false;
        if (rewindSound.isPlaying)
        {
            rewindSound.Stop();
        }
    }
}
