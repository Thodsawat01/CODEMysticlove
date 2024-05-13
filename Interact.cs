using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IInteractable
{
    void Interact();
}

public class Interact : MonoBehaviour
{
    public Transform Player;
    public float InteractRange;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Player == null)
            {
                return;
            }

            Ray ray = new Ray(Player.position, Player.forward);
            Debug.DrawRay(ray.origin, ray.direction * InteractRange, Color.green, 0.5f); // Debug draw the ray

            if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
            {
                GameObject hitObject = hitInfo.collider.gameObject;

                IInteractable interactable = hitObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}