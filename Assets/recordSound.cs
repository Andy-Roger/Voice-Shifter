using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordSound : MonoBehaviour
{
    private AudioSource originalAudio;
    private int audLength = 3;
    public static float clipEndTime;
    private bool isRecording = false;
    public Transform keyboard;


    public Color pressedColor;
    public Color normalColor;
    private Renderer rend;

    private void Start()
    {
        originalAudio = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        rend.material.color = normalColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRecording = true;
            originalAudio.clip = Microphone.Start(Microphone.devices[0], true, audLength, 44100);
            originalAudio.Play();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            isRecording = false;
            clipEndTime = originalAudio.time;
            Microphone.End(Microphone.devices[0]);

            foreach (Transform key in keyboard)
            {
                key.GetComponent<AudioSource>().clip = originalAudio.clip;


            }
        }
    }

    void OnTouchDown()
    {
        isRecording = true;
        originalAudio.clip = Microphone.Start(Microphone.devices[0], true, audLength, 44100);
        originalAudio.Play();
        rend.material.color = pressedColor;
    }

    void OnTouchUp()
    {
        isRecording = false;
        clipEndTime = originalAudio.time;
        Microphone.End(Microphone.devices[0]);

        for(int i = 0; i < keyboard.childCount; i ++)
        {
            AudioSource keyAudioSource = keyboard.GetChild(i).GetComponent<AudioSource>();
            keyAudioSource.clip = AudioClip.Create(keyboard.GetChild(i).name, originalAudio.clip.samples, 1, 44100, false);

            float[] samples = new float[originalAudio.clip.samples];
            originalAudio.clip.GetData(samples, 0);
            keyAudioSource.clip.SetData(samples, 0);

            //pitchshift
            pitchShifter.PitchShift(Mathf.Pow(1.059463094359f, i), keyAudioSource.clip.samples, 44100, samples);
            keyAudioSource.clip.SetData(pitchShifter.outdata, 0);
        }

        rend.material.color = normalColor;
    }
}
