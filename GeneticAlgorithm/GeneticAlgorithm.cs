namespace GeneticAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using GeneticAlgorithm.Chromosome;
    using GeneticAlgorithm.Crossover;
    using GeneticAlgorithm.Evaluation;
    using GeneticAlgorithm.Mutation;
    using GeneticAlgorithm.Population;
    using GeneticAlgorithm.Selection;
    using GeneticAlgorithm.Termination;

    public class GeneticAlgorithm<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        private readonly IPopulationFactory<TGeneSequence> _populationFactory;
        private readonly ITermination<TGeneSequence> _termination;
        private readonly IFitnessEvaluator<TGeneSequence> _fitnessEvaluator;
        private readonly ISelection<TGeneSequence> _preservationSelection;
        private readonly ISelection<TGeneSequence> _parentSelection;
        private readonly ICrossover<TGeneSequence> _crossover;
        private readonly IMutation<TGeneSequence> _mutation;
        private readonly int _initialPopulation;

        private GeneticAlgorithmSession<TGeneSequence> _currentSession;

        public GeneticAlgorithm(
            IPopulationFactory<TGeneSequence> populationFactory,
            ITermination<TGeneSequence> termination,
            IFitnessEvaluator<TGeneSequence> fitnessEvaluator,
            ISelection<TGeneSequence> preservationSelection,
            ISelection<TGeneSequence> parentSelection,
            ICrossover<TGeneSequence> crossover,
            IMutation<TGeneSequence> mutation,
            int initialPopulation)
        {
            _populationFactory = populationFactory;
            _termination = termination;
            _fitnessEvaluator = fitnessEvaluator;
            _preservationSelection = preservationSelection;
            _parentSelection = parentSelection;
            _crossover = crossover;
            _mutation = mutation;
            _initialPopulation = initialPopulation;
        }

        public GeneticAlgorithmResult<TGeneSequence> Start()
        {
            var keepRunning = true;
            var session = new GeneticAlgorithmSession<TGeneSequence>();

            // Create the initial starting population
            var startingPopulation = new List<Chromosome<TGeneSequence>>();
            for (var i = 0; i < _initialPopulation; i++)
            {
                startingPopulation.Add(_populationFactory.Create());
            }

            session.CurrentPopulation = startingPopulation;
            _currentSession = session;

            // Calculate the initial fitness score of each chromosome
            CalculateFitness();

            var bestSelection = _currentSession.CurrentPopulation.OrderByDescending(x => x.FitnessScore).ToList();
            Console.WriteLine($"Running session: {session}. Current population: {_currentSession.CurrentPopulation.Count} Best fitness score: {bestSelection.First().FitnessScore}");

            while (keepRunning)
            {
                // Run one evolve generation
                Evolve();

                bestSelection = _currentSession.CurrentPopulation.OrderByDescending(x => x.FitnessScore).ToList();
                Console.WriteLine($"Running session: {session}. Current population: {_currentSession.CurrentPopulation.Count} Best fitness score: {bestSelection.First().FitnessScore}");

                // Check if we should keep on running yes/ no
                keepRunning = !_termination.ShouldTerminate(session);
            }
           
            var newResult = new GeneticAlgorithmResult<TGeneSequence>(new ReadOnlyCollection<Chromosome<TGeneSequence>>(bestSelection));

            return newResult;
        }

        private void Evolve()
        {
            _currentSession.GenerationCount++;

            // Select parts of the population to preserve and not mutate
            var preservedPopulation = _preservationSelection.Select(_currentSession.CurrentPopulation);

            // Select parents which will create children
            var parents = _parentSelection.Select(_currentSession.CurrentPopulation);

            // Crossover (ie, create children from the selected parents)
            var children = _crossover.Crossover(parents);

            // Mutate the children (This sounds horribly, but this isn't real. It's just a genetic algorithm with bits and bytes...
            _mutation.Mutate(children);

            // Create a new population with the preserved population and all the new children
            var newPopulation = new List<Chromosome<TGeneSequence>>(preservedPopulation);
            newPopulation.AddRange(children);

            _currentSession.CurrentPopulation = newPopulation;

            // Calculate the fitness score for the current population
            CalculateFitness();
        }

        private void CalculateFitness()
        {
            foreach(var chromosome in _currentSession.CurrentPopulation.Where(x => x.FitnessScore == null))
            {
                chromosome.FitnessScore = _fitnessEvaluator.Evaluate(chromosome);
            }
        }
    }
}
