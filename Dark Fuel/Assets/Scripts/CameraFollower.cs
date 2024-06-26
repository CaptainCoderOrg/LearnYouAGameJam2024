using System.Collections;
using System.Collections.Generic;
using CaptainCoder.UnityEngine;
using NaughtyAttributes;
using UnityEngine;

namespace CaptainCoder.DarkFuel
{
    public class CameraFollower : MonoBehaviour
    {

        [field: SerializeField]
        public Transform Target { get; set; }
        public RoomController Room { get; private set; }
        public RoomController QueuedRoom { get; private set; }

        public void QueueRoom(RoomController newRoom)
        {
            QueuedRoom = newRoom;
            if (Room == null)
            {
                LeaveRoom(null);
            }
        }

        public void LeaveRoom(RoomController lastRoom)
        {
            if (Room != lastRoom) { return; }
            if (Room != null)
            { 
                Room.Hide(); 
            }
            Room = QueuedRoom;
            CenterCamera();
        }

        [Button("Center Camera")]
        public void CenterCamera()
        {
            transform.position = Room.CameraFocus.transform.position;
            Room.Show();
        }
    }
}