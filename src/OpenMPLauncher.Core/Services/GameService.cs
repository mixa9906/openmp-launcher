using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenMPLauncher.Core.Services
{
    /// <summary>
    /// Service for game installation and updates
    /// </summary>
    public interface IGameService
    {
        Task<bool> InstallBuildAsync(string buildType, IProgress<DownloadProgress> progress);
        Task<bool> VerifyClientFilesAsync();
        Task<bool> LaunchGameAsync();
        Task<bool> UpdateBuildAsync(string buildType, IProgress<DownloadProgress> progress);
        bool IsGameInstalled();
    }

    public class DownloadProgress
    {
        public long BytesReceived { get; set; }
        public long TotalBytes { get; set; }
        public int ProgressPercentage => (int)((BytesReceived * 100) / TotalBytes);
    }

    public class GameService : IGameService
    {
        private readonly string _gameDirectory;
        private readonly string _cacheDirectory;
        private readonly ILogger<GameService> _logger;

        public GameService(string gameDirectory, string cacheDirectory, ILogger<GameService> logger)
        {
            _gameDirectory = gameDirectory;
            _cacheDirectory = cacheDirectory;
            _logger = logger;
        }

        public bool IsGameInstalled()
        {
            try
            {
                var exePath = Path.Combine(_gameDirectory, "gta_sa.exe");
                return File.Exists(exePath);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InstallBuildAsync(string buildType, IProgress<DownloadProgress> progress)
        {
            try
            {
                _logger.LogInformation($"Installing {buildType} build...");
                
                Directory.CreateDirectory(_gameDirectory);
                Directory.CreateDirectory(_cacheDirectory);

                // Simulate download with progress
                for (int i = 0; i <= 100; i++)
                {
                    progress?.Report(new DownloadProgress { BytesReceived = i * 1024 * 1024, TotalBytes = 100 * 1024 * 1024 });
                    await Task.Delay(10);
                }

                _logger.LogInformation($"{buildType} build installed successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Build installation failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> VerifyClientFilesAsync()
        {
            try
            {
                _logger.LogInformation("Verifying client files...");
                
                // Verify GTA SA executable
                var exePath = Path.Combine(_gameDirectory, "gta_sa.exe");
                if (!File.Exists(exePath))
                {
                    _logger.LogWarning("GTA SA executable not found");
                    return false;
                }

                _logger.LogInformation("Client files verified successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"File verification failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LaunchGameAsync()
        {
            try
            {
                var exePath = Path.Combine(_gameDirectory, "gta_sa.exe");
                
                if (!File.Exists(exePath))
                {
                    _logger.LogWarning("Game executable not found");
                    return false;
                }

                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = exePath,
                    WorkingDirectory = _gameDirectory,
                    UseShellExecute = false
                };

                using (var process = System.Diagnostics.Process.Start(processInfo))
                {
                    _logger.LogInformation("Game launched successfully");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Game launch failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateBuildAsync(string buildType, IProgress<DownloadProgress> progress)
        {
            return await InstallBuildAsync(buildType, progress);
        }
    }
}
