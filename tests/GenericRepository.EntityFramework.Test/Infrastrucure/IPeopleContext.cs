using System;
using System.Data.Entity;

namespace GenericRepository.EntityFramework.Test.Infrastrucure
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="GenericRepository.EntityFramework.IEntitiesContext" />
    public interface IPeopleContext : IDisposable, IEntitiesContext {

        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        IDbSet<Person> People { get; set; }
        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        IDbSet<Book> Books { get; set; }
    }
}