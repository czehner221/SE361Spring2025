using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame
{
    public class Player : Character
    {
        public int jumpSpeed_ { get; set; }
        public int gravity_ { get; set; }
        public bool directionFacing_ { get; set; }
        public int score_ { get; set; }
        public bool grounded_ { get; set; }
        public bool goLeft_ { get; set; }
        public bool goRight_ { get; set; }
        public bool jumping_ { get; set; }
        public bool lastKeyRight_ { get; set; }
        public bool lastKeyLeft_ { get; set; }

        public Player(string name, int top, int bottom, int left, int right, int speed, Image image)
            : base(name, top, bottom, left, right, speed, image)
        {
            jumpSpeed_ = 0;
            gravity_ = 0;
            directionFacing_ = true;
            score_ = 0;
            grounded_ = true;
            goLeft_ = false;
            goRight_ = false;
            jumping_ = false;
            lastKeyRight_ = true;
            lastKeyLeft_ = false;
        }
    }
}
