using System.IO;
using System.Threading.Tasks;
using UniTools.IO;
using UnityEngine;

namespace UniTools.Build
{
    [CreateAssetMenu(
        fileName = nameof(EnsureDirectoryExists),
        menuName = nameof(UniTools) + "/Build/IO/" + nameof(EnsureDirectoryExists)
    )]
    public sealed class EnsureDirectoryExists : ScriptableCustomBuildStep
    {
        [SerializeField] private PathProperty m_path = default;

        public override async Task Execute()
        {
            Directory.CreateDirectory(m_path.ToString());

            await Task.CompletedTask;
        }
    }
}