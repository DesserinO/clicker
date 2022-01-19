using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SaveClient : MonoBehaviour
{
    int id = 1;
    string score = "";
    ulong[] lvls = { 0, 0};
    public SaveClient(string score, ulong[] lvls)
    {
        this.score = score;
        this.lvls = lvls;
    }

    public void LoadClient(out string return1, out ulong[] return2)
    {
        return1 = this.score;
        return2 = this.lvls;
    }

    static SaveData saveData = new SaveData();
    static int port = 7777; // порт сервера
    static string address = "127.0.0.1"; // адрес сервера

    public void Load()
    {
        try
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);


            var data = new byte[256];
            var receivedData = socket.Receive(data);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            
            /*return1 = data;*/
        }
        catch { }
        }

    public void Save()
    {
        try
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);
            socket.Send(saveData.ToBytes(id, score, lvls));

            // закрываем сокет
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
