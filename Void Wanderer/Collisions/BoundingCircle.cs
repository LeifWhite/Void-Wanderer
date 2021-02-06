using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Void_Wanderer.Collisions
{
    /// <summary>
    /// This class was modified from the CollisionExample tutorial in CIS 580
    /// </summary>
    public struct BoundingCircle
    {
        /// <summary>
        /// center of circle
        /// </summary>
        public Vector2 Center;
        /// <summary>
        /// radius of circle
        /// </summary>
        public float Radius;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
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