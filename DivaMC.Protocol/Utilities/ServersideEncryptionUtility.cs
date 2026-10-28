using System.Data;
using System.Net;
using System.Numerics;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DivaMC.Protocol.IO.Protocol.Packets.Login;
using DivaMC.Protocol.Player.Common;

namespace DivaMC.Protocol.Utilities
{
    public sealed class ServersideEncryptionUtility : IDisposable
    {
        private static readonly HttpClient Http = new();
        
        private readonly RSA Rsa;
        private readonly byte[] Sample;
        private byte[]? Secret = null;
        public required string Username { get; init; }
        public string ServerId { get; init; } = string.Empty;
        public bool Authenticate { get; init; } = true;
        public ServersideEncryptionUtility(int sampleSize = 64)
        {
            Rsa = RSA.Create(1024);
            Sample = RandomNumberGenerator.GetBytes(sampleSize);
        }
        public CBEncryptionRequestPacket GenerateRequest()
        {
            return new()
            {
                Authenticate = Authenticate,
                ServerId = ServerId,
                PublicKey = Rsa.ExportSubjectPublicKeyInfo(),
                DecryptedSample = Sample,
            };
        }
        public byte[] HandleResponse(SBEncryptionResponsePacket sbEncryptionResponsePacketPacket)
        {
            if (Secret is not null) throw new InvalidOperationException();
            bool success = Enumerable.SequenceEqual(Sample, Rsa.Decrypt(sbEncryptionResponsePacketPacket.EncryptedSample, RSAEncryptionPadding.Pkcs1));
            Secret = Rsa.Decrypt(sbEncryptionResponsePacketPacket.EncryptedSecret, RSAEncryptionPadding.Pkcs1);
            if (!success) throw new CryptographicException();
            return Secret;
        }
        public async Task<Profile> AuthenticateAsync(IPAddress? ip = null)
        {
            if (!Authenticate) throw new InvalidOperationException();
            byte[] hash = SHA1.HashData(Encoding.ASCII.GetBytes(ServerId).Concat(Secret ?? []).Concat(Rsa.ExportSubjectPublicKeyInfo()).ToArray());
            Array.Reverse(hash);
            BigInteger number = new BigInteger(hash);
            string hashstring = (number < 0 ? "-" + (-number).ToString("x") : number.ToString("x")).TrimStart('0');
            HttpResponseMessage auth = await Http.GetAsync($"https://sessionserver.mojang.com/session/minecraft/hasJoined?username={Username}&serverId={hashstring}{(ip is not null ? $"&ip={ip}" : string.Empty)}");
            if (auth.StatusCode != HttpStatusCode.OK) throw new AuthenticationException();
            Profile? profile = await JsonSerializer.DeserializeAsync<Profile>(await auth.Content.ReadAsStreamAsync(), Global.JsonSerializerOptions);
            if (profile is null) throw new NoNullAllowedException();
            return profile;
        }
        public void Dispose()
        {
            Rsa.Dispose();
        }
    }
}