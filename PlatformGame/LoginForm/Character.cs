using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame
{
    public class Character
    {
        public string name_ { get; set; }
        public int top_ { get; set; }
        public int bottom_ { get; set; }
        public int left_ { get; set; }
        public int right_ { get; set; }
        public int speed_ { get; set; }
        public Image image_ { get; set; }

        public Character(string name, int top, int bottom, int left, int right, int speed, Image image)
        {
            name_ = name;
            top_ = top;
            bottom_ = bottom;
            left_ = left;
            right_ = right;
            speed_ = speed;
            image_ = image;
        }
    }
}
