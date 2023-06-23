using System.IO;
using UnityEngine;

namespace BaltaRed.ExtensionMethods
{
    public enum PathDirectory { None, Data, Streaming, Persistent }

    public static class JsonExtension
    {
        #region JsonSerialize
        /// <summary>
        /// Serializar e salvar valores em um arquivo json.
        /// </summary>
        /// <param name="path">Diretório onde o arquivo será salvo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonSerialize<T>(this T data, string path, string fileName)
        {
            string _path = Path(PathDirectory.None, path, fileName, true);
            string _json = JsonUtility.ToJson(data, true);

            File.WriteAllText(_path, _json);


#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        #endregion

        #region JsonDeserialize Class
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="outData">Saida do valor</param>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize<T>(this T data, out T outData, string path, string fileName)
        {
            string _path = Path(PathDirectory.None, path, fileName, false);

            outData = data;

            if (_path == string.Empty || !File.Exists(_path))
            {
                return;
            }

            string json = File.ReadAllText(_path);
            outData = JsonUtility.FromJson<T>(json);
        }
        #endregion

        #region Path
        private static string Path(PathDirectory _pathType, string complementPath, string fileName, bool save)
        {
            string _path = string.Empty;

            switch (_pathType)
            {
                case PathDirectory.None:
                    _path = complementPath;
                    break;
                case PathDirectory.Data:
                    _path = System.IO.Path.Combine(Application.dataPath, complementPath);
                    break;

                case PathDirectory.Streaming:
                    _path = System.IO.Path.Combine(Application.streamingAssetsPath, complementPath);
                    break;

                case PathDirectory.Persistent:
                    _path = System.IO.Path.Combine(Application.persistentDataPath, complementPath);
                    break;
            }

            if (Directory.Exists(_path) == false)
            {
                Directory.CreateDirectory(_path);
            }

            string _finalPath;

            if (save)
            {
                _finalPath = _path + "/" + fileName + ".json";
            }
            else
            {
                _finalPath = File.Exists(_path + "/" + fileName + ".json") ? _path + "/" + fileName + ".json" : string.Empty;
            }

            return _finalPath;
        }
        #endregion
    }
}