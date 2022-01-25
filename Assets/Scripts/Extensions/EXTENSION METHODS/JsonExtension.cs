using UnityEngine;
using System.IO;

namespace ExtensionMethods
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

        #region JsonSerialize
        /// <summary>
        /// Serializar e salvar valores em um arquivo json.
        /// </summary>
        /// <param name="path">Diretório onde o arquivo será salvo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonSerialize<T>(this T data, PathDirectory path, string fileName)
        {
            string _path = Path(path, string.Empty, fileName, true);
            string _json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_path, _json);


#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        #endregion

        #region JsonSerialize
        /// <summary>
        /// Serializar e salvar valores em um arquivo json.
        /// </summary>
        /// <param name="path">Diretório onde o arquivo será salvo</param>
        /// <param name="folder">Complemento do local do arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonSerialize<T>(this T data, PathDirectory path, string folder, string fileName)
        {
            string _path = Path(path, folder, fileName, true);
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
                return;

            string json = File.ReadAllText(_path);
            outData = JsonUtility.FromJson<T>(json);

        }
        #endregion

        #region JsonDeserialize Class
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="outData">Saida do valor</param>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize<T>(this T data, out T outData, PathDirectory path, string fileName)
        {
            string _path = Path(path, string.Empty, fileName, false);

            outData = data;
            if (_path == string.Empty || !File.Exists(_path))
                return;

            string json = File.ReadAllText(_path);
            outData = JsonUtility.FromJson<T>(json);

        }
        #endregion

        #region JsonDeserialize Class
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="outData">Saida do valor</param>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="folder">Complemento do local do arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize<T>(this T data, out T outData, PathDirectory path, string folder, string fileName)
        {
            string _path = Path(path, folder, fileName, false);

            outData = data;
            if (_path == string.Empty || !File.Exists(_path))
                return;

            string json = File.ReadAllText(_path);
            outData = JsonUtility.FromJson<T>(json);

        }
        #endregion


        #region JsonDeserialize Object
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="outData">Saida do valor</param>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="fileName">Nome dor arquivo</param>
        public static void JsonDeserialize(this object data, out object outData, string path, string fileName)
        {
            string _path = Path(PathDirectory.None, path, fileName, false);

            outData = data;
            if (_path == string.Empty || !File.Exists(_path))
                return;

            string json = File.ReadAllText(_path);
            outData = JsonUtility.FromJson<object>(json);
        }
        #endregion

        #region JsonDeserialize Object
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="outData">Saida do valor</param>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize(this object data, out object outData, PathDirectory path, string fileName)
        {
            string _path = Path(path, string.Empty, fileName, false);

            outData = data;
            if (_path == string.Empty || !File.Exists(_path))
                return;

            string json = File.ReadAllText(_path);
            outData = JsonUtility.FromJson<object>(json);
        }
        #endregion

        #region JsonDeserialize Object
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="outData">Saida do valor</param>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="folder">Complemento do local do arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize(this object data, out object outData, PathDirectory path, string folder, string fileName)
        {
            string _path = Path(path, folder, fileName, false);

            outData = data;
            if (_path == string.Empty || !File.Exists(_path))
                return;

            string json = File.ReadAllText(_path);
            outData = JsonUtility.FromJson<object>(json);
        }
        #endregion


        #region JsonDeserialize ScriptableObject
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize(this ScriptableObject data, string path, string fileName)
        {
            string _path = Path(PathDirectory.None, path, fileName, false);

            if (_path == string.Empty || !File.Exists(_path))
                return;

            string _json = File.ReadAllText(_path);
            JsonUtility.FromJsonOverwrite(_json, data);
        }
        #endregion

        #region JsonDeserialize ScriptableObject
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize(this ScriptableObject data, PathDirectory path, string fileName)
        {
            string _path = Path(path, string.Empty, fileName, false);

            if (_path == string.Empty || !File.Exists(_path))
                return;

            string _json = File.ReadAllText(_path);
            JsonUtility.FromJsonOverwrite(_json, data);
        }
        #endregion

        #region JsonDeserialize ScriptableObject
        /// <summary>
        /// Desserializar e carregar valores de um arquivo json.
        /// </summary>
        /// <param name="path">Diretório onde se encontra o arquivo</param>
        /// <param name="folder">Complemento do local do arquivo</param>
        /// <param name="fileName">Nome do arquivo</param>
        public static void JsonDeserialize(this ScriptableObject data, PathDirectory path, string folder, string fileName)
        {
            string _path = Path(path, folder, fileName, false);

            if (_path == string.Empty || !File.Exists(_path))
                return;

            string _json = File.ReadAllText(_path);
            JsonUtility.FromJsonOverwrite(_json, data);
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

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            string _finalPath = string.Empty;
            if (save)
                _finalPath = _path + "/" + fileName + ".json";
            else
                _finalPath = File.Exists(_path + "/" + fileName + ".json") ? _path + "/" + fileName + ".json" : string.Empty;

            return _finalPath;
        }
        #endregion
    }
}