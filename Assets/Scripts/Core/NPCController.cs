using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {
    [SerializeField]
    private Animator localAnimator = null;
    private Vector3 previousPosition = Vector3.zero;

    void Start () {
        previousPosition = transform.position;
    }

    void Update () {
        Vector3 deltaPosition = transform.position - previousPosition;
        if (deltaPosition != Vector3.zero)
        {
            localAnimator.SetFloat("SpeedX", deltaPosition.x);
            localAnimator.SetFloat("SpeedY", deltaPosition.y);
        }
        localAnimator.SetBool("Running", previousPosition != transform.position);
        localAnimator.SetFloat("Speed", Mathf.Abs(deltaPosition.magnitude) / Time.deltaTime);
        previousPosition = transform.position;
    }
}
