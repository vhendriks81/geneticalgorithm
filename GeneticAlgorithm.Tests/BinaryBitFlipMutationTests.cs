using Xunit;
using GeneticAlgorithm.Mutation;
using GeneticAlgorithm.Chromosome;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;

namespace GeneticAlgorithm.Tests
{
    public class BinaryBitFlipMutationTests
    {
        [Fact]
        public void Mutate_ShouldFlipBitsBasedOnProbability()
        {
            // Arrange
            var mutationProbability = 1.0; // 100% probability to ensure mutation
            var mutation = new BinaryBitFlipMutation(mutationProbability);
            var chromosome = new Chromosome<BitArray>(new BitArray(new bool[] { true, false, true }));
            var population = new List<Chromosome<BitArray>> { chromosome };

            // Act
            mutation.Mutate(population);

            // Assert
            chromosome.GeneSequence[0].Should().BeFalse();
            chromosome.GeneSequence[1].Should().BeTrue();
            chromosome.GeneSequence[2].Should().BeFalse();
        }

        [Fact]
        public void Mutate_ShouldNotFlipBitsWhenProbabilityIsZero()
        {
            // Arrange
            var mutationProbability = 0.0; // 0% probability to ensure no mutation
            var mutation = new BinaryBitFlipMutation(mutationProbability);
            var chromosome = new Chromosome<BitArray>(new BitArray(new bool[] { true, false, true }));
            var population = new List<Chromosome<BitArray>> { chromosome };

            // Act
            mutation.Mutate(population);

            // Assert
            chromosome.GeneSequence[0].Should().BeTrue();
            chromosome.GeneSequence[1].Should().BeFalse();
            chromosome.GeneSequence[2].Should().BeTrue();
        }
    }
}
