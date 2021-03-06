﻿namespace AggregateFramework.Tests.TestDoubles
{
    internal class TestAggregate : AggregateBase<TestState>
    {
        public bool ActionWasPerformed { get; private set; }

        public TestAggregate(TestState state) : base(state)
        {
            ActionWasPerformed = false;
        }

        public TestAggregate()
        {
            ActionWasPerformed = false;
        }

        public void PerformAction()
        {
            ActionWasPerformed = true;
        }
    }
}
