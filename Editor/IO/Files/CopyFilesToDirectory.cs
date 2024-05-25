using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace UniTools.Build
{
    [CreateAssetMenu(
        fileName = nameof(CopyFilesToDirectory),
        menuName = MenuPaths.IO + nameof(CopyFilesToDirectory)
    )]
    public sealed class CopyFilesToDirectory : BuildStep
    {
        [SerializeField] private PathProperty[] m_sourceFiles = default;
        [SerializeField] private PathProperty m_destinationDirectory = default;

        public override async Task Execute()
        {
            var destinationFolder = m_destinationDirectory.ToString();
            if (!Directory.Exists(destinationFolder))
            {
                throw new DirectoryNotFoundException($"{destinationFolder} does not exist!");
            }
            
            foreach (var filePath in m_sourceFiles)
            {
                var fileName = Path.GetFileName(filePath.ToString());
                var dest = Path.Combine(destinationFolder, fileName);
                FileUtil.DeleteFileOrDirectory(dest);
                FileUtil.CopyFileOrDirectory(filePath.ToString(), dest);
            }
            
            await Task.CompletedTask;
        }
    }
}