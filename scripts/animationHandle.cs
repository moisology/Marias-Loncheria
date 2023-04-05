using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationHandle : MonoBehaviour
{

    private Animation anim;
    private float animationTimer;
    public string[] animiationNames;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animation>();
        animationTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        animationTimer += 1f;
        if (animationTimer >= 2f) { animationTimer = 0f; }
    }

    public void playAnimation(int index)
    {
        if (animationTimer == 0f)
        {
            anim.Play(animiationNames[index]);
        }
    }
}
