using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /* Movement variables */
    [SerializeField] float controlSpeed = 35f;
    [SerializeField] float xClampRange = 10f;
    [SerializeField] float posYClampRange = 17f;
    [SerializeField] float negYClampRange = -6f;

    /* Rotation variables */
    [SerializeField] float controlRollFactor = 25f;
    [SerializeField] float controlPitchFactor = 20f;
    [SerializeField] float rotationSpeed = 10f;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }


    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    private void ProcessTranslation()
    {
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xClampRange, xClampRange);

        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, negYClampRange, posYClampRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0f);
    }

    private void ProcessRotation()
    {
        float pitch = controlPitchFactor * movement.y;
        float roll = controlRollFactor * movement.x;

        Quaternion targetRotation = Quaternion.Euler(-pitch, 0f, -roll); 
        // Having one of these values be negative allows us to combine with a postive or negative x vector to give us positive or negative movement
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
