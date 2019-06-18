using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithm.Exceptions
{
    using System.Runtime.Serialization;

    [Serializable]
    public class FitnessScoreAlreadySetException : Exception
    {
        public FitnessScoreAlreadySetException()
        {
        }

        public FitnessScoreAlreadySetException(string message)
            : base(message)
        {
        }

        public FitnessScoreAlreadySetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FitnessScoreAlreadySetException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
