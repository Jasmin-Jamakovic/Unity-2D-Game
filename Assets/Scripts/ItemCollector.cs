using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int pineapples = 0;
    [SerializeField] private Text points;
    [SerializeField] private AudioSource collectionSF;
    private void OnTriggerEnter2D(Collider2D collisoin)
    {
        if (collisoin.gameObject.CompareTag("Ananas"))
        {
            collectionSF.Play();
            Destroy(collisoin.gameObject);
            pineapples++;
            Debug.Log("Ananas: " + pineapples);
            points.text = "Points: " + pineapples;
        }
    }
}
