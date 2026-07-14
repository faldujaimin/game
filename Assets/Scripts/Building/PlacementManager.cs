using UnityEngine;

namespace Game.Building
{
    /// <summary>
    /// Handles Ghost Preview, Input, and Snapping logic.
    /// </summary>
    public class PlacementManager : MonoBehaviour
    {
        [Header("Settings")]
        public float placementRange = 10f;
        public float rotationStep = 90f;
        public LayerMask snapLayer;
        public LayerMask terrainLayer;

        private BuildingData _currentBlueprint;
        private GameObject _ghostInstance;
        private bool _isValidPosition;
        private bool _isSnapped;
        private SnapPoint _currentSnapPoint;
        private float _currentRotation;

        public void SetBlueprint(BuildingData data)
        {
            _currentBlueprint = data;
            _currentRotation = 0f;
            
            if (_ghostInstance != null) Destroy(_ghostInstance);
            
            if (data != null && data.GhostPrefab != null)
            {
                _ghostInstance = Instantiate(data.GhostPrefab);
            }
            
            BuildingEvents.OnBlueprintSelected?.Invoke(data);
        }

        private void Update()
        {
            if (_currentBlueprint == null || _ghostInstance == null) return;

            HandleRotation();
            UpdateGhostPosition();
            HandlePlacement();
            
            // Undo/Cancel placement
            if (Input.GetMouseButtonDown(1)) // Right click
            {
                SetBlueprint(null);
            }
        }

        private void HandleRotation()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _currentRotation += rotationStep;
                _currentRotation %= 360f;
            }
        }

        private void UpdateGhostPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _isSnapped = false;
            _currentSnapPoint = null;

            // 1. Try Snapping
            if (Physics.Raycast(ray, out RaycastHit snapHit, placementRange, snapLayer))
            {
                SnapPoint point = snapHit.collider.GetComponent<SnapPoint>();
                if (point != null && !point.IsOccupied && point.AllowedCategories.Contains(_currentBlueprint.Category))
                {
                    _ghostInstance.transform.position = point.transform.position;
                    _ghostInstance.transform.rotation = point.transform.rotation;
                    _isSnapped = true;
                    _currentSnapPoint = point;
                }
            }

            // 2. Free Placement (Terrain Alignment)
            if (!_isSnapped && Physics.Raycast(ray, out RaycastHit terrainHit, placementRange, terrainLayer))
            {
                _ghostInstance.transform.position = terrainHit.point;
                _ghostInstance.transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
            }

            // 3. Validate
            _isValidPosition = PlacementValidator.CanPlace(_currentBlueprint, _ghostInstance.transform.position, _ghostInstance.transform.rotation, _isSnapped);

            // Change Ghost Color based on validity
            Renderer[] renderers = _ghostInstance.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                r.material.color = _isValidPosition ? Color.green : Color.red;
            }
        }

        private void HandlePlacement()
        {
            if (Input.GetMouseButtonDown(0) && _isValidPosition)
            {
                // Request final spawn from BuildingManager
                BuildingManager.Instance.ConstructBuilding(_currentBlueprint, _ghostInstance.transform.position, _ghostInstance.transform.rotation, _currentSnapPoint);
                
                // Allow continuous building
                _currentSnapPoint = null;
            }
        }
    }
}
