using UnityEngine;
using System.Collections.Generic;

public class BaseOfInventar : MonoBehaviour
{
  public List<ThingPropetries> Items = null;

  private void Start()
  {
    DontDestroyOnLoad(this);
  }
}
