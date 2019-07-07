using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    private Coroutine spawner = null;

	private float speed = 15f;

	private float interval = 2f;

    private List<GameObject[]> objects = new List<GameObject[]>();

	private GameObject[] currentList;
	private int currentIndex = 0;

	private int maxItems = 0;

    public GameObject[] deathCubes;

    public GameObject[] deathBalls;

    public GameObject[] coins;

    void Awake()
    {
        GameController.Instance.GameStarted += Game_GameStarted;
        GameController.Instance.GameEnded += Game_GameEnded;
    }

    private void Game_GameEnded(object sender, System.EventArgs e)
    {
        if (spawner != null)
        {
            StopCoroutine(spawner);
            spawner = null;

            if (currentList != null && currentList.Length > 0)
            {
                foreach (var obj in currentList)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void Game_GameStarted(object sender, System.EventArgs e)
    {
        if (spawner == null)
        {
            spawner = StartCoroutine(StartSpawning());
        }
    }

    // Use this for initialization
    IEnumerator StartSpawning () {
        objects.Clear();
        objects.Add(deathCubes);
        objects.Add(deathBalls);
        objects.Add(coins);

		while (true)
		{
            int objList = Random.Range(0, objects.Count);
            currentList = objects[objList];
            currentIndex = 0;
            maxItems = 3 + Random.Range(0, 3);

            for (int i = 0; i < maxItems; i++)
            {
                yield return new WaitForSeconds(interval);

                if (GameController.Instance.IsRunning)
                {
                    var obj = currentList[currentIndex];
                    var rb = obj.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    obj.transform.position = transform.position;
                    obj.SetActive(true);
                    rb.AddForce(Vector3.left * speed, ForceMode.Impulse);

                    currentIndex++;
                }
            }
		}
	}
}
