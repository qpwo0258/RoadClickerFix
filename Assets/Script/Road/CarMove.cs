using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField]
    public Sprite[] carColors = null;

    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private float speed = 0f;
    private Road road = null;

    public void MoveCar(Road road)
    {
        this.road = road;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = carColors[Random.Range(0, 6)];
        gameObject.SetActive(true);
    }

    private void Update()
    {
        speed = GameManager.Instance.CarSpeed[road.roadObjectNum - 1];
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.localPosition.x > 0.8f)
        {
            gameObject.SetActive(false);
        }
    }
}
