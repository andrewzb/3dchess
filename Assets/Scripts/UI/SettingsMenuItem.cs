using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{
    [HideInInspector] public Image image;
    [HideInInspector] public Transform position;


    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<Image>();
        position = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
