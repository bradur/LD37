// Date   : 10.12.2016 14:22
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Animator animator;

    private float fadeTime = 1f;

    private bool dying = false;
    private bool visible = false;

    public void Die()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public void Init(string message, Transform parent, float messageFadeTime)
    {
        transform.SetParent(parent, false);
        txtComponent.text = message;
        fadeTime = messageFadeTime;
    }

    public void Show()
    {
        if (!dying)
        {
            visible = true;
            animator.SetTrigger("Show");
        }
    }

    public void MoveUp()
    {
        if (!dying)
        {
            animator.SetTrigger("MoveUp");
        }
    }

    public void StartDying()
    {
        dying = true;
        animator.SetTrigger("Die");
    }

    void Update()
    {
        if (visible)
        {
            if (!dying)
            {
                fadeTime -= Time.unscaledDeltaTime;
                if (fadeTime <= 0.05f)
                {
                    animator.SetTrigger("MoveUp");
                }
            }
        }
    }

}
