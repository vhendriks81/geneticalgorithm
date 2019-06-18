namespace GeneticAlgorithm.Population
{
    using System.Collections;
    using GeneticAlgorithm.Chromosome;

    public class BasicBinaryPopulationFactory : IPopulationFactory<BitArray>
    {
        private readonly int _geneCount;
        private readonly bool _defaultValue;

        public BasicBinaryPopulationFactory(int geneCount, bool defaultValue)
        {
            _geneCount = geneCount;
            _defaultValue = defaultValue;
        }

        public Chromosome<BitArray> Create()
        {
            var geneSequence = new BitArray(_geneCount, _defaultValue);

            var newChromosome = new Chromosome<BitArray>(geneSequence);

            return newChromosome;
        }
    }
}
