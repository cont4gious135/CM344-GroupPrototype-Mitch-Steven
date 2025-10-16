using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class WinStateManager : MonoBehaviour
{
    public GameObject BlueDoor;
    public GameObject RedDoor;
    public GameObject YellowDoor;
    private SpriteRenderer m_spriteRenderer;
    private CapsuleCollider2D m_capsuleCollider2D;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(BlueDoor.GetComponent<BlueDoorScript>().locked == false && RedDoor.GetComponent<RedDoorScript>().locked == false && YellowDoor.GetComponent<YellowDoorScript>().locked == false)
        {
            m_capsuleCollider2D.enabled = true;
            m_spriteRenderer.enabled = true;
        }
    }
}
