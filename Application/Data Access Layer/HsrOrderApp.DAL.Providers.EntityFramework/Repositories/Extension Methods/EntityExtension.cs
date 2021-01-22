namespace System.Data.Objects.DataClasses
{
    /// <summary>
    /// Entity extensions
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Ensures that entity referenced by <paramref name="entityReference"/> is loaded.
        /// </summary>
        public static EntityReference<TEntity> GetEnsureLoadedReference<TEntity>(this EntityReference<TEntity> entityReference) where TEntity : EntityObject
        {
            if (!entityReference.IsLoaded)
            {
                entityReference.Load();
            }
            return entityReference;
        }

        /// <summary>
        /// Ensures that entity collection is loaded.
        /// </summary>
        public static EntityCollection<TEntity> GetEnsureLoadedReference<TEntity>(this EntityCollection<TEntity> entityCollection) where TEntity : EntityObject
        {
            if (!entityCollection.IsLoaded)
            {
                entityCollection.Load();
            }
            return entityCollection;
        }
    }
}