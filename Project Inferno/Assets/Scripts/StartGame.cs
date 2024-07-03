using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Animator mAnimator;
    public GameObject uiCanvas; // Reference to the Canvas Group
    private bool isAnimating = false; // Flag to check if animation is playing

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();

        if (uiCanvas != null){
            uiCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null && !isAnimating){
            if(Input.GetMouseButtonDown(0)){
                mAnimator.SetTrigger("Start");
                StartCoroutine(WaitForAnimation());
            }
        }
    }

    // Coroutine to wait for the animation to complete
    private IEnumerator WaitForAnimation()
    {
        isAnimating = true;
        yield return new WaitForSeconds(8f);

        if (uiCanvas != null)
        {
            uiCanvas.SetActive(true);
        }
    }
}
