using Microsoft.Extensions.Logging;
using PicoFacialDataModule.Models;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using VRCFaceTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PicoFacialDataModule
{
    enum PicoFacialDataPayload
    {
        FT_INFO_START,
        PXR_EYE_POSE_START = 380,
        PXR_EYE_POSE_END = PXR_EYE_POSE_START + 156
    }

    public class PicoFacialDataModule : ExtTrackingModule
    {
        private const int PORT = 9030;
        private const string MULTICAST_ADDRESS = "239.255.255.250";

        private const string DISCOVER_PAYLOAD = "DISCOVER_DAEMON";

        private const string PING = "MARCO";
        private const string REPLY = "POLO";

        private const string STOP = "STOP";

        private UdpClient? _udpClient;
        private IPEndPoint? _client;
        private bool _established;

        private FaceTrackingParser? _faceTrackingParser;
        private EyeTrackingParser? _eyeTrackingParser;

        private ModuleSettings? _moduleSettings;

        public override (bool SupportsEye, bool SupportsExpression) Supported => (true, false);

        public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eyeAvailable, bool expressionAvailable)
        {
            try
            {
                ModuleInformation.Name = "Pico 4 P/E Facial Tracking Daemon";
                ModuleInformation.Active = true;

                var stream = GetType().Assembly.GetManifestResourceStream("PicoFacialDataModule.Assets.icon.png");

                ModuleInformation.StaticImages = stream != null ? new List<Stream> { stream } : ModuleInformation.StaticImages;

                _udpClient = new UdpClient(PORT)
                {
                    EnableBroadcast = true,
                    MulticastLoopback = false,
                };

                _udpClient.Client.ReceiveTimeout = 2000;

                _faceTrackingParser = new FaceTrackingParser();
                _eyeTrackingParser = new EyeTrackingParser();

                _moduleSettings = SettingsManager.GetOrCreate();

                return (!_moduleSettings.DisableEyeTracking, !_moduleSettings.DisableFaceTracking);
            } catch (Exception e)
            {
                Logger.LogCritical($"Initialization failed with the following message: {e.Message}\n Stacktrace:\n{e.StackTrace}");
                return (false, false);
            }
        }

        public override void Update()
        {
            if (!ModuleInformation.Active)
            {
                Thread.Sleep(500);
                return;
            }

            try
            {
                if (!_established)
                {
                    byte[]? initialResult = Start();

                    _established = true;
                    ProcessReply(initialResult);
                }

                byte[]? result = null;

                try
                {
                    IPEndPoint? receiver = null;
                    result = _udpClient!.Receive(ref receiver);
                }
                catch
                {
                    _established = false;
                }

                ProcessReply(result);

            } catch (Exception e)
            {
                Logger.LogCritical($"The module failed with the following exception: {e.Message}\n Stacktrace:\n{e.StackTrace}");

                // Good night!
                Thread.Sleep(Timeout.Infinite);
            }
        }

        public override void Teardown()
        {
            if (_udpClient != null && _client != null)
                _udpClient.Send(Encoding.UTF8.GetBytes(STOP), _client);

            if (_udpClient != null)
                _udpClient.Dispose();
        }

        private void ProcessReply(byte[]? result)
        {
            if (result == null)
                return;

            // Keep-alive ping.
            if (result.Length == PING.Length + 1)
            {
                _udpClient!.Send(Encoding.UTF8.GetBytes(REPLY), _client);
                return;
            }

            if (result.Length < (int)PicoFacialDataPayload.PXR_EYE_POSE_END || result.Length > (int)PicoFacialDataPayload.PXR_EYE_POSE_END)
                return;

            if (!MemoryMarshal.TryRead<PicoFTInfo>(result![(int)PicoFacialDataPayload.FT_INFO_START..(int)PicoFacialDataPayload.PXR_EYE_POSE_START], out var picoFTInfo))
                return;

            if (!MemoryMarshal.TryRead<PxrEyePoseDataV2>(result![(int)PicoFacialDataPayload.PXR_EYE_POSE_START..], out var eyeData))
                return;

            if (!_moduleSettings!.DisableFaceTracking)
                _faceTrackingParser!.Parse(picoFTInfo);
           
            if (!_moduleSettings.DisableEyeTracking)
                _eyeTrackingParser!.Parse(eyeData, picoFTInfo);
        }

        /// <summary>
        /// Wakes up the peer daemon by sending a ping, and waiting for a reply.
        /// The daemon will shut down automatically once the UDP port gets disposed.
        /// </summary>
        /// <returns></returns>
        private byte[] Start()
        {
            var broadCastEndpoint = new IPEndPoint(IPAddress.Parse(MULTICAST_ADDRESS), PORT);
            var discoverPayload = Encoding.UTF8.GetBytes(DISCOVER_PAYLOAD);

            byte[]? reply = null;

            // Get all network cards.
            var networkIPs = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            while (true)
            {
                foreach (var IP in networkIPs)
                {
                    _udpClient!.Client.SetSocketOption(
                        SocketOptionLevel.IP,
                        SocketOptionName.MulticastInterface,
                        IP.GetAddressBytes()
                     );
                    _udpClient.Send(discoverPayload, discoverPayload.Length, broadCastEndpoint);
                }

                IPEndPoint? receiver = null;

                try
                {
                    reply = _udpClient!.Receive(ref receiver);
                }
                catch { }

                if (reply != null)
                {
                    _client = receiver!;
                    return reply;
                }
            }
        }
    }
}
