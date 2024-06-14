using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0;
    private bool isFading = false;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isFading)
        {
            Debug.Log("Already fading, ignoring OnStateEnter for " + animator.gameObject.name);
            return;  // If already fading, do not reset
        }

        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + animator.gameObject.name);
            return;
        }
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
        isFading = true;  // Start fading process
        Debug.Log("OnStateEnter called for " + animator.gameObject.name);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isFading || spriteRenderer == null) return;  // Only update if fading

        timeElapsed += Time.deltaTime;
        float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime));
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
        if (timeElapsed > fadeTime)
        {
            Debug.Log("Destroying " + objToRemove.name);
            Destroy(objToRemove);
            isFading = false;  // Reset fading process
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isFading = false;  // Ensure fading is reset when state exits
        Debug.Log("OnStateExit called for " + animator.gameObject.name);
    }
}
