using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    public void MoveVertical(float direction)
    {
        var positionChange = direction * speed * Time.deltaTime;
        transform.position += new Vector3(0, positionChange, 0);
    }
}
