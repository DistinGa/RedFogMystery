using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {
    [SerializeField]
    private Animator localAnimator = null;
    private Vector3 previousPosition = Vector3.zero;

    void Start () {
        if (localAnimator == null)
            localAnimator = GetComponent<Animator>();
        if (localAnimator == null)
            Debug.LogWarning("На объекте " + gameObject.name + " не найден Animator", gameObject);

        previousPosition = transform.position;
        SetWind(Camera.main.GetComponent<CameraController>().Map.tag == "Wind");
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

    public void SetWind(bool sw)
    {
        localAnimator.SetBool("Wind", sw);
    }
}
