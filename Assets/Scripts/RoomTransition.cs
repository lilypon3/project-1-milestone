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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coroutineParameters = new CoroutineParameters(false);
        _transform = gameObject.transform;

        messenger = gameObject.GetComponent<MessengerComponent>();
        messenger.subscribe(this);
        messageQueue = new Queue<ArrowInputMessage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (messageQueue.Count > 0 && !coroutineParameters.getRunning())
        {
            runTransition(messageQueue.Dequeue());
        }
    }

    public void receive(IMessage message)
    {
        if (message.GetType() == typeof(ArrowInputMessage)) 
                messageQueue.Enqueue((ArrowInputMessage) message);
    }

    void runTransition(ArrowInputMessage message)
    {
        int direction = message.getInput();
        Vector3 finalPos = _transform.position;

        if (direction == ArrowInputMessage.RIGHT_ARROW) finalPos = _transform.position - rightRoomVector;
        else if (direction == ArrowInputMessage.LEFT_ARROW) finalPos = _transform.position + rightRoomVector;
        else if (direction == ArrowInputMessage.UP_ARROW) finalPos = _transform.position + upRoomVector;
        else if (direction == ArrowInputMessage.DOWN_ARROW) finalPos = _transform.position - upRoomVector;
        
        StartCoroutine(CoroutineUtilities.moveObjectOverTime(_transform,
                                                             _transform.position, 
                                                             finalPos, 
                                                             transitionDuration, 
                                                             coroutineParameters));
    }

}

