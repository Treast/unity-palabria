using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour
{

    public EventTrigger.TriggerEvent OnComplete;


    Ray ray;
    RaycastHit hit;

    private Selector selector;
    private AudioSource audioData;
    private Animator selectionAnimation;

    private bool hoverInTriggered = false;
    private bool hoverOutTriggered = false;

    private bool triggered = false;

    void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), Camera.main.GetComponent<Collider>());
        selector = GameObject.FindGameObjectWithTag("BackgroundSelector").GetComponent<Selector>();

        Debug.Log("COUCOU");
        ShowAnimationPointer();
    }

    void Update()
    {


        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0.5f);

        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {

            if (hit.transform.gameObject == gameObject)
            {

                if (this.hoverInTriggered)
                {
                    return;
                }
                Debug.Log(hit.transform.gameObject.name + " test " + gameObject.name);
                this.hoverInTriggered = true;
                this.hoverOutTriggered = false;

                MouseEnter();
            }
            else
            {
                if (this.hoverOutTriggered || !this.hoverInTriggered)
                {
                    return;
                }
                this.hoverInTriggered = false;
                this.hoverOutTriggered = true;
                MouseLeave();
            }
        }
    }

    void ShowAnimationPointer()
    {

        selectionAnimation = selector.GetComponent<Animator>();
        EventManager.StartListening("OnSelectionAnimationFinished", OnSelectionAnimationFinished);

        selector.showInteractionPointer();
    }

    void OnSelectionAnimationFinished()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.gameObject.name + " Finale " + gameObject.name);
            if (hit.transform.gameObject == gameObject)
            {

                triggered = true;

                OnComplete.Invoke(null);

                selector.hideInteractionPointer();

                triggered = false;
                MouseLeave();
            }
        }
    }

    public void MouseEnter()
    {
        Debug.Log("Avant trigger");
        Debug.Log("TESTSDT " + selector.GetComponent<Animator>());
        if (triggered) return;

        Debug.Log("GOGOGO");
        selectionAnimation.ResetTrigger("SelectorCanceled");
        selectionAnimation.SetTrigger("SelectorAppearing");
    }

    public void MouseLeave()
    {
        if (triggered) return;

        Debug.Log("STOPOPOPO");
        selectionAnimation.ResetTrigger("SelectorAppearing");
        selectionAnimation.SetTrigger("SelectorCanceled");
    }
    public void animationOfFlower()
    {
        Debug.Log(selector);
        //triggered = false;
        //Instantiate(hitGameObject, transform.position, Quaternion.identity);
    }

    public void playAudioOfFlower()
    {

        audioData = gameObject.GetComponent<AudioSource>();

        //AudioManager.Instance.CurrentAudio.g;

        //StartCoroutine(AudioController.FadeIn(audioData, 1f));
        // Jouer l'animation de la fleu 
    }
}