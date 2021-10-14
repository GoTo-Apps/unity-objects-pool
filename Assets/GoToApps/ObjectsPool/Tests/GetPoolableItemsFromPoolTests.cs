using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    /// <summary>
    /// Tests verifying the receipt of PoolableItems from the pool.
    /// </summary>
    public class GetPoolableItemsFromPoolTests
    {
        /// <summary>
        /// Create base some poolable item instance.
        /// </summary>
        /// <returns>Instantiated poolable item.</returns>
        private SomePoolableItem CreateSomePoolableItemExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>();
        }
        
        /// <summary>
        /// Create default Pool Manager instance.
        /// </summary>
        /// <returns>Instantiated Pool Manager.</returns>
        private PoolManager CreatePoolManagerInstance()
        {
            return new GameObject("Objects Pool").AddComponent<PoolManager>();
        }

        /// <summary>
        /// The test checks the possibility of obtaining 1 instance of the PoolableItem class from a filled game pool.
        /// </summary>
        [Test]
        public void GetSingleGameObjectFromPoolTest()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            PoolableItem poolableItem = CreateSomePoolableItemExtendedFromPoolableItem();
            pool.AddItemToPool(poolableItem);
            
            // Test Actions
            PoolableItem poolableItemFromPool = pool.GetItemFromPool();
            
            // Assert
            int poolableItemInstanceId = poolableItem.GetInstanceID();
            int poolableItemFromPoolInstanceId = poolableItemFromPool.GetInstanceID();
            Assert.True(poolableItemInstanceId == poolableItemFromPoolInstanceId);
            
            // Clear
            Object.Destroy(pool);
            Object.Destroy(poolableItemFromPool);
            Object.Destroy(poolableItem);
        }
        
        /// <summary>
        /// The test checks whether it is possible to get multiple instances of the PoolableItem class from a full pool.
        /// </summary>
        [Test]
        public void GetMultipleGameObjectsFromPoolTest()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            int itemsCount = Random.Range(10, 100);
            for (int i = 0; i < itemsCount; i++)
            {
                PoolableItem poolableItem = CreateSomePoolableItemExtendedFromPoolableItem();
                pool.AddItemToPool(poolableItem);
            }
            
            // Test Action
            List<PoolableItem> poolableItemsFromPool = pool.GetItemsFromPool(itemsCount);
            
            // Assert
            Assert.True(itemsCount == poolableItemsFromPool.Count);

            // Clear
            Object.Destroy(pool);
            foreach (PoolableItem poolableItem in poolableItemsFromPool)
            {
                Object.Destroy(poolableItem);
            }
        }
    }
}