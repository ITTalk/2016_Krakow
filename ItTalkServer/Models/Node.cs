
using System;

namespace ItTalkServer.Models
{
    public class Node
    {
        public int Id { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public string Image { get; private set; }

        public Node(int id, string image)
        {
            Id = id;
            RegisteredAt = DateTime.Now;
            Image = image;
        }
    }
}