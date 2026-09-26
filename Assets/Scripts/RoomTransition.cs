using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoomTransition : MonoBehaviour, IReceiverComponent
{
    [SerializeField] Vector3 rightRoomVector;
    [SerializeField] Vector3 upRoomVector;
    [SerializeField] float transitionDuration;
    CoroutineParameters coroutineParameters;
    Transform _transform;
    MessengerComponent messenger;
    Queue<ArrowInputMessage> messageQueue;
    GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coroutineParameters = new CoroutineParameters(false);
        _transform = gameObject.transform;

        messenger = gameObject.GetComponent<MessengerComponent>();
        messenger.subscribe(this);
        messageQueue = new Queue<ArrowInputMessage>();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("RoomTransition could not find a GameObject tagged 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (messageQueue.Count > 0 && !coroutineParameters.getRunning())
        {
            ArrowInputMessage msg = messageQueue.Dequeue();
            DoorDirection direction = ConvertInputToDirection(msg.getInput());
            TriggerTransition(direction, player);
        }
    }

    public void receive(IMessage message)
    {
        if (message.GetType() == typeof(ArrowInputMessage)) 
                messageQueue.Enqueue((ArrowInputMessage) message);
    }

    public DoorDirection ConvertInputToDirection(int input)
    {
        if (input == ArrowInputMessage.RIGHT_ARROW) return DoorDirection.Right;
        if (input == ArrowInputMessage.LEFT_ARROW) return DoorDirection.Left;
        if (input == ArrowInputMessage.UP_ARROW) return DoorDirection.Up;
        return DoorDirection.Down;
    }

    public void TriggerTransition(DoorDirection direction, GameObject player)
    {
    if (coroutineParameters.getRunning()) return; // ignore re-triggers mid-transition

    Vector3 finalPos = _transform.position;
    Vector3 playerNewPos = player.transform.position;

    float horizontal_shift = 4f;
    float vertical_shift = 5f;

    if (direction == DoorDirection.Right)
    {
        finalPos = _transform.position - rightRoomVector;
        playerNewPos = player.transform.position + new Vector3(horizontal_shift, 0, 0);
    }
    else if (direction == DoorDirection.Left)
    {
        finalPos = _transform.position + rightRoomVector;
        playerNewPos = player.transform.position - new Vector3(horizontal_shift, 0, 0);
    }
    else if (direction == DoorDirection.Up)
    {
        finalPos = _transform.position + upRoomVector;
        playerNewPos = player.transform.position + new Vector3(0, vertical_shift, 0);
    }
    else if (direction == DoorDirection.Down)
    {
        finalPos = _transform.position - upRoomVector;
        playerNewPos = player.transform.position - new Vector3(0, vertical_shift, 0);
    }

    // Disable Link's controls during the pan so he can't walk into walls mid-transition
    MonoBehaviour movement = player.GetComponent<GridMovement>(); 
    if (movement != null) movement.enabled = false;

    StartCoroutine(TransitionAndReenable(finalPos, player, playerNewPos, movement));
}

IEnumerator TransitionAndReenable(Vector3 cameraTarget, GameObject player, Vector3 playerTarget, MonoBehaviour movement)
{
    yield return CoroutineUtilities.moveObjectOverTime(_transform, _transform.position, cameraTarget, transitionDuration, coroutineParameters);
    player.transform.position = playerTarget;
    if (movement != null) movement.enabled = true;
}

}

