using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movementScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float rcsrotate = 100f; // rcs= reaction control system
    [SerializeField] float rcsthrust = 100f; // rcs= reaction control system
    [SerializeField] float levelLoadDelay = 2f;

    Rigidbody rigidbody;

    enum State { Alive, Dead, Transcending }
    [SerializeField] bool isCollision = true; // default is collisions on
    [SerializeField] State state = State.Alive;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            FixRotationPosition();
            RespondToRotateInput();
            RespondToThrustInput();
        }

    }

    private void FixRotationPosition()
    {
        float ms = Time.time % 1;
        if (ms < 0.1) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            float zRotation = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }
        
    }

    private void RespondToRotateInput()
    {

        float rotationthisframe = rcsrotate * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            ManualRotate(rotationthisframe);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ManualRotate(-rotationthisframe);
        }


    }

    private void ManualRotate(float rotationthisframe)
    {
        rigidbody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward * rotationthisframe);
        rigidbody.freezeRotation = false; // resume phisics control control of rotation
    }

    private void RespondToThrustInput()
    {
        float thrustthisframe = rcsthrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            applythrust(thrustthisframe);
        }
    }

    private void applythrust(float thrustthisframe)
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustthisframe);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (isCollision == true)
        {
            if (state != State.Alive)
                return;
            switch (collision.gameObject.tag)
            {
                case "right_answer":
                    print ("landing");
                    Landing();
                    break;
                case "wrong_answer":
                    state = State.Dead;
                    print("dead");
                    Invoke("ReloadScene", levelLoadDelay);
                    break;
                case "Obstacle":
                    state = State.Dead;
                    print("dead");
                    Invoke("ReloadScene", levelLoadDelay);
                    break;

            }
        }
        else { return; }
    }



    private void Landing()
    {
        state = State.Transcending;
        Invoke("loadnextscene", levelLoadDelay);
        //rigidbody.AddRelativeForce(Vector3.up * 100);
    }

    private void loadnextscene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;// gives you current scene index
        int nextSceneIndex;
        int lastScene = SceneManager.sceneCountInBuildSettings - 1; // gives you number of levels -1
        if (currentSceneIndex == lastScene)
            nextSceneIndex = 0;
        else
            nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void RespondToCINPUT()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCollision = !(isCollision);
        }

    }
}
