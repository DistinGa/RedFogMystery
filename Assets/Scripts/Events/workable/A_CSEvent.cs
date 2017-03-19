using UnityEngine;
//Абстрактный класс для элементов катсцен.
[System.Serializable]
public abstract class CSEvent: MonoBehaviour
{
    public abstract void OnEventAction();
    public System.Action NextStep;
}
