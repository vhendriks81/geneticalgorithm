namespace GeneticAlgorithm.Selection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GeneticAlgorithm.Chromosome;

    public class BestFitnessSelection<TGeneSequence> : ISelection<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        private readonly int _selectionCount;

        public BestFitnessSelection(int selectionCount)
        {
            _selectionCount = selectionCount;
        }

        public ICollection<Chromosome<TGeneSequence>> Select(ICollection<Chromosome<TGeneSequence>> population)
        {
            var newSelection = population.OrderByDescending(x => x.FitnessScore).Take(_selectionCount);

            return new List<Chromosome<TGeneSequence>>(newSelection);
        }
    }
}
