﻿using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace szyfrant
{
    class Cryptographer
    {
        AesManaged aes = new AesManaged();
        int iterations = 16999;
        string salt = "|#$HeXzr-q5$v3Q|#$HeXzr-q5$v3Q#|#$HeXzr-q5$v3Q#|#$HeXzr-q5$v3Q##";
        string initializationVector = "|#$HeXzr-q5$v3Q#";

        public Cryptographer()
        {
            setup();
        }

        void setup()
        {
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 256;
            aes.IV = Encoding.ASCII.GetBytes(initializationVector);
        }

        public void encrypt(string path, string password, string destFile)
        {
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            //string tempPath = zip(path);
            //byte[] valueBytes = File.ReadAllBytes(tempPath);
            byte[] valueBytes = zip(path);
            byte[] encryptedBytes;

            //BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
            //byte[] valueBytes = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);

            Rfc2898DeriveBytes passBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            byte[] keyBytes = passBytes.GetBytes(aes.KeySize / 8);
            aes.Key = keyBytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(valueBytes, 0, valueBytes.Length);
                }
                encryptedBytes = memoryStream.ToArray();
            }
            aes.Clear();
            File.WriteAllBytes(destFile, encryptedBytes);
        }

        public void decrypt(string path, string password, string destPath)
        {
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] valueBytes = File.ReadAllBytes(path);
            byte[] decrypted;

            //FileStream fs = new FileStream(path, FileMode.Open);
            //BinaryReader binaryReader = new BinaryReader(fs, Encoding.UTF8);
            //byte[] valueBytes = binaryReader.ReadBytes((int)fs.Length);

            //BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
            //byte[] valueBytes = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
            
            Rfc2898DeriveBytes passBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            byte[] keyBytes = passBytes.GetBytes(aes.KeySize / 8);
            aes.Key = keyBytes;

            using (MemoryStream memoryStream = new MemoryStream(valueBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(valueBytes, 0, valueBytes.Length);
                }
                decrypted = memoryStream.ToArray();
            }

            aes.Clear();
            //File.WriteAllBytes("decrypted", decrypted);
            //unzip("decrypted", destPath);
            unzip2(decrypted, destPath);
        }

        void unzip(string path, string destPath)
        {
            using (ZipArchive zip = ZipFile.Open(path, ZipArchiveMode.Read))
            {
                zip.ExtractToDirectory(destPath);
            }
        }

        void unzip2(byte[] zippedBytes, string destPath)
        {
            MemoryStream ms = new MemoryStream(zippedBytes);
            using (var archive = new ZipArchive(ms, ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory(destPath);
            }
        }

        byte[] zip(string path)
        {
            byte[] zippedBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    archive.CreateEntryFromFile(path, getFilenameFromPath(path));
                }
                zippedBytes = memoryStream.GetBuffer();
            }
            return zippedBytes;
        }

        string getFilenameFromPath(string path)
        {
            return path.Substring(path.LastIndexOf(@"\") + 1);
        }
    }
}