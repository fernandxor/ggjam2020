using UnityEngine;

public class Pickable : MonoBehaviour
{
    Rigidbody2D rb;

    public Rigidbody2D Rb
    {
        get
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();
            return rb;
        }
    }
    
    protected bool isPicked;

    public bool IsPicked { get => isPicked; }

    public virtual void Pick(Transform grabPos) {
        isPicked = true;
        Rb.isKinematic = true;
        //Rb.velocity = Vector2.zero;
        //Rb.angularVelocity = 0;
        transform.SetParent(grabPos);
        transform.localPosition = Vector2.zero;
    }

    public virtual void Drop() {
        isPicked = false;
        Rb.isKinematic = false;
        transform.SetParent(null);
    }
}
