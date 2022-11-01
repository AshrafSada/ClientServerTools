using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    private const String ADDRESS = "www.google.com";
    private const Int32 PORT = 80;

    private static void Main()
    {
        ConnectToServer();

        //
        _ = Console.ReadKey();
    }

    private static void ConnectToServer()
    {
        try
        {
            using var client = new TcpClient(ADDRESS, PORT);
            //client.Connect();
            if (client.Connected)
            {
                using var netStream = client.GetStream();

                var resMessage = $"connecting to {ADDRESS} via {PORT}";
                Console.WriteLine(resMessage);

                // get decoded data
                byte[] bytes = Encoding.UTF8.GetBytes(resMessage);

                // write the binary data to network stream
                netStream.Write(bytes, 0, bytes.Length);

                // write response to console
                using var streamReader = new StreamReader(netStream, Encoding.UTF8);
                var res = streamReader.ReadToEnd();
                Console.WriteLine(res.Length);
            }
            else
            {
                Console.WriteLine($"Connection to address {ADDRESS} failed, aborting ...");
            }
        }
        catch (InvalidOperationException invalidOperationException)
        {
            Console.WriteLine(invalidOperationException.Message);
            throw;
        }
        catch (ArgumentNullException argumentNullException)
        {
            Console.WriteLine(argumentNullException.Message);
            throw;
        }
        catch (ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            Console.WriteLine(argumentOutOfRangeException.Message);
            throw;
        }
        catch (FormatException formatException)
        {
            Console.WriteLine(formatException.Message);
            throw;
        }
        catch (SocketException socketException)
        {
            Console.WriteLine(socketException.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
