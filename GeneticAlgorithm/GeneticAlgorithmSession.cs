using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithm
{
    using GeneticAlgorithm.Chromosome;

    public class GeneticAlgorithmSession<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        public int GenerationCount { get; set; }

        public ICollection<Chromosome<TGeneSequence>> CurrentPopulation { get; set; }

        public override string ToString()
        {
            return $"Generation Count: {GenerationCount}";
        }
    }
}
