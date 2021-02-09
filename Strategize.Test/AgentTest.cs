using System;
using System.Collections.Generic;
using Xunit;

namespace Strategize.Test
{
    public class AgentTest
    {
        [Fact]
        public void WithLowEnergyEatingShouldBePrioritized()
        {
            IAgent<ExampleContext> agent = new Agent<ExampleContext>();
            var context = new ExampleContext(0);
            agent.Consider(ExampleStrategy.Singleton, context);
            agent.Tick(context);
            
            Assert.Equal(EatAction.Singleton, agent.CurrentAction);
            Assert.Equal(100, context.Energy);
            Assert.Equal(0, context.TimesExercised);
        }
        
        [Fact]
        public void WithHighEnergyExercisingShouldBePrioritized()
        {
            IAgent<ExampleContext> agent = new Agent<ExampleContext>();
            var context = new ExampleContext(100);
            agent.Consider(ExampleStrategy.Singleton, context);
            agent.Tick(context);
            
            Assert.Equal(ExerciseAction.Singleton, agent.CurrentAction);
            Assert.Equal(75, context.Energy);
            Assert.Equal(1, context.TimesExercised);
        }

        private class ExampleContext
        {
            public ExampleContext(byte energy)
            {
                Energy = energy;
            }
            
            public byte Energy { get; private set; }
            
            public int TimesExercised { get; private set; }

            public void EatSomething()
            {
                Energy = 100;
            }

            public void Exercise()
            {
                if (Energy <= 25)
                    throw new InvalidOperationException("Not enough energy");

                Energy -= 25;
                TimesExercised++;
            }
        }

        private class ExampleStrategy : IStrategy<ExampleContext>
        {
            public static readonly ExampleStrategy Singleton = new ExampleStrategy();
            private static readonly Lazy<IEnumerable<IAction<ExampleContext>>> _actions =
                new Lazy<IEnumerable<IAction<ExampleContext>>>(() => new IAction<ExampleContext>[] {EatAction.Singleton, ExerciseAction.Singleton, });

            private ExampleStrategy() {}

            public IEnumerable<IAction<ExampleContext>> Actions => _actions.Value;
        }

        private class EatAction : IAction<ExampleContext>
        {
            public static readonly EatAction Singleton = new EatAction();
            
            private EatAction() {}
            
            public IStrategy<ExampleContext> Strategy => ExampleStrategy.Singleton;
            
            public float EvaluatePriority(ExampleContext context)
            {
                return context.Energy <= 25 ? 100 : 0;
            }

            public void OnEnter(ExampleContext context)
            {
                // no action required
            }

            public ActionResult OnTick(ExampleContext context)
            {
                context.EatSomething();
                return ActionResult.Yield;
            }

            public void OnFinish(ExampleContext context)
            {
                // no action required
            }
        }

        private class ExerciseAction : IAction<ExampleContext>
        {
            public static readonly ExerciseAction Singleton = new ExerciseAction();
            
            private ExerciseAction() {}
            
            public IStrategy<ExampleContext> Strategy => ExampleStrategy.Singleton;
            
            public float EvaluatePriority(ExampleContext context)
            {
                return 50;
            }

            public void OnEnter(ExampleContext context)
            {
                // no action required
            }

            public ActionResult OnTick(ExampleContext context)
            {
                context.Exercise();
                return ActionResult.Yield;
            }

            public void OnFinish(ExampleContext context)
            {
                // no action required
            }
        }
    }
}