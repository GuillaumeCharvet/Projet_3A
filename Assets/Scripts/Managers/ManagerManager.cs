using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ManagerManager : MonoBehaviour
{
    public static ManagerManager Instance;
    
    [SerializeField]
    private List<Manager> managers;

    private void Awake()
    {
        if (ManagerManager.Instance == null)
        {
            ManagerManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
            managers = GetComponents<Manager>().ToList();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public T GetManager<T>() where T : Manager
    {
        T returnedValue = null;
        returnedValue = Instance.managers.Find((currentManager) => currentManager.GetType() == typeof(T)) as T ;
        return returnedValue;
    }
}
