using System;
using NAudio.Wave;

public class AudioPlayer
{
    public AudioPlayer(string v)
    {
        V = v;
    }

    public string V { get; }

    public void PlayAudio()
    {
        string filePath = @"C:\Users\kdynt\Desktop\CyberSecurittChatBot-main (2)\CyberSecurittChatBot-main\CyberSecurittChatBot\Chatbot_voice.mp3";

        try
        {
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                Console.WriteLine("Playing audio...");
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error playing audio: " + ex.Message);
        }
    }
}
