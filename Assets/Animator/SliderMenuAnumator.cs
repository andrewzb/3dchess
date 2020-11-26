using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenuAnumator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void ToggleRewindSlider()
    {
        if (animator != null)
        {
            bool isOpen = animator.GetBool("show");
            animator.SetBool("show", !isOpen);
        }
    }
}
