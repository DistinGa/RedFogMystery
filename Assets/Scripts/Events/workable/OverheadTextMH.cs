using UnityEngine;
using System.Collections;

public class OverheadTextMH : MonoBehaviour {
    public Transform targetObject;
    [Multiline][TextArea]
    public string text;
    public float delay;

    public void OnEventAction()
    {
        GameManager.GM.OverheadText(targetObject, text, delay);
    }
}
