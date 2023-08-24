using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ImageMove());
    }
    private IEnumerator ImageMove()
    {
        while (true)
        {
            gameObject.transform.DOMoveY(2.2f, 1f);
            yield return new WaitForSeconds(1f);
            gameObject.transform.DOMoveY(2.15f, 1f);
            yield return new WaitForSeconds(1f);
        }
    }
}
