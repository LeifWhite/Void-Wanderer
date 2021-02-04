using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Void_Wanderer.Collisions
{
    /// <summary>
    /// This class was modified from the CollisionExample tutorial in CIS 580
    /// </summary>
    public struct BoundingPoint
    {
        public float X;
        public float Y;
        
        public BoundingPoint(float x, float y)
        {
            X = x;
            Y = y;
           

        }
        public BoundingPoint(Vector2 position)
        {
            X = position.X;
            Y = position.Y;
           

        }

        /// <summary>
        /// Determines if this BoundingPoint collides with a BoundingRectangle
        /// </summary>
        /// <param name="r">the BoundingRectangle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle r)
        {
            return CollisionHelper.Collides(r, this);
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
    }
}
