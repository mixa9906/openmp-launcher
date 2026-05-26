# Build Configuration Guide

## Overview

The OpenMP Launcher supports three different build configurations optimized for different PC specifications.

## Build Profiles

### 1. Light Build (Легкая сборка)

**Target Size:** ~2 GB

**System Requirements:**
- RAM: 2 GB minimum
- CPU: Any dual-core processor
- GPU: Integrated graphics
- Storage: SSD recommended

**Specifications:**
```json
{
  "name": "light",
  "displayName": "Легкая сборка",
  "targetSize": 2048,
  "textureQuality": "low",
  "graphicsQuality": "low",
  "drawDistance": 100,
  "fps_target": 30,
  "resolution": "800x600 - 1024x768"
}
```

**Included Components:**
- Optimized textures (256x256 resolution)
- Low-poly models
- Minimal effects
- Streaming assets
- Compressed audio

**Configuration File:**
```xml
<!-- light_config.xml -->
<config>
  <graphics>
    <quality>low</quality>
    <textureResolution>256</textureResolution>
    <modelLOD>high</modelLOD>
    <shadowQuality>off</shadowQuality>
    <waterQuality>simple</waterQuality>
  </graphics>
  <performance>
    <maxFPS>30</maxFPS>
    <drawDistance>100</drawDistance>
    <antialiasing>off</antialiasing>
  </performance>
</config>
```

### 2. Medium Build (Средняя сборка)

**Target Size:** ~4 GB

**System Requirements:**
- RAM: 4 GB minimum
- CPU: Quad-core processor
- GPU: GTX 1050 / RX 560 equivalent
- Storage: SSD recommended

**Specifications:**
```json
{
  "name": "medium",
  "displayName": "Средняя сборка",
  "targetSize": 4096,
  "textureQuality": "medium",
  "graphicsQuality": "medium",
  "drawDistance": 200,
  "fps_target": 60,
  "resolution": "1280x720 - 1920x1080"
}
```

**Included Components:**
- Balanced textures (512x512 resolution)
- Medium-poly models
- Standard effects
- Partial streaming
- Compressed audio

**Configuration File:**
```xml
<!-- medium_config.xml -->
<config>
  <graphics>
    <quality>medium</quality>
    <textureResolution>512</textureResolution>
    <modelLOD>medium</modelLOD>
    <shadowQuality>medium</shadowQuality>
    <waterQuality>realistic</waterQuality>
    <particles>medium</particles>
  </graphics>
  <performance>
    <maxFPS>60</maxFPS>
    <drawDistance>200</drawDistance>
    <antialiasing>2x</antialiasing>
  </performance>
</config>
```

### 3. Heavy Build (Сильная сборка)

**Target Size:** ~8 GB

**System Requirements:**
- RAM: 8 GB minimum
- CPU: Octa-core processor
- GPU: RTX 2060 / RTX 3060 equivalent or better
- Storage: SSD required for fast loading

**Specifications:**
```json
{
  "name": "heavy",
  "displayName": "Сильная сборка",
  "targetSize": 8192,
  "textureQuality": "high",
  "graphicsQuality": "high",
  "drawDistance": 400,
  "fps_target": 144,
  "resolution": "1920x1080 - 2560x1440"
}
```

**Included Components:**
- High-resolution textures (1024x1024 - 2048x2048)
- High-poly models
- Advanced effects (ray tracing ready)
- Full assets loaded
- Uncompressed audio
- 4K assets support

**Configuration File:**
```xml
<!-- heavy_config.xml -->
<config>
  <graphics>
    <quality>ultra</quality>
    <textureResolution>2048</textureResolution>
    <modelLOD>low</modelLOD>
    <shadowQuality>ultra</shadowQuality>
    <shadowResolution>4096</shadowResolution>
    <waterQuality>complex</waterQuality>
    <particles>high</particles>
    <rayTracing>true</rayTracing>
  </graphics>
  <performance>
    <maxFPS>144</maxFPS>
    <drawDistance>400</drawDistance>
    <antialiasing>4x</antialiasing>
    <reflections>true</reflections>
  </performance>
</config>
```

## Installation Process

### 1. Build Detection

```csharp
public class BuildDetector
{
    public static BuildType DetectOptimalBuild()
    {
        var ramGB = GetTotalRAM() / 1024 / 1024 / 1024;
        var vramMB = GetGPUMemory();
        var cpuCores = Environment.ProcessorCount;
        
        if (ramGB >= 8 && vramMB >= 4096 && cpuCores >= 8)
            return BuildType.Heavy;
        
        if (ramGB >= 4 && vramMB >= 2048 && cpuCores >= 4)
            return BuildType.Medium;
        
        return BuildType.Light;
    }
}
```

### 2. Download Manager

