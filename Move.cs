using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    readonly float speed = 15.0f;
    int status; //0 for stay, 1 for middle, 2 for dest
    Vector3 mid, dest;

    public void reset()
    {
        status = 0;
    }

    void Update()
    {
        if (status == 1) //middle
        {
            transform.position = Vector3.MoveTowards(transform.position, mid, speed * Time.deltaTime);
            if (transform.position == mid) status = 2;
        }
        if (status == 2) //dest
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            if (transform.position == dest) status = 0;
        }
    }
    public void setDest(Vector3 _dest)
    {
        dest = _dest;
        mid = _dest;
        if (_dest.y == transform.position.y) //boat
        {
            status = 2;
        }
        else if (_dest.y < transform.position.y) //land-boat
        {
            mid.y = transform.position.y;
        }
        else if (_dest.y > transform.position.y) //boat-land
        {
            mid.x = transform.position.x;
        }
        status = 1;
    }

}
