using System;
using UnityEditor;
using UnityEngine;

public class AssetsImporter: AssetPostprocessor {
    void OnPreprocessTexture() {
      if (assetPath.Contains("Assets/Sprites/Items")) {
        TextureImporter importer = assetImporter as TextureImporter;
        importer.textureType = TextureImporterType.Sprite;
        importer.filterMode = FilterMode.Point;
        importer.isReadable = true;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.textureCompression = TextureImporterCompression.Uncompressed;

        TextureImporterSettings settings = new TextureImporterSettings();
        importer.ReadTextureSettings(settings);
        settings.spriteAlignment = (int)SpriteAlignment.TopLeft;
        importer.SetTextureSettings(settings);
      // TODO: remove next.
      } else if (assetPath.Contains("Assets/Sprites/")) {
        TextureImporter importer = assetImporter as TextureImporter;
        importer.textureType = TextureImporterType.Sprite;
        importer.filterMode = FilterMode.Point;
        importer.isReadable = true;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
      }
    }
}