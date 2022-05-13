
using System;
using System.Text;
using System.Security.Cryptography;

public static class RSA
{
    private const bool OptimalAsymmetricEncryptionPadding = false;

    public static string SignRSASHA1(string data, string privateKeyXML)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKeyXML);

        var dataBytes = Encoding.UTF8.GetBytes(data);
        var signedData = rsa.SignData(dataBytes, CryptoConfig.MapNameToOID("SHA1"));
        return Convert.ToBase64String(signedData);
    }

    public static string SignRSASHA1(string[] data, string privateKeyXML)
    {
        return SignRSASHA1(string.Join("", data), privateKeyXML);
    }

    public static bool VerifyRSASHA1(string data, string signature, string publicKeyXML)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKeyXML);

        return rsa.VerifyData(Encoding.UTF8.GetBytes(data), CryptoConfig.MapNameToOID("SHA1"),
            Convert.FromBase64String(signature));
    }

    public static bool VerifyRSASHA1(string[] data, string signature, string publicKeyXML)
    {
        return VerifyRSASHA1(string.Join("", data), signature, publicKeyXML);
    }

    public static string GenerateKey()
    {
        var rsa = new RSACryptoServiceProvider(1024);
        return string.Format("Private key: {0} ;  Pulic key: {1}", rsa.ToXmlString(true), rsa.ToXmlString(false));
    }

    public static bool Test(string data, string publicKey, string privateKey)
    {
        return VerifyRSASHA1(data, SignRSASHA1(data, privateKey), publicKey);
    }

    public static string ComputeHash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var algorithm = new SHA512CryptoServiceProvider();

        var hashedBytes = algorithm.ComputeHash(inputBytes);

        return Convert.ToBase64String(hashedBytes);
        //return BitConverter.ToString(hashedBytes);
    }

    public static string EncryptText(string text, int keySize, string publicKeyXml)
    {
        var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), keySize, publicKeyXml);
        return Convert.ToBase64String(encrypted);
    }

    public static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
    {
        if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
        var maxLength = GetMaxDataLength(keySize);
        if (data.Length > maxLength)
            throw new ArgumentException(String.Format("Maximum data length is {0}", maxLength), "data");
        if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
        if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");

        using (var provider = new RSACryptoServiceProvider(keySize))
        {
            provider.FromXmlString(publicKeyXml);
            return provider.Encrypt(data, OptimalAsymmetricEncryptionPadding);
        }
    }

    public static string DecryptText(string text, int keySize, string publicAndPrivateKeyXml)
    {
        var decrypted = Decrypt(Convert.FromBase64String(text), keySize, publicAndPrivateKeyXml);
        return Encoding.UTF8.GetString(decrypted);
    }

    public static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
    {
        if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
        if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
        if (String.IsNullOrEmpty(publicAndPrivateKeyXml))
            throw new ArgumentException("Key is null or empty", "publicAndPrivateKeyXml");

        using (var provider = new RSACryptoServiceProvider(keySize))
        {
            provider.FromXmlString(publicAndPrivateKeyXml);
            return provider.Decrypt(data, OptimalAsymmetricEncryptionPadding);
        }
    }

    public static int GetMaxDataLength(int keySize)
    {        
        return ((keySize - 384) / 8) + 37;
    }

    public static bool IsKeySizeValid(int keySize)
    {
        return keySize >= 384 &&
                keySize <= 16384 &&
                keySize % 8 == 0;
    }
}
