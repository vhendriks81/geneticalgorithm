using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GeneticAlgorithm.Chromosome;
using GeneticAlgorithm.Termination;
using Xunit;

namespace GeneticAlgorithm.Tests
{
    public class StalenessTerminationTests
    {
        [Fact]
        public void ShouldTerminate_WhenStaleGenerationsExceeded_ReturnsTrue()
        {
            // Arrange
            var termination = new StalenessTermination<BitArray>(3);
            var session = new GeneticAlgorithmSession<BitArray>
            {
                CurrentPopulation = new List<Chromosome<BitArray>>
                {
                    new Chromosome<BitArray>(new BitArray(1)) { FitnessScore = 1 },
                    new Chromosome<BitArray>(new BitArray(1)) { FitnessScore = 1 }
                }
            };

            // Act
            var result1 = termination.ShouldTerminate(session);
            var result2 = termination.ShouldTerminate(session);
            var result3 = termination.ShouldTerminate(session);
            var result4 = termination.ShouldTerminate(session);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeTrue();
        }

        [Fact]
        public void ShouldTerminate_WhenFitnessImproves_ResetsStaleCount()
        {
            // Arrange
            var termination = new StalenessTermination<BitArray>(3);
            var session = new GeneticAlgorithmSession<BitArray>
            {
                CurrentPopulation = new List<Chromosome<BitArray>>
                {
                    new Chromosome<BitArray>(new BitArray(1)) { FitnessScore = 1 },
                    new Chromosome<BitArray>(new BitArray(1)) { FitnessScore = 1 }
                }
            };

            // Act
            var result1 = termination.ShouldTerminate(session);
            var result2 = termination.ShouldTerminate(session);
            session.CurrentPopulation.First().FitnessScore = 2;
            var result3 = termination.ShouldTerminate(session);
            var result4 = termination.ShouldTerminate(session);
            var result5 = termination.ShouldTerminate(session);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
            result3.Should().BeFalse();
            result4.Should().BeFalse();
            result5.Should().BeFalse();
        }
    }
}
