using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private MeshRenderer meshRenderer = null;
    [SerializeField]
    private float speed = 1f;
    private float offsetX = 0f;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        offsetX -= Time.deltaTime * speed;
        meshRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
    }
}