using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class S_GetRewards : MonoBehaviour
{
    [SerializeField]
    public GameObject pickableObject;

    public GameObject buttonGet;
    private bool pickable = false;
    private bool picked = false;

    public AudioClip popupSound;
    public AudioSource popupSource;

    public AudioClip getSound;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !picked)
        {
            Debug.Log("est rentré dans le collider");
            buttonGet.SetActive(true);
            pickable = true;
            popupSource.PlayOneShot(popupSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !picked)
        {
            Debug.Log("sort du collider");
            buttonGet.SetActive(false);
            pickable = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (pickable && (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("XboxB")))
        {
            popupSource.PlayOneShot(getSound);
            Debug.Log("le son se joue");
            pickableObject.SetActive(false);
            buttonGet.SetActive(false);
            picked = true;
            pickable = false;
        }
    }
}