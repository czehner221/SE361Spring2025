using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame
{
    public class Enemy : Character
    {
        public Enemy(string name, int top, int bottom, int left, int right, int speed, Image image)
            : base(name, top, bottom, left, right, speed, image)
        {

        }
    }
}
