using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Fruit_Dropped : Fruit
{
    [SerializeField] private Vector2 speed;
    [SerializeField] private Color transparentColor;
                     protected bool canPickup;

    //this function allow us to do something with delay
    protected virtual IEnumerator BlinkImage()
    {
        anim.speed = 0;
        sr.color = transparentColor;

        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;
        speed.x *= -1;

        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;
        speed.x *= -1;

        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
        speed.x *= -1;

        yield return new WaitForSeconds(.2f);
        sr.color = transparentColor;
        speed.x *= -1;

        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;
        speed.x *= -1;

        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;
        speed.x *= -1;

        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
        speed.x *= -1;

        yield return new WaitForSeconds(.2f);
        sr.color = transparentColor;
        speed.x *= -1;

        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;
        speed.x *= -1;

        yield return new WaitForSeconds(.3f);

        speed.x = 0; 
        anim.speed = 1;
        canPickup = true;
       
            
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StartCoroutine(BlinkImage());  
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(canPickup)
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed.x, speed.y) * Time.deltaTime;
    }
}
