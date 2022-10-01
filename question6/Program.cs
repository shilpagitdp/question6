using System;
using System.IO;
using System.Security.Cryptography;



class filehandling
{
    static void Main(string[] args)
    {
        // Declare file name
        string file;

        // Content in files before copying
        Console.WriteLine("Before copy:\n");
        file = File.ReadAllText("C:\\Users\\shilpa.devanga\\file1.txt");
        Console.WriteLine("data in first:\n" + file);
        file = File.ReadAllText("C:\\Users\\shilpa.devanga\\file2.txt");
        Console.WriteLine("data in last :\n" + file + "\n\n\n");
        // Copy file with overwriting
        File.Copy("C:\\Users\\shilpa.devanga\\file1.txt", "C:\\Users\\shilpa.devanga\\file2.txt", true);
        // Content in files after copying
        Console.WriteLine("After copy:\n");
        file = File.ReadAllText("C:\\Users\\shilpa.devanga\\file1.txt");
        Console.WriteLine("data in first:\n" + file);
        file = File.ReadAllText("C:\\Users\\shilpa.devanga\\file2.txt");
        Console.WriteLine("data in last :\n" + file + "\n\n\n");
        //string fileName = @"C:\Users\shilpa.devanga\file2.txt";
        //File.Encrypt(fileName);
        byte[] key = { 0x02, 0x03, 0x01, 0x03, 0x03, 0x07, 0x07, 0x08, 0x09, 0x09, 0x11, 0x11, 0x16, 0x17, 0x19, 0x16 };
        // ENCRYPT DATA
        try
        {
            // create file stream
            FileStream myStream = new FileStream(@"C:\Users\shilpa.devanga\file2.txt", FileMode.OpenOrCreate);
            // configure encryption key.  
            Aes aes = Aes.Create();
            aes.Key = key;
            // store IV
            byte[] iv = aes.IV;
            myStream.Write(iv, 0, iv.Length);
            // encrypt filestream  
            CryptoStream cryptStream = new CryptoStream(
                myStream,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);
            // write to filestream
            StreamWriter sWriter = new StreamWriter(cryptStream);
            string plainText = "Welcome to the lab of MrNetTek!";
            sWriter.WriteLine(plainText);
            // done
            Console.WriteLine("---SUCCESSFUL ENCRYPTION---\n");
        }
        catch
        {
            // error  
            Console.WriteLine("---ENCRYPTION FAILED---");
            throw;
        }
        // SHOW ENCRYPTED DATA
        try
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\shilpa.devanga\file2.txt");
            // encrypted data
            Console.WriteLine("Encrypted Data: {0}\n\n", text);
            Console.WriteLine("Press any key to view decrypted data\n");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        // DECRYPT DATA
        try
        {
            // create file stream
            FileStream myStream = new FileStream(@"C:\Users\shilpa.devanga\file2.txt", FileMode.Open);
            // create instance
            Aes aes = Aes.Create();
            // reads IV value
            byte[] iv = new byte[aes.IV.Length];
            myStream.Read(iv, 0, iv.Length);
            // decrypt data
            CryptoStream cryptStream = new CryptoStream(
               myStream,
               aes.CreateDecryptor(key, iv),
               CryptoStreamMode.Read);
            // read stream
            StreamReader sReader = new StreamReader(cryptStream);
            // display stream
            Console.WriteLine("\n---SUCCESSFUL DECRYPTION---\n");
            Console.WriteLine("Decrypted data: {0}", sReader.ReadToEnd());
            Console.ReadKey();
        }
        catch
        {
            // error
            Console.WriteLine("---DECRYPTION FAILED---");

        }
    }
}
