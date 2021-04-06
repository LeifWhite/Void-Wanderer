using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Void_Wanderer.ParticleSystems
{
    public class LandingParticleSystem : ParticleSystem
    {
        IParticleEmitter _emitter;
        public Color DirtColor;
        public LandingParticleSystem(IParticleEmitter emitter, Color color) : base(120)
        {
            _emitter = emitter;
            DirtColor = color;
        }
        protected override void InitializeConstants()
        {
            textureFilename = "VW Dirt Speck";
            minNumParticles = 1;
            maxNumParticles = 3;
            //blendState = BlendState.Additive;
           
        }
        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = _emitter.Velocity;
            var acceleration = new Vector2(RandomHelper.NextFloat(-200,200), RandomHelper.NextFloat(-200, -100));
            var scale = RandomHelper.NextFloat(0.5f, 1f);
            var lifetime = RandomHelper.NextFloat(0.1f, 0.5f);

            p.Initialize(where+new Vector2(_emitter.Size.X / 2, _emitter.Size.Y) +Vector2.UnitX*RandomHelper.NextFloat(-_emitter.Size.X/2, _emitter.Size.X/2), velocity, acceleration, DirtColor, lifetime: lifetime, scale: scale);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //AddParticles(_emitter.Position);
        }
       /// <summary>
       /// Adds particles
       /// </summary>
        public void SpawnParticles(float height)
        {
            for (int i = 0; i < Math.Pow(height, 1); i += 5)
            {
                AddParticles(_emitter.Position);
            }
        }
    }
}