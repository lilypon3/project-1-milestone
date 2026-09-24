using System.Collections.Generic;
using UnityEngine;

public class MessengerComponent : MonoBehaviour
{
    bool DEBUG_MODE = false;
    HashSet<IReceiverComponent> subscribers;

    void Awake()
    {
        subscribers = new HashSet<IReceiverComponent>();
        if (DEBUG_MODE) Debug.Log("Created subscribers set.");
    }

    public void send(IMessage message)
    {
        foreach (IReceiverComponent receiver in subscribers) receiver.receive(message);
        if (DEBUG_MODE) Debug.Log("Sent message: " + message.ToString());
    }

    public void subscribe(IReceiverComponent receiver)
    {
        subscribers.Add(receiver);
        if (DEBUG_MODE) Debug.Log("Added " + receiver.GetType() + " receiver. Number of subscribers: " + subscribers.Count.ToString());
    }

    public void unsubscribe(IReceiverComponent receiver)
    {
        subscribers.Remove(receiver);
        if (DEBUG_MODE) Debug.Log("Removed " + receiver.GetType() + " receiver. Number of subscribers: " + subscribers.Count.ToString());
    }


}
