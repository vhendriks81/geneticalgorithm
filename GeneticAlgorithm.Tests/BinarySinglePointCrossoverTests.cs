using System.Collections;
using System.Collections.Generic;
using GeneticAlgorithm.Chromosome;
using GeneticAlgorithm.Crossover;
using Xunit;
using FluentAssertions;

namespace GeneticAlgorithm.Tests
{
    public class BinarySinglePointCrossoverTests
    {
        [Fact]
        public void Crossover_ShouldCreateChildrenWithMixedGenes()
        {
            // Arrange
            var parent1 = new Chromosome<BitArray>(new BitArray(new[] { true, true, true, true }));
            var parent2 = new Chromosome<BitArray>(new BitArray(new[] { false, false, false, false }));
            var crossover = new BinarySinglePointCrossover();
            var selection = new List<Chromosome<BitArray>> { parent1, parent2 };

            // Act
            var children = crossover.Crossover(selection);

            // Assert
            children.Should().HaveCount(4);
            foreach (var child in children)
            {
                child.GeneSequence[0].Should().BeTrue();
                child.GeneSequence[1].Should().BeTrue();
                child.GeneSequence[2].Should().BeTrue();
                child.GeneSequence[3].Should().BeTrue();
            }
        }
    }
}
