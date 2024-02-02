using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedObjectActions : MonoBehaviour
{
	[SerializeField]
	ARTrackedImageManager m_TrackedImageManager;

	void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

	void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

	void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
	{
		foreach (var newImage in eventArgs.added)
		{
			Debug.Log("Tracked image found!");
		}

		foreach (var updatedImage in eventArgs.updated)
		{
			// Handle updated event

		}

		foreach (var removedImage in eventArgs.removed)
		{
			// Handle removed event

		}
	}

private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			ListAllImages();
		}
	}

	void ListAllImages()
	{
		Debug.Log(
			$"There are {m_TrackedImageManager.trackables.count} images being tracked.");

		foreach (var trackedImage in m_TrackedImageManager.trackables)
		{
			Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
					  $"{trackedImage.transform.position}");
		}
	}
}
