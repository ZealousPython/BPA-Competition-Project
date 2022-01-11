using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// make the characters in their sprites move when the mouse enters the button
public class HighlightPlayerMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject sprite;
    private Animator anim;
    void Start()
    {
        Time.timeScale = 1;
        anim = sprite.GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("move", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("move", false);
    }
}
