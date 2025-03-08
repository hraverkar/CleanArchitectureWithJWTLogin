namespace hr.makemystamp.com.application.Interfaces
{
    public interface ITokenBlackListService
    {
        public void RevokeToken(string token, DateTime expiry);
        public bool IsTokenRevoked(string token);
    }
}
