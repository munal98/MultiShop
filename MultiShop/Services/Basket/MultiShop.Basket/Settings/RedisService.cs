using StackExchange.Redis;

namespace MultiShop.Basket.Settings
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer? _connectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));  // Host parametresi null olamaz
            _port = port > 0 ? port : throw new ArgumentOutOfRangeException(nameof(port), "Port must be a positive number.");  // Port negatif olamaz
        }

        public void Connect()
        {
            // Bağlantı zaten sağlanmışsa, tekrar bağlanmaya gerek yok
            if (_connectionMultiplexer is not null && _connectionMultiplexer.IsConnected)
                return;

            // Bağlantıyı kur
            _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        }

        public IDatabase GetDb(int db = 0)
        {
            EnsureConnected();  // Bağlantı yoksa hata fırlat
            return _connectionMultiplexer!.GetDatabase(db);  // Bağlantı sağlandıysa veritabanını al
        }

        private void EnsureConnected()
        {
            // Eğer bağlantı yoksa, Connect() metodu çağrılmalı
            if (_connectionMultiplexer is null || !_connectionMultiplexer.IsConnected)
            {
                throw new InvalidOperationException("Redis connection is not established. Call Connect() first.");
            }
        }
    }
}
