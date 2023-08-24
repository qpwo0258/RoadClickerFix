using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadObject : MonoBehaviour
{
    [SerializeField]
    private GameObject carTemplate = null;
    private float delay = 0f;

    private Road road = null;
    public void StartSpawnCar(Road road)
    {
        this.road = road;
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar()
    {
        GameObject newCar = null;
        CarMove newCarComponent = null;
        while (true)
        {
            if (road.isAccident == false)
            {
                newCar = null;
                delay = 8f / (float)(road.amount * (road.roadNumber + 1));
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.activeSelf == false)
                    {
                        newCar = transform.GetChild(i).gameObject;
                        break;
                    }
                }
                if (newCar == null)
                {
                    newCar = Instantiate(carTemplate, GameManager.Instance.UI.roadObjects[road.roadObjectNum - 1].transform);
                }
                newCar.transform.localPosition = new Vector2(-.8f, Random.Range(-.03f, .15f));
                newCarComponent = newCar.GetComponent<CarMove>();
                newCarComponent.MoveCar(road);
                yield return new WaitForSeconds(Random.Range(delay, delay * 1.2f));
            }
            else
            {
                yield return new WaitUntil(()=>road.isAccident == false);
            }
        }
    }
}