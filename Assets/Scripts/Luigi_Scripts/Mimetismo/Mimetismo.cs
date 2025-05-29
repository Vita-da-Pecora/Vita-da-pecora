using UnityEngine;

public class Mimetismo : MonoBehaviour
{
    public bool isInvisible { get; private set; } = false;
    private bool nearSheep = false;
    private SheepGroupMovement SheepGroup; // riferimento al gruppo da seguire
    public float velocit‡Sincronizzazione = 2f;

    void Update()
    {
        if (nearSheep && Input.GetKeyDown(KeyCode.E))
        {
            isInvisible = !isInvisible;
            Debug.Log("Stato mimetismo: " + isInvisible);

            //bool enable = isInvisible ? false : true;
            //GetComponent<ThirdPersonController>().isMovementActive = enable;
            //GetComponent<CharacterController>().enabled = enable;
        }

        if (!nearSheep && isInvisible)
        {
            isInvisible = false;
            Debug.Log("Mimetismo disattivato: sei uscito dalla zona.");
        }

        if (isInvisible && SheepGroup != null)
        {
            // Sincronizza la posizione gradualmente verso il gruppo
            Vector3 posizioneTarget = SheepGroup.GetPlayerPosition();
            transform.position = Vector3.Lerp(transform.position, posizioneTarget, Time.deltaTime * velocit‡Sincronizzazione);
            if (Vector3.Distance(transform.position, posizioneTarget) <= 0.3f)
            {
                transform.position = posizioneTarget;
            }

            // Sincronizza anche la rotazione verso quella del gruppo
            Quaternion rotazioneTarget = SheepGroup.transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotazioneTarget, Time.deltaTime * velocit‡Sincronizzazione);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Sono vivo");
        //if (other.CompareTag("SheepGroup"))
        if (other.TryGetComponent(out SheepGroupMovement sheepGroupMovement))

        {
            nearSheep = true;
            SheepGroup = sheepGroupMovement; // salva il gruppo attuale
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Sono vivo2");
        //if (other.CompareTag("SheepGroup"))
        if (other.TryGetComponent(out SheepGroupMovement sheepGroupMovement))
        {
            nearSheep = false;
            SheepGroup = null;
        }
    }
}


//PARTE DA INSERIRE NEI NEMICI
//void update()
//{
//    if (player.getcomponent<mimetismo>().isinvisible)
//    {
//        // ignora il player
//        return;
//    }

//    // continua a cercare/seguire il player
//}