using System.Collections;
using System.Collections.Generic;
using CaptainCoder.UnityEngine;
using UnityEngine;

namespace CaptainCoder.DarkFuel
{
    public class CameraFollower : MonoBehaviour
    {

        [field: SerializeField]
        public Transform Target { get; set; }
        public RoomController Room;
        void Awake()
        {
        }

        public void SetRoom(RoomController newRoom)
        {
            if (Room != null) 
            { 
                Room.Hide(); 
            }
            Room = newRoom;
            transform.position = Room.CameraFocus.transform.position;
            Room.Show();
        }
    }
}