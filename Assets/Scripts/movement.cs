using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public float speed = 0;
    public float jumpspeed = 0;
    public float maxSpeed = 1.0f; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI TimerTxt;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private bool isGrounded; // Verificar se está no chão
    private int count;

    public Vector3 jump;
    private AudioSource audioSource;

    public float Timeleft;
    public bool TimeOn = false;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        TimeOn = true;
        SetCountText();
        winTextObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();

    }

    void OnMove(InputValue movementValue)
    {    
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }


    void SetCountText()
    {
        countText.text = "Pontos: " + count.ToString();
        if (count >= 17)
        {
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0F, movementY);
        rb.AddForce(movement * speed);

        // Limitar a velocidade máxima
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if(TimeOn){
            if(Timeleft > 0){
                Timeleft -= Time.deltaTime;
                updateTimer(Timeleft);
            }
            else{
                Debug.Log("Time is up");
                Timeleft =0;
                TimeOn = false;
            }
        }
        if(!TimeOn){
            SceneManager.LoadScene(2);
        }
        // Pulo
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Vector3 jump = new Vector3(0.0f, 35.0f, 0.0f);
            rb.AddForce(jump * jumpspeed);
        }

        isGrounded = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            audioSource.PlayOneShot(audioSource.clip);
            SetCountText();
        }
    }

    // Detecta se está tocando o chão
    private void OnCollisionStay()
    {
        isGrounded = true;

    }



    void updateTimer(float currentTime){

        currentTime +=1;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);


        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}