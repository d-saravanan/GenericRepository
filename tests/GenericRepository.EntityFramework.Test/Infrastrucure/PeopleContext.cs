using System.Data.Entity;

namespace GenericRepository.EntityFramework.Test.Infrastrucure
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GenericRepository.EntityFramework.EntitiesContext" />
    /// <seealso cref="GenericRepository.EntityFramework.Test.Infrastrucure.IPeopleContext" />
    public class PeopleContext : EntitiesContext, IPeopleContext {

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleContext"/> class.
        /// </summary>
        public PeopleContext() { }

        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        public IDbSet<Person> People { get; set; }
        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public IDbSet<Book> Books { get; set; }

    }
}