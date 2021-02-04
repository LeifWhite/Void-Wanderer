using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Void_Wanderer.Collisions
{
    public struct BoundingCircle
    {
        public Vector2 Center;
        public float Radius;
        public BoundingCircle(Vector2 c, float r)
        {
            Center = c;
            Radius = r;
        }
        /// <summary>
        /// Determines if this BoundingPoint collides with a BoundingCircle
        /// </summary>
        /// <param name="c">the BoundingCircle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle c)
        {
            return CollisionHelper.Collides(c, this);
        }
        /// <summary>
        /// Determines if this BoundingPoint collides with a BoundingRectangle
        /// </summary>
        /// <param name="c">the BoundingRect</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle c)
        {
            return CollisionHelper.Collides(c, this);
        }
    }
}