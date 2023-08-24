using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartButton : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ButtonMove());
    }
    private IEnumerator ButtonMove()
    {
        while(true)
        {
            gameObject.transform.DOMoveY(-3.05f, 1f);
            yield return new WaitForSeconds(1f);
            gameObject.transform.DOMoveY(-3f, 1f);
            yield return new WaitForSeconds(1f);
        }
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
