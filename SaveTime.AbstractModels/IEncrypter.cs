namespace SaveTime.AbstractModels
{
    public interface IEncrypter
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
