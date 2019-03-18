using LifeLike.Shared.Models;
using System;

namespace LifeLike.Shared.Model
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}