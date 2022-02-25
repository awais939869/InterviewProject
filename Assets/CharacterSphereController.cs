using Services;
using TMPro;
using Tools.Pool;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSphereController : MonoBehaviour, IPoolInit, IPoolOnSpawn, IPoolOnDespawn
{
    [SerializeField] private Rigidbody characterRigidbody;
    [SerializeField] private float moveSpeed;
    public GameObject canvus;
    public string[] Dialogs;
    int dialogNum=0;
    bool flag;
    // Update is called once per frame
    void Update()
    {
        var netForce = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            netForce += (Vector3.forward * moveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            netForce += (Vector3.back * moveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            netForce += (Vector3.left * moveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            netForce += (Vector3.right * moveSpeed * Time.deltaTime);
        }
        
        if (Input.anyKey == false)
        {
            netForce = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.Space) && !flag)
        {
            flag = true;
            canvus.SetActive(true);
            Debug.Log(canvus.transform.GetChild(0).GetChild(0).gameObject.name);
            canvus.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Dialogs[dialogNum]);
            dialogNum++;
            if(dialogNum==Dialogs.Length)
            {
                dialogNum = 0;
            }
            Invoke(nameof(canvusoff), 3);
        }

        characterRigidbody.AddForce(netForce);
    }

    public void canvusoff()
    {
        flag = false;
        canvus.SetActive(false);
    }
    public void Init()
    {
        Debug.Log("Character Initialized");
    }

    public void OnSpawn(PoolService poolService)
    {
        Debug.Log("Character Spawned");
    }

    public void OnDespawn()
    {
        Debug.Log("Character Despawned");
    }
}
