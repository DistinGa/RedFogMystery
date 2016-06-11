using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

    private Animator _animator;
    private CanvasGroup _canvasGroup;
    private CharacterMoving _controller;

    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _controller = FindObjectOfType<CharacterMoving>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
        else
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            {
                BlockMoving(true);
                IsOpen = true;
            }
            else
            {
                BlockMoving(false);
                IsOpen = false;
            }
        }
    }
    private void BlockMoving(bool block)
    {
        _controller.KeyboardControl = !block;
    }
}
