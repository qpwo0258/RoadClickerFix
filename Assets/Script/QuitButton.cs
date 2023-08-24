using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private bool isClicked;
    [SerializeField]
    private Image selectImage = null;
    private AudioSource audioSource = null;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnClickQuitButton()
    {
        if (isClicked) return;
        audioSource.Play();
        isClicked = true;
        selectImage.gameObject.SetActive(true);
    }
    public void OnClickNoButton()
    {
        audioSource.Play();
        selectImage.gameObject.SetActive(false);
        isClicked = false;
    }
    public void OnClickYesButton()
    {
        Application.Quit();
    }
}
