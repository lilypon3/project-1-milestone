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
    public Vector2Int current_room;                 // set start room in Inspector
    public RoomManager room_manager;

    public Vector2Int StartingRoom => room_manager != null ? room_manager.starting_room : current_room;
    static Vector2Int ToDelta(DoorDirection d) =>
        d == DoorDirection.Right ? Vector2Int.right :
        d == DoorDirection.Left  ? Vector2Int.left  :
        d == DoorDirection.Up    ? Vector2Int.up    : Vector2Int.down;


    void Start()
{
    if (room_manager != null) current_room = room_manager.starting_room;
    messageQueue = new Queue<ArrowInputMessage>();
    coroutineParameters = new CoroutineParameters(false);
    _transform = gameObject.transform;

    messenger = gameObject.GetComponent<MessengerComponent>();
    if (messenger != null) messenger.subscribe(this);

    player = GameObject.FindGameObjectWithTag("Player");
    if (player == null)
    {
        Debug.LogWarning("RoomTransition could not find a GameObject tagged 'Player'.");
    }
}

    // Update is called once per frame
    void Update()
    {
        if (messageQueue == null || coroutineParameters == null) return;
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

        float horizontal_shift = 5f;
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

        if (room_manager != null) room_manager.Exit(current_room);
        StartCoroutine(TransitionAndReenable(finalPos, player, playerNewPos, movement, direction));
    }

    IEnumerator TransitionAndReenable(Vector3 cameraTarget, GameObject player, Vector3 playerTarget, MonoBehaviour movement, DoorDirection direction)
    {
        yield return CoroutineUtilities.moveObjectOverTime(_transform, _transform.position, cameraTarget, transitionDuration, coroutineParameters);
        player.transform.position = playerTarget;
        if (movement != null) movement.enabled = true;
        current_room += ToDelta(direction);   // pass direction into the coroutine
        if (room_manager != null) room_manager.Enter(current_room);
    }

    public void SetCurrentRoom(Vector2Int room)
    {
        if (room == current_room) return;
        if (room_manager != null) room_manager.Exit(current_room);
        current_room = room;
        if (room_manager != null) room_manager.Enter(current_room);
    }

    public void SnapCameraTo(Vector3 pos)
    {
        _transform.position = new Vector3(pos.x, pos.y, _transform.position.z);
    }

    public void TeleportToRoom(Vector2Int room, Vector3 playerPos, Vector3 cameraPos)
{
    if (coroutineParameters != null && coroutineParameters.getRunning()) return;

    if (room_manager != null) room_manager.Exit(current_room);
    current_room = room;

    SnapCameraTo(cameraPos);

    GameObject p = player != null ? player : GameObject.FindGameObjectWithTag("Player");
    Rigidbody2D prb = p.GetComponent<Rigidbody2D>();
    prb.position = playerPos;
    prb.linearVelocity = Vector2.zero;
    p.transform.position = playerPos;

    if (room_manager != null) room_manager.Enter(current_room);
}

}

