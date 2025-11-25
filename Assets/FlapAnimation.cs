using UnityEngine;

public class FlapAnimation : MonoBehaviour
{
    public Animator flapAnimator1;
    public Animator flapAnimator2;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            flapAnimator1.SetTrigger("Flap1");
            flapAnimator2.SetTrigger("Flap2");
        }
    }
}
