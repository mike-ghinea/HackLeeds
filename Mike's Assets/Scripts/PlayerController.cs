using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    public int collisionCount;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        collisionCount = 0;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            countText.text = "Count: " + count.ToString();
            SetCountText();
            if (count >= 13)
            {
                winText.text = "YOU WIN";
                SceneManager.LoadScene("puzzle");
            }
                
        }

        if (other.gameObject.CompareTag("Finished"))
        {
            winText.text = "YOU WIN";
            speed = 0;
            ExecuteAfterTime(10);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        Debug.Log(message: "Collision" + collisionCount);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white, duration: 5);
        }
        if (collisionCount > 1)
        {
            winText.text = "YOU DED";
            speed = 0;

        }
    }
}