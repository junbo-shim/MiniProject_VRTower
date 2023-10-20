using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip loopedAudioClip; // �ݺ� ����� ����� Ŭ��

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = loopedAudioClip;
        audioSource.loop = true; // �ݺ� ��� Ȱ��ȭ
        audioSource.Play(); // ����� ��� ����
    }
}
