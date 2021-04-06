using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Void_Wanderer.ParticleSystems
{
    public class TeleportParticleSystem : ParticleSystem
    {
        IParticleEmitter _emitter;
       
        public TeleportParticleSystem(IParticleEmitter emitter) : base(600)
        {
            _emitter = emitter;
           
        }
        protected override void InitializeConstants()
        {
            textureFilename = "PixelCircle";
            minNumParticles = 1;
            maxNumParticles = 3;
            blendState = BlendState.Additive;
           
        }
        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = _emitter.Velocity;
            var acceleration = new Vector2(RandomHelper.NextFloat(-400,400), RandomHelper.NextFloat(-400, 400));
            var scale = RandomHelper.NextFloat(1f, 2f);
            var lifetime = RandomHelper.NextFloat(0.3f, 0.9f);

            p.Initialize(where+new Vector2(_emitter.Size.X / 2, _emitter.Size.Y/2)+new Vector2(RandomHelper.NextFloat(-_emitter.Size.X/2, _emitter.Size.X/2), RandomHelper.NextFloat(-_emitter.Size.X / 2, _emitter.Size.X / 2)), velocity, acceleration, Color.Purple, lifetime: lifetime, scale: scale);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //AddParticles(_emitter.Position);
        }
       /// <summary>
       /// Adds particles
       /// </summary>
        public void SpawnParticles()
        {
            for (int i = 0; i < 100; i++)
            {
                AddParticles(_emitter.Position);
            }
            
        }
    }
}