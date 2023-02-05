using UnityEngine;

namespace Game.UI
{
    public class DeleteAllData : MonoBehaviour
    {
        public void DeleteAll()
        {
            GameManager.Instance.DeleteAllData();
        }
    }
}
