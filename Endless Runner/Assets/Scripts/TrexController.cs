using UnityEngine;

public class TrexController : MonoBehaviour {

    private bool touchingGround = false;
    private float jumpForce = 15;
    private AudioSource audioSource;

    public AudioClip jump;
    public AudioClip coin;
    public AudioClip explosion;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (!GameController.Instance.IsRunning)
        {
            return;
        }

	    if (touchingGround && Input.GetButton("Fire1"))
        {
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            touchingGround = false;
            audioSource.PlayOneShot(jump);
        }
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            touchingGround = true;
        }
        else if (collision.gameObject.tag == "Death")
        {
            audioSource.PlayOneShot(explosion);
            GameController.Instance.GameOver();
        }
        else if (collision.gameObject.tag == "Coin")
        {
            audioSource.PlayOneShot(coin);
            collision.gameObject.transform.root.gameObject.SetActive(false);
            GameController.Instance.PickUpCoin();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            touchingGround = false;
        }
    }
}
