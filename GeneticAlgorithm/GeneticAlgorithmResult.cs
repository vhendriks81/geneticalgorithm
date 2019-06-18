namespace GeneticAlgorithm
{
    using System;
    using System.Collections.Generic;
    using GeneticAlgorithm.Chromosome;

    public class GeneticAlgorithmResult<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        public GeneticAlgorithmResult(IReadOnlyCollection<Chromosome<TGeneSequence>> selection)
        {
            Selection = selection;
        }

        public IReadOnlyCollection<Chromosome<TGeneSequence>> Selection { get; }
    }
}
