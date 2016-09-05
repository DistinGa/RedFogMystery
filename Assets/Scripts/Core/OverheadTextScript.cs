using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverheadTextScript : MonoBehaviour
{
    Text textObj;

    void Start()
    {
        textObj = GetComponent<Text>();
    }

    public void ShowOverheadText(string text, float delay)
    {

        StartCoroutine(DelayedText(text, delay));
    }

    IEnumerator DelayedText(string text, float delay)
    {
        textObj.text = text;
        yield return new WaitForSeconds(delay);
        textObj.text = "";
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}
