using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class playSound : MonoBehaviour
{
    private AudioSource aud;
    private bool keyIsDown = false;

    public Color pressedColor;
    public Color normalColor;
    private Renderer rend;

    private int steps;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        rend.material.color = normalColor;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            aud.Play();
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            aud.Stop();
        }

        if (aud.isPlaying)
        {
            if (aud.time > recordSound.clipEndTime)
            {
                aud.Play();
            }
        }
    }

    void OnTouchDown(Vector3 hitPoint)
    {
        aud.Play();
        rend.material.color = pressedColor;
    }

    void OnTouchUp()
    {
        aud.Stop();
        rend.material.color = normalColor;
    }

    //if sound is playing, listen to the mouse pos that is sent by the stay method and add to pitch
    void OnTouchStay(Vector3 hitPoint)
    {

    }


    void OnTouchExit()
    {
        aud.Stop();
        rend.material.color = normalColor;
    }
}
