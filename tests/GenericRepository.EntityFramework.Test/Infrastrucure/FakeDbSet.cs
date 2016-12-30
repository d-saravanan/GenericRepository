using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository.EntityFramework.Test.Infrastrucure
{
    public class FakeDbSet<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        /// <summary>
        /// The collection
        /// </summary>
        ObservableCollection<TEntity> _collection;
        /// <summary>
        /// The query
        /// </summary>
        IQueryable _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbSet{TEntity}"/> class.
        /// </summary>
        public FakeDbSet()
        {
            _collection = new ObservableCollection<TEntity>();
            _query = _collection.AsQueryable();
        }

        /// <summary>
        /// Adds the given entity to the context underlying the set in the Added state such that it will
        /// be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        /// <remarks>
        /// Note that entities that are already in the context in some other state will have their state set
        /// to Added.  Add is a no-op if the entity is already in the context in the Added state.
        /// </remarks>
        public TEntity Add(TEntity entity)
        {
            _collection.Add(entity);
            return entity;
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }

        /// <summary>
        /// Attaches the given entity to the context underlying the set.  That is, the entity is placed
        /// into the context in the Unchanged state, just as if it had been read from the database.
        /// </summary>
        /// <param name="entity">The entity to attach.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        /// <remarks>
        /// Attach is used to repopulate a context with an entity that is known to already exist in the database.
        /// SaveChanges will therefore not attempt to insert an attached entity into the database because
        /// it is assumed to already be there.
        /// Note that entities that are already in the context in some other state will have their state set
        /// to Unchanged.  Attach is a no-op if the entity is already in the context in the Unchanged state.
        /// </remarks>
        public TEntity Attach(TEntity entity)
        {

            _collection.Add(entity);
            return entity;
        }

        /// <summary>
        /// Creates a new instance of an entity for the type of this set or for a type derived
        /// from the type of this set.
        /// Note that this instance is NOT added or attached to the set.
        /// The instance returned will be a proxy if the underlying context is configured to create
        /// proxies and the entity type meets the requirements for creating a proxy.
        /// </summary>
        /// <typeparam name="TDerivedEntity">The type of entity to create.</typeparam>
        /// <returns>
        /// The entity instance, which may be a proxy.
        /// </returns>
        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {

            return Activator.CreateInstance<TDerivedEntity>();
        }

        /// <summary>
        /// Creates a new instance of an entity for the type of this set.
        /// Note that this instance is NOT added or attached to the set.
        /// The instance returned will be a proxy if the underlying context is configured to create
        /// proxies and the entity type meets the requirements for creating a proxy.
        /// </summary>
        /// <returns>
        /// The entity instance, which may be a proxy.
        /// </returns>
        public TEntity Create()
        {

            return Activator.CreateInstance<TEntity>();
        }

        /// <summary>
        /// Finds an entity with the given primary key values.
        /// If an entity with the given primary key values exists in the context, then it is
        /// returned immediately without making a request to the store.  Otherwise, a request
        /// is made to the store for an entity with the given primary key values and this entity,
        /// if found, is attached to the context and returned.  If no entity is found in the
        /// context or the store, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>
        /// The entity found, or null.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>
        /// The ordering of composite key values is as defined in the EDM, which is in turn as defined in
        /// the designer, by the Code First fluent API, or by the DataMember attribute.
        /// </remarks>
        public TEntity Find(params object[] keyValues)
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> that represents a local view of all Added, Unchanged,
        /// and Modified entities in this set.  This local view will stay in sync as entities are added or
        /// removed from the context.  Likewise, entities added to or removed from the local view will automatically
        /// be added to or removed from the context.
        /// </summary>
        /// <value>
        /// The local view.
        /// </value>
        /// <remarks>
        /// This property can be used for data binding by populating the set with data, for example by using the Load
        /// extension method, and then binding to the local data through this property.  For WPF bind to this property
        /// directly.  For Windows Forms bind to the result of calling ToBindingList on this property
        /// </remarks>
        public ObservableCollection<TEntity> Local
        {

            get { return _collection; }
        }

        /// <summary>
        /// Marks the given entity as Deleted such that it will be deleted from the database when SaveChanges
        /// is called.  Note that the entity must exist in the context in some other state before this method
        /// is called.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        /// <remarks>
        /// Note that if the entity exists in the context in the Added state, then this method
        /// will cause it to be detached from the context.  This is because an Added entity is assumed not to
        /// exist in the database such that trying to delete it does not make sense.
        /// </remarks>
        public TEntity Remove(TEntity entity)
        {

            _collection.Remove(entity);
            return entity;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TEntity> GetEnumerator()
        {

            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {

            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.
        /// </summary>
        public Type ElementType
        {
            get { return _query.ElementType; }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.
        /// </summary>
        public Expression Expression
        {
            get { return _query.Expression; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        public IQueryProvider Provider
        {
            get { return _query.Provider; }
        }
    }
}