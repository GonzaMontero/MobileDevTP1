using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTutorial : MonoBehaviour
{
    bool finishedTutorial = false;
    int tutorialPhase = 0;
    bool moving = false;

    [SerializeField] Vector3[] positions;
    [SerializeField] GameObject bag;
    [SerializeField] Sprite[] images;
    [SerializeField] SpriteRenderer tutorialScreen;

    enum PlayerKeys
    {
        ASDW,
        ARROWS
    }

    [SerializeField] PlayerKeys t;

    [SerializeField] GameObject[] buttons;

    private void Start()
    {
        bag.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!finishedTutorial)
            switch (t)
            {
                case PlayerKeys.ASDW:
                    if (!moving)
                    {
                        if (Input.anyKeyDown)
                        {
                            //We check all keys and pass the key and corresponding sprite to the handler
                            foreach (KeyCode keyPressed in System.Enum.GetValues(typeof(KeyCode))) 
                            {
                                if (Input.GetKey(keyPressed))
                                {
                                    switch (tutorialPhase)
                                    {
                                        case 0:
                                            HandleKey(keyPressed, images[0]);
                                            break;
                                        case 1:
                                            HandleKey(keyPressed, images[1]);
                                            break;
                                        case 2:
                                            HandleKey(keyPressed, images[3]);
                                            break;
                                        case 3:
                                            HandleKey(keyPressed, images[4]);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                        break;
                case PlayerKeys.ARROWS:
                    if (!moving)
                    {
                        if (Input.anyKeyDown)
                        {
                            foreach (KeyCode keyPressed in System.Enum.GetValues(typeof(KeyCode)))
                            {
                                if (Input.GetKey(keyPressed))
                                {
                                    switch (tutorialPhase)
                                    {
                                        case 0:
                                            HandleKey(keyPressed, images[0]);
                                            break;
                                        case 1:
                                            HandleKey(keyPressed, images[1]);
                                            break;
                                        case 2:
                                            HandleKey(keyPressed, images[3]);
                                            break;
                                        case 3:
                                            HandleKey(keyPressed, images[4]);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
    }

    void HandleKey(KeyCode k, Sprite spr)
    {
        switch (k)
        {
            case KeyCode.W:
            case KeyCode.UpArrow:
                if (tutorialPhase == 0)
                {
                    bag.gameObject.SetActive(true);
                    tutorialPhase = 1;
                }
                break;

            case KeyCode.A:
            case KeyCode.LeftArrow:
                if (tutorialPhase == 1)
                {
                    tutorialPhase = 2;
                    StartCoroutine(Move());
                }
                break;

            case KeyCode.S:
            case KeyCode.DownArrow:
                if (tutorialPhase == 2)
                {
                    tutorialPhase = 3;
                    StartCoroutine(Move());
                }
                break;

            case KeyCode.D:
            case KeyCode.RightArrow:
                if (tutorialPhase == 3)
                {
                    StartCoroutine(Move());
                }
                break;
            default:
                return;
        }
        tutorialScreen.sprite = spr;
    }


    bool button0Pressed;
    bool button1Pressed;
    bool button2Pressed;
    bool button3Pressed;

    public void ButtonPressed(int b)
    {
        if (b == 0 && !button0Pressed)
        {
            ButtonHandler(images[0], buttons[1].gameObject);

            bag.gameObject.SetActive(true);
            tutorialPhase = 1;
            button0Pressed = true;
        }
        else if (b == 1 && !button1Pressed)
        {
            ButtonHandler(images[1], buttons[2].gameObject);

            tutorialPhase = 2;
            StartCoroutine(Move());
            button1Pressed = true;
        }
        else if (b == 2 && !button2Pressed)
        {
            ButtonHandler(images[2], buttons[3].gameObject);

            tutorialPhase = 3;
            button2Pressed = true;
        }
        else if (b == 3 && !button3Pressed)
        {
            //Since this is a special case, we ignore the Handler
            StartCoroutine(Move());
            tutorialScreen.sprite = images[3];
            button3Pressed = true;
        }
    }

    void ButtonHandler(Sprite spr, GameObject buttonToEnable)
    {
        tutorialScreen.sprite = spr;
        buttonToEnable.SetActive(true);
    }

    IEnumerator Move()
    {
        moving = true;
        while (bag.transform.position != positions[tutorialPhase])
        {
            bag.transform.position = Vector3.MoveTowards(bag.transform.position, positions[tutorialPhase], 100 * Time.deltaTime);
            yield return null;
        }
        moving = false;

        if (tutorialPhase == 3)
            finishedTutorial = true;
        StopCoroutine(Move());
    }

    public bool GetFinishedTutorial()
    {
        return finishedTutorial;
    }
}