```csharp
public class BuildDownloader
{
    public async Task<bool> DownloadBuildAsync(
        BuildType buildType, 
        IProgress<DownloadProgress> progress)
    {
        var buildUrl = GetBuildUrl(buildType);
        var buildPath = GetBuildPath(buildType);
        
        using var client = new HttpClient();
        using var response = await client.GetAsync(buildUrl, HttpCompletionOption.ResponseHeadersRead);
        
        var totalBytes = response.Content.Headers.ContentLength ?? 0L;
        var canResumeDownload = response.Headers.AcceptRanges.Contains("bytes");
        
        using var contentStream = await response.Content.ReadAsStreamAsync();
        using var fileStream = File.Create(buildPath, 81920, FileOptions.SequentialScan);
        
        var totalRead = 0L;
        var buffer = new byte[81920];
        int read;
        
        while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            await fileStream.WriteAsync(buffer, 0, read);
            totalRead += read;
            
            progress?.Report(new DownloadProgress 
            { 
                BytesReceived = totalRead, 
                TotalBytes = totalBytes 
            });
        }
        
        return true;
    }
}
```

### 3. File Verification

```csharp
public class BuildVerifier
{
    public async Task<bool> VerifyBuildAsync(BuildType buildType)
    {
        var checksumFile = Path.Combine(GetBuildPath(buildType), "checksums.json");
        var checksums = JsonSerializer.Deserialize<Dictionary<string, string>>(
            File.ReadAllText(checksumFile));
        
        foreach (var (file, expectedHash) in checksums)
        {
            var actualHash = await ComputeSHA256(file);
            if (actualHash != expectedHash)
            {
                Logger.LogError($"Checksum mismatch: {file}");
                return false;
            }
        }
        
        return true;
    }
    
    private async Task<string> ComputeSHA256(string filePath)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        using var fileStream = File.OpenRead(filePath);
        var hash = await sha256.ComputeHashAsync(fileStream);
        return Convert.ToHexString(hash);
    }
}
```

## Build Switching

### Switch Build at Runtime

```csharp
public class BuildManager
{
    public async Task<bool> SwitchBuildAsync(BuildType newBuild)
    {
        // Backup current build
        var currentBuild = GetCurrentBuild();
        var backupPath = Path.Combine(GetBuildPath(currentBuild), "backup");
        Directory.CreateDirectory(backupPath);
        
        // Download and install new build
        var success = await DownloadBuildAsync(newBuild);
        
        if (!success)
        {
            // Restore backup
            RestoreFromBackup(backupPath, currentBuild);
            return false;
        }
        
        // Update configuration
        SaveCurrentBuild(newBuild);
        
        return true;
    }
}
```

## Size Management

### Cleanup Unused Builds

```csharp
public class StorageManager
{
    public void CleanupUnusedBuilds()
    {
        var usedBuild = GetCurrentBuild();
        var buildsDir = GetBuildsDirectory();
        
        foreach (var buildDir in Directory.GetDirectories(buildsDir))
        {
            var buildType = Path.GetFileName(buildDir);
            
            if (buildType != usedBuild.ToString())
            {
                Directory.Delete(buildDir, true);
                Logger.LogInfo($"Removed unused build: {buildType}");
            }
        }
    }
}
```

### Compression

```csharp
public class BuildCompressor
{
    public void CompressBuild(BuildType buildType)
    {
        var buildPath = GetBuildPath(buildType);
        var compressedPath = buildPath + ".compressed";
        
        using (var zipFile = ZipFile.Open(compressedPath, ZipArchiveMode.Create))
        {
            foreach (var file in Directory.GetFiles(buildPath, "*", SearchOption.AllDirectories))
            {
                var entryName = Path.GetRelativePath(buildPath, file);
                zipFile.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
            }
        }
    }
}
```

## Update Process

```csharp
public async Task UpdateBuildAsync(BuildType buildType)
{
    var manifest = await FetchManifestAsync(buildType);
    var localFiles = GetLocalFiles(buildType);
    var filesToUpdate = new List<string>();
    
    foreach (var (file, remoteHash) in manifest)
    {
        var localHash = ComputeHash(file);
        
        if (localHash != remoteHash)
            filesToUpdate.Add(file);
    }
    
    // Download only changed files
    foreach (var file in filesToUpdate)
    {
        await DownloadFileAsync(file, buildType);
    }
    
    // Verify final build
    if (!await VerifyBuildAsync(buildType))
        throw new Exception("Build verification failed after update");
}
```

## Configuration Storage

Build configurations are stored in:
- `%APPDATA%/OpenMPLauncher/builds/{buildType}/config.json`
- `%APPDATA%/OpenMPLauncher/builds/{buildType}/settings.xml`

## Best Practices

1. **Always Verify** - Never skip checksum verification
2. **Keep Backup** - Maintain backup during build switch
3. **Monitor Storage** - Warn when storage space is low
4. **Gradual Updates** - Download in chunks for stability
5. **Background Tasks** - Allow background downloads
6. **Pause/Resume** - Support pausing large downloads
