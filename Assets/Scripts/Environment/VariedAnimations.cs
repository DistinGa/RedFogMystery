using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class VariedAnimations : MonoBehaviour {
	void Start () {
        Animator Animator = GetComponent<Animator>();
        Animator.SetFloat("Offset", Random.Range(0f, 1f));
        Animator.SetFloat("SpeedModifier", Random.Range(0.9f, 1.1f));

    }
	
}
