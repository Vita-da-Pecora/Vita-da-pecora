using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Fuse : MonoBehaviour
{
    // script da inserire nel player!!!!!!!!!!!!!!!!!!!

    // i fusibili devono avere il capsule collider con is trigger attivo e un rigidbody senza gravità, ogni fuse un tag
    // il player con box/circle collider e is trigger non attivo, rigidbody con use gravity attivo

    // collisione con fusibile, si disattivano, bool is taken true, mi avvicino alla cassetta, compare premi E spawna il fusibile,
    // insieme si accende la lucina sopra, se presi tutti e tre, si accende la luce del faro

    [Header("--- Fuses --- ")]
    [SerializeField] private GameObject fuse1;
    [SerializeField] private GameObject fuse2;
    [SerializeField] private GameObject fuse3;

    [Header("--- Lighthouse --- ")]
    [SerializeField] private GameObject lightHouse;
    [SerializeField] private GameObject indicationBox; // se si entra in questo range compare il canvas con "E: interagisci"
    [SerializeField] private GameObject door;

    [Header("--- Spawnpoints --- ")]
    [SerializeField] private Transform fuse1SpawnPoint;
    [SerializeField] private Transform fuse2SpawnPoint;
    [SerializeField] private Transform fuse3SpawnPoint;

    [Header("--- Canvas --- ")]
    [SerializeField] private Canvas canvasIndication;
    [SerializeField] private Canvas canvasFusesCount;

    [Header("--- Fuses Canvas ---")]
    [SerializeField] private Image emptyGreen;
    [SerializeField] private Image emptyOrange;
    [SerializeField] private Image emptyBrown;
    [SerializeField] private Image takenGreen;
    [SerializeField] private Image takenOrange;
    [SerializeField] private Image takenBrown;


    private bool isTaken1 = false;
    private bool isTaken2 = false;
    private bool isTaken3 = false;
    private bool isSpawned1 = false;
    private bool isSpawned2 = false;
    private bool isSpawned3 = false;
    private bool isFinished = false;

    public void Start()
    {
        canvasIndication.gameObject.SetActive(false);
        //canvasFusesCount.gameObject.SetActive(false);
        lightHouse.gameObject.SetActive(false);
        door.SetActive(false);
        emptyGreen.gameObject.SetActive(true);
        emptyOrange.gameObject.SetActive(true);
        emptyBrown.gameObject.SetActive(true);
        takenGreen.gameObject.SetActive(false);
        takenOrange.gameObject.SetActive(false);
        takenBrown.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Tag dell'oggetto con cui il player sta collidendo: " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "Fuse1": //green
                if (!isSpawned1)
                {
                    other.gameObject.SetActive(false);
                    isTaken1 = true;
                    emptyGreen.gameObject.SetActive(false);
                    takenGreen.gameObject.SetActive(true);
                }
                break;

            case "Fuse2": //orange
                if (!isSpawned2)
                {
                    other.gameObject.SetActive(false);
                    isTaken2 = true;
                    emptyOrange.gameObject.SetActive(false);
                    takenOrange.gameObject.SetActive(true);
                }
                break;

            case "Fuse3": //brown
                if (!isSpawned3)
                {
                    other.gameObject.SetActive(false);
                    isTaken3 = true;
                    emptyBrown.gameObject.SetActive(false);
                    takenBrown.gameObject.SetActive(true);
                }
                break;
        }

        if (other.gameObject.tag == "IndicationBox")
        {
            if (isTaken1 || isTaken2 || isTaken3)
            {
                canvasIndication.gameObject.SetActive(true);

                if (Input.GetKey(KeyCode.E))
                {
                    Debug.Log("pulsante premuto");

                    if (isTaken1 && !isSpawned1)
                        StartCoroutine(SpawnFuse1());

                    if (isTaken2 && !isSpawned2)
                        StartCoroutine(SpawnFuse2());

                    if (isTaken3 && !isSpawned3)
                        StartCoroutine(SpawnFuse3());
                }
            }
        }

        if (isFinished)
        {
            door.SetActive(true);
            if (other.gameObject.tag == "Door")
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    private IEnumerator SpawnFuse1()
    {
        fuse1.transform.position = fuse1SpawnPoint.position;
        fuse1.transform.rotation = fuse1SpawnPoint.rotation;
        fuse1.SetActive(true);
        isSpawned1 = true;
        yield return null;
    }
    private IEnumerator SpawnFuse2()
    {
        fuse2.transform.position = fuse2SpawnPoint.position;
        fuse2.transform.rotation = fuse2SpawnPoint.rotation;
        fuse2.SetActive(true);
        isSpawned2 = true;
        yield return null;
    }

    private IEnumerator SpawnFuse3()
    {
        fuse3.transform.position = fuse3SpawnPoint.position;
        fuse3.transform.rotation = fuse3SpawnPoint.rotation;
        fuse3.SetActive(true);
        isSpawned3 = true;
        yield return null;
    }


    private void OnTriggerExit(Collider other)
    {
        if (isTaken1 || isTaken2 || isTaken3)
        {
            if (other.gameObject.tag == "IndicationBox")
            {
                canvasIndication.gameObject.SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (isSpawned1 && isSpawned2 && isSpawned3)
        {
            lightHouse.gameObject.SetActive(true);
            canvasIndication.gameObject.SetActive(false);
            isFinished = true;
            canvasFusesCount.gameObject.SetActive(false);
        }
    }
}