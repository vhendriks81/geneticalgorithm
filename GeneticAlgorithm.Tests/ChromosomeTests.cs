using Xunit;
using GeneticAlgorithm.Chromosome;
using GeneticAlgorithm.Exceptions;
using System.Collections;
using FluentAssertions;

namespace GeneticAlgorithm.Tests
{
    public class ChromosomeTests
    {
        [Fact]
        public void FitnessScore_SetOnce_SetsValue()
        {
            // Arrange
            var geneSequence = new BitArray(10);
            var chromosome = new Chromosome<BitArray>(geneSequence);
            var expectedFitnessScore = 100.0;

            // Act
            chromosome.FitnessScore = expectedFitnessScore;

            // Assert
            chromosome.FitnessScore.Should().Be(expectedFitnessScore);
        }

        [Fact]
        public void FitnessScore_SetTwice_ThrowsException()
        {
            // Arrange
            var geneSequence = new BitArray(10);
            var chromosome = new Chromosome<BitArray>(geneSequence);
            chromosome.FitnessScore = 100.0;

            // Act & Assert
            Assert.Throws<FitnessScoreAlreadySetException>(() => chromosome.FitnessScore = 200.0);
        }

        [Fact]
        public void Clone_CreatesDeepCopy()
        {
            // Arrange
            var geneSequence = new BitArray(10);
            var chromosome = new Chromosome<BitArray>(geneSequence);

            // Act
            var clone = (Chromosome<BitArray>)chromosome.Clone();

            // Assert
            clone.Should().NotBeSameAs(chromosome);
            clone.GeneSequence.Should().NotBeSameAs(chromosome.GeneSequence);
            for (int i = 0; i < geneSequence.Count; i++)
            {
                clone.GeneSequence[i].Should().Be(chromosome.GeneSequence[i]);
            }
        }
    }
}
