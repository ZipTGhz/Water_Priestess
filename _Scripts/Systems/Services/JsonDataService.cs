using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    private const string KEY = "9XkGpBaOfzlAi1bwyM/ISfvnRg+l6GL6vpxuB97qaz8=";
    private const string IV = "8wR9Sz5TciS11oXq1i5ywg==";

    public void SaveData<T>(string relativePath, T data, bool encrypted)
    {
        string path = Application.persistentDataPath + relativePath;
        if (File.Exists(path))
        {
            Debug.Log("Data exist. Deleting old file and writing a new one!");
            File.Delete(path);
        }
        using FileStream stream = File.Create(path);
        if (encrypted)
            WriteEncryptedData(data, stream);
        else
        {
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }

    //Mã hóa sẽ chuyển tất cả dữ liệu thành một mảng byte
    private void WriteEncryptedData<T>(T data, FileStream stream)
    {
        using Aes aesProvider = Aes.Create();
        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);
        using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new(stream, cryptoTransform, CryptoStreamMode.Write);

        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data)));
    }

    public T LoadData<T>(string relativePath, bool encrypted)
    {
        string path = Application.persistentDataPath + relativePath;
        if (!File.Exists(path))
        {
            Debug.LogError($"{path} doesn't not exist!");
            throw new FileNotFoundException($"{path} doesn't not exist!");
        }
        T data;
        if (encrypted)
            data = ReadEncryptedData<T>(path);
        else
            data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

        return data;
    }

    //Giải mã sẽ cần phải đọc ngược mảng byte thành dữ liệu
    private T ReadEncryptedData<T>(string path)
    {
        byte[] fileBytes = File.ReadAllBytes(path);
        using Aes aesProvider = Aes.Create();
        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor();
        using MemoryStream memoryStream = new(fileBytes);
        using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Read);

        using StreamReader reader = new(cryptoStream);

        string result = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<T>(result);
    }
}
