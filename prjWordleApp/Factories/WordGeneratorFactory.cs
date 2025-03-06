using prjWordleApp.Services;
    
    namespace prjWordleApp.Factories
    {
        /// <summary>
        /// Factory class for creating instances of <see cref="IWordGenerator"/>.
        /// </summary>
        public class WordGeneratorFactory
        {
            /// <summary>
            /// Creates an instance of <see cref="IWordGenerator"/>.
            /// </summary>
            /// <returns>An instance of <see cref="IWordGenerator"/>.</returns>
            public static IWordGenerator CreateGenerator()
            {
                return new DefaultWordGenerator();
            }
        }
    }