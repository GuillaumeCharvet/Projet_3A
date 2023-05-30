using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Item", fileName = "New Item (0)", order = 0)]
public class Item : MonoBehaviour
{
  public LocalizedStringRef description;

  private void Start()
  {
    Debug.Log(description.GetValue(Language.English));
  }
 
}
