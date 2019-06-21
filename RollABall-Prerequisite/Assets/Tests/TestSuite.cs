using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
  #region Module
  public class TestSuite
  {
    private GameManager gameManager;

    [SetUp] public void Setup()
    {
      GameObject prefab = Resources.Load<GameObject>("Prefabs/Game"); // Load resource first
      GameObject clone = Object.Instantiate(prefab);
      gameManager = clone.GetComponent<GameManager>();
    }
    #region Unit Tests
    [UnityTest] public IEnumerator GameManagerWasLoaded()
    {
      yield return new WaitForEndOfFrame();

      // Check if it exists after frame
      Assert.IsTrue(gameManager != null);
    }
    [UnityTest] public IEnumerator PlayerExistsInGame()
    {
      yield return new WaitForEndOfFrame();

      Player player = gameManager.GetComponentInChildren<Player>();
      Assert.IsTrue(player != null);
    }
    [UnityTest] public IEnumerator ItemCollidesWithPlayer()
    {
      // Get the player
      Player player = Object.FindObjectOfType<Player>();
      // Get an item
      Item item = Object.FindObjectOfType<Item>();

      // Position both in the same location
      player.transform.position = new Vector3(0, 2, 0);
      item.transform.position = new Vector3(0, 2, 0);

      yield return new WaitForSeconds(0.1f);

      // Asset that item should be destroyed (non-existent)
      Assert.IsTrue(item == null);
    }
    [UnityTest] public IEnumerator PlayerShootsItem()
    {
      Player player = Object.FindObjectOfType<Player>();
      Item item = Object.FindObjectOfType<Item>();

      player.transform.position = new Vector3(0, 3, -3);
      item.transform.position = new Vector3(0, 3, 0);

      yield return null;

      Assert.IsTrue(player.Shoot());
      // Assert.IsTrue(item == null);
    }
    #endregion
    [TearDown] public void TearDown()
    {
      Object.Destroy(gameManager.gameObject); // Remove resources
    }
  }
  #endregion
}