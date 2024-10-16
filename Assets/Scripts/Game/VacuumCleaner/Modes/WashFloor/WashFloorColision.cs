using UnityEngine;

namespace VacuumCleaner.Modes
{
    public class WashFloorColision : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private WashFloorModel model;

        private Ray _ray;

        private Vector3? _collision;
        private Quaternion _left;
        private Quaternion _right;

        private Vector3 _leftBoundary;
        private Vector3 _rightBoundary;

        private void Start()
        {
            _left = Quaternion.AngleAxis(-model.MaxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(model.MaxAngle, Vector3.up);
            _leftBoundary = _left * target.forward;
            _rightBoundary = _right * target.forward;
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsWasheable(other))
            {
                if (CanWash(other))
                {
                    other.GetComponentInParent<IWasheable>().IsBeingWashed(model.WashSpeed, model.MinScale);
                }
            }
        }

        private static bool IsWasheable(Collider other)
        {
            IWasheable washeable = other.GetComponentInParent<IWasheable>();

            return washeable != null;
        }

        private bool CanWash(Collider other)
        {
            var angleToObject = Vector3.Angle(target.forward, other.transform.position - target.position);

            if (!(angleToObject <= model.MaxAngle)) return false;

            _ray = new Ray(target.position, other.transform.position - target.position);

            if (Physics.Raycast(_ray, out var hit, model.RenderDistance, model.WallLayer))
            {
                return false;
            }

            return true;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            _left = Quaternion.AngleAxis(-model.MaxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(model.MaxAngle, Vector3.up);
            _leftBoundary = _left * target.forward;
            _rightBoundary = _right * target.forward;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(target.position, target.position + target.forward * model.RenderDistance);

            Gizmos.DrawLine(target.position, target.position + _leftBoundary * model.RenderDistance);

            Gizmos.DrawLine(target.position, target.position + _rightBoundary * model.RenderDistance);
            Gizmos.DrawRay(_ray);
            if (_collision != null)
            {
                Gizmos.DrawSphere(_collision.Value, 0.5f);
            }
        }
#endif
    }
}
