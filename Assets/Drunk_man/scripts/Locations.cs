using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class KeyPairStringCamera
{
    public string locationName;
    public Camera camera;
    public AudioClip soundClip;
}
public class Locations : MonoBehaviour
{
    [SerializeField] private GameObject _mapUI;
    [SerializeField] private KeyPairStringCamera[] _locations;
    [SerializeField] private Camera _currentCamera;
    [SerializeField] private UnityEvent _onOpenEvent;
    [SerializeField] private UnityEvent _onCloseEvent;
    
    [SerializeField] private AudioSource _audioSource;

    public void Open()
    {
        _mapUI.SetActive(true);
        _onOpenEvent.Invoke();
    }

    public void Close()
    {
        _mapUI.SetActive(false);
        _onCloseEvent.Invoke();
    }

    public void ChangeLocation(string locationName)
    {
        Close();
        
        foreach (KeyPairStringCamera keyPair in _locations)
        {
            if (keyPair.locationName == locationName)
            {
                _currentCamera.enabled = false;
                keyPair.camera.enabled = true;
                
                _currentCamera.gameObject.SetActive(false);
                keyPair.camera.gameObject.SetActive(true);

                _audioSource.clip = keyPair.soundClip;
                _audioSource.Play();
                _currentCamera = keyPair.camera;
                
                return;
            }
        }
    }

    public void StartGame()
    {
        Application.LoadLevel("ScenePier");
    }
    
    public void EndGame()
    {
        Application.LoadLevel(DialogueManager.instance.isWin ? "EndGameGood" : "EndGameBad");
    }
}