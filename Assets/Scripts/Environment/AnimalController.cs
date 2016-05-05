using UnityEngine;
using System.Collections;

public class AnimalController : MonoBehaviour {
    AudioSource AuSource;
    SpriteRenderer SprRand;
    Animator Animator;

    float ActionTime;

	// Use this for initialization
	void Start () {
        Animator = GetComponent<Animator>();
        AuSource = GetComponent<AudioSource>();
        SprRand = GetComponent<SpriteRenderer>();
        SprRand.sortingOrder = (int)(-transform.position.y * 2);

        ActionTime = Random.Range(5f, 20f);
    }
	
	// Update is called once per frame
	void Update () {
        ActionTime -= Time.deltaTime;

        if (ActionTime < 0)
        {
            Animator.SetTrigger("First");
            ActionTime = Random.Range(5f, 20f);
        }

    }

    public void PlayMee()
    {
        AuSource.pitch = Random.Range(0.9f, 1.1f);
        AuSource.Play();
    }

}
