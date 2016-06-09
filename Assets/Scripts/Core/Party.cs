using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Party : MonoBehaviour
{
    [SerializeField]
    private int distance = 40; //diistance = 40 - 1,3 sec
    [SerializeField]
    private Vector3[] trace = new Vector3[0];
    public GameObject FollowTo = null;   //За кем идти в режиме партии

  private void Start ()
  {
    Array.Resize(ref trace, distance + 1);
    for (var i = 0; i < trace.Length - 1; i++)
    {
      trace[i] = FollowTo.transform.position;
    }
	}

    private void Update()
    {
        if (FollowTo.transform.position != trace[0])
        {
            for (var i = trace.Length - 1; i > 0; i--)
            {
                trace[i] = trace[i - 1];
            }
            trace[0] = FollowTo.transform.position;

            transform.position = trace[distance];
        }
    }

}
